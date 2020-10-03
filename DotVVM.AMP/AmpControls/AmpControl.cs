using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DotVVM.AMP.Config;
using DotVVM.AMP.Enums;
using DotVVM.AMP.Validator;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.AmpControls
{
    public abstract class AmpControl : HtmlGenericControl, IAmpControl
    {
        public string Sizes
        {
            get { return (string)GetValue(SizesProperty); }
            set { SetValue(SizesProperty, value); }
        }
        public static readonly DotvvmProperty SizesProperty
            = DotvvmProperty.Register<string, AmpControl>(c => c.Sizes, null);

        public string Heights
        {
            get { return (string)GetValue(HeightsProperty); }
            set { SetValue(HeightsProperty, value); }
        }
        public static readonly DotvvmProperty HeightsProperty
            = DotvvmProperty.Register<string, AmpControl>(c => c.Heights, null);

        public bool NoLoading
        {
            get { return (bool)GetValue(NoLoadingProperty); }
            set { SetValue(NoLoadingProperty, value); }
        }
        public static readonly DotvvmProperty NoLoadingProperty
            = DotvvmProperty.Register<bool, AmpControl>(c => c.NoLoading, false);

        public string Media
        {
            get { return (string)GetValue(MediaProperty); }
            set { SetValue(MediaProperty, value); }
        }
        public static readonly DotvvmProperty MediaProperty
            = DotvvmProperty.Register<string, AmpControl>(c => c.Media, null);


        public AmpLayout? Layout
        {
            get { return (AmpLayout)GetValue(LayoutProperty); }
            set { SetValue(LayoutProperty, value); }
        }
        public static readonly DotvvmProperty LayoutProperty
            = DotvvmProperty.Register<AmpLayout?, AmpControl>(c => c.Layout);

        public string Height
        {
            get { return GetPropertyValueOrLoadAttribute(HeightProperty, "height"); }
            set { SetValue(HeightProperty, value); Attributes.Remove("height"); }
        }
        public static readonly DotvvmProperty HeightProperty
            = DotvvmProperty.Register<string, AmpControl>(c => c.Height, null);

        public string Width
        {
            get { return GetPropertyValueOrLoadAttribute(WidthProperty, "width");}
            set { SetValue(WidthProperty, value); Attributes.Remove("width"); }
        }

        public DotvvmAmpConfiguration AmpConfiguration { get; set; }
        protected string GetPropertyValueOrLoadAttribute(DotvvmProperty property, string attributeName)
        {
            if (IsPropertySet(property))
            {
                return GetValue<string>(property);
            }

            var attribute = GetAttribute(attributeName);
            if (attribute!=null)
            {
                Attributes.Remove(attributeName);
                SetValue(property,attribute);
            }
            return attribute;
        }

        public static readonly DotvvmProperty WidthProperty
            = DotvvmProperty.Register<string, AmpControl>(c => c.Width, null);

        protected abstract IEnumerable<AmpLayout> SupportedLayouts { get; }

        public AmpControl(string tagName) : base(tagName)
        {
            
        }

        protected override void OnLoad(IDotvvmRequestContext context)
        {
            base.OnLoad(context);
            Validate();
        }

        protected virtual void Validate()
        {
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            if (IsPropertySet(LayoutProperty))
            {
                AddLayoutAttribute(writer, Layout);
            }
            AddAttributeIfPresent(writer, "height", Height);
            AddAttributeIfPresent(writer, "width", Width);
            AddAttributeIfPresent(writer, "sizes", Sizes);
            AddAttributeIfPresent(writer, "heights", Heights);
            AddBoolAttributeIfPresent(writer, "NoLoading", NoLoading);
            AddAttributeIfPresent(writer, "media", Media);
            base.AddAttributesToRender(writer, context);
        }

        public void AddLayoutAttribute(IHtmlWriter writer, AmpLayout? layout)
        {
            if (!layout.HasValue)
                return;

            if (!SupportedLayouts.Contains(layout.Value))
            {
                throw new AmpException($"Layout {layout} is not supported. Supported layouts are: {string.Join(" ,",SupportedLayouts)}.");
            }
            string layoutText = null;
            switch (layout)
            {
                case AmpLayout.container:
                    layoutText = "container";
                    break;
                case AmpLayout.noDisplay:
                    layoutText = "nodisplay";
                    break;
                case AmpLayout.fixedLayout:
                    layoutText = "fixed";
                    break;
                case AmpLayout.responsive:
                    layoutText = "responsive";
                    break;
                case AmpLayout.fixedHeight:
                    layoutText = "fixed-height";
                    break;
                case AmpLayout.fill:
                    layoutText = "fill";
                    break;
                case AmpLayout.flexItem:
                    layoutText = "flex-item";
                    break;
                case AmpLayout.intrinsic:
                    layoutText = "intrinsic";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            writer.AddAttribute("layout", layoutText);
        }
        private void AddBoolAttributeIfPresent(IHtmlWriter writer, string attributeName, bool add)
        {
            if (add)
            {
                writer.AddAttribute(attributeName,null);
            }
        }

        protected virtual void AddAttributeIfPresent(IHtmlWriter writer, string attributeName, string attributeValue)
        {
            if (!string.IsNullOrWhiteSpace(attributeValue))
            {
                writer.AddAttribute(attributeName, attributeValue);
            }
        }
        
        protected string GetAttribute(string attributeName)
        {
            if (Attributes.ContainsKey(attributeName))
            {
                if (Attributes[attributeName] is IStaticValueBinding binding)
                {
                    return binding.Evaluate(this)?.ToString();
                }
                return (string) Attributes[attributeName];
            }

            return null;
        }
    }

    public class Layout : AmpControl
    {
        public Layout() : base("amp-layout")
        {
        }

        protected override IEnumerable<AmpLayout> SupportedLayouts => new []{AmpLayout.fill,AmpLayout.fixedLayout,AmpLayout.fixedHeight,AmpLayout.flexItem,AmpLayout.intrinsic,AmpLayout.noDisplay,AmpLayout.responsive,AmpLayout.container};
    }
}
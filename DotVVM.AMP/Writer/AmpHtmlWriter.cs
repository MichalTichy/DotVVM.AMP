using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using DotVVM.AMP.Config;
using DotVVM.AMP.Enums;
using DotVVM.AMP.Validator;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.Writer
{
    public class AmpHtmlWriter : IAmpHtmlWriter
    {
        protected IHtmlWriter writer;
        protected readonly IAmpValidator validator;
        protected readonly ILogger Logger;

        protected Dictionary<string, string> Attributes = new Dictionary<string, string>();
        protected bool StartTagSkipped = false;
        public AmpHtmlWriter(StreamWriter textWriter, IDotvvmRequestContext context, IAmpValidator validator, ILogger logger)
        {
            writer = new HtmlWriter(textWriter, context);
            this.validator = validator;
            Logger = logger;
        }

        public virtual void AddAttribute(string name, string value, bool append = false, string appendSeparator = null)
        {
            if (!validator.CheckAttribute(name, value)) return;

            if (Attributes.ContainsKey(name))
            {
                if (append)
                    Attributes[name] += appendSeparator ?? string.Empty + value;
                else
                    Attributes[name] = value;
            }
            else
            {
                Attributes.Add(name,value);
            }
            writer.AddAttribute(name, value, append, appendSeparator);
        }

        public virtual void AddStyleAttribute(string name, string value)
        {
            if (validator.CheckStyleAttribute(name, value))
            {
                Attributes.Add(name,value);
                writer.AddStyleAttribute(name, value);
            }
        }

        public virtual void AddKnockoutDataBind(string name, string expression)
        {
            if (validator.ValidateKnockoutDataBind(name))
                writer.AddKnockoutDataBind(name, expression); // this should never happen because knockout bindings are invalid in AMP
        }

        public virtual void AddKnockoutDataBind(string name, KnockoutBindingGroup bindingGroup)
        {
            if (validator.ValidateKnockoutDataBind(name))
                writer.AddKnockoutDataBind(name, bindingGroup); // this should never happen because knockout bindings are invalid in AMP
        }

        public virtual void RenderBeginTag(string name)
        {
            if (validator.CheckHtmlTag(name,Attributes))
            {
                Attributes.Clear();
                writer.RenderBeginTag(name);
            }
            else
            {
                Logger?.LogError($"Html tag {name} not rendered. All its content will not be rendered.");
                StartTagSkipped = true;
            }
        }

        public virtual void RenderSelfClosingTag(string name)
        {
            if (validator.CheckHtmlTag(name, Attributes))
            {
                Attributes.Clear();
                writer.RenderSelfClosingTag(name);
            }
        }

        public virtual void RenderEndTag()
        {
            if (!StartTagSkipped)
            {
                writer.RenderEndTag();
            }
            else
            {
                StartTagSkipped = false;
            }
        }

        public virtual void WriteText(string text)
        {
            if (StartTagSkipped) return;
            writer.WriteText(text);
        }

        public virtual void WriteUnencodedText(string text)
        {
            if (StartTagSkipped) return;
            writer.WriteUnencodedText(text);
        }

        public virtual void WriteHtmlAttribute(string name, string value)
        {
            if (validator.CheckAttribute(name, value))
            {
                Attributes.Add(name,value);
                writer.WriteHtmlAttribute(name, value);
            }
        }

        public virtual void SetErrorContext(DotvvmBindableObject obj)
        {
            writer.SetErrorContext(obj);
        }
    }
}
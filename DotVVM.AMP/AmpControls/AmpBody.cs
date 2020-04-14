using System;
using System.Reflection;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.AMP.AmpControls
{
    public class AmpBody : HtmlGenericControl
    {
        internal static FieldInfo isBodyRenderedPropertyInfo = typeof(ResourceManager).GetField("BodyRendered", BindingFlags.NonPublic | BindingFlags.Instance);

        public AmpBody() : base("body")
        {

        }
        protected override void RenderBeginTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            isBodyRenderedPropertyInfo.SetValue(context.ResourceManager, true);

            base.RenderBeginTag(writer, context);
        }

        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            ResourcesRenderer.RenderResources(context.ResourceManager,writer,context,ResourceRenderPosition.Body);
            base.RenderContents(writer, context);
        }
    }
}
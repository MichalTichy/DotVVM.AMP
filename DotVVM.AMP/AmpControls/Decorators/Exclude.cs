using System;
using System.Collections.Generic;
using System.Text;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.AmpControls.Decorators
{
    public class Exclude:AmpDecoratorBase
    {
        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            if (!IsInAmpPage)
            {
                base.RenderContents(writer, context);
            }
        }
    }
}

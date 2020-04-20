using System.Linq;
using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public class GridViewTransform : ControlValidatorTransformBase<GridView>
    {
        public GridViewTransform(DotvvmAmpConfiguration ampConfiguration, ILogger logger) : base(ampConfiguration, logger)
        {
        }


        protected override void ValidateControl(GridView control, IDotvvmRequestContext context)
        {
            IsPropertySupported(control, view => !view.InlineEditing, view => view.InlineEditing = false, nameof(GridView.InlineEditing));

            IsPropertySupported(control, view => !view.EditRowDecorators?.Any() ?? false, view => view.EditRowDecorators?.Clear(), nameof(GridView.EditRowDecorators));

            IsPropertySupported(control, view => view.SortChanged == null, view => view.SortChanged = null, nameof(GridView.SortChanged));
        }
    }
}
using System.Linq;
using DotVVM.AMP.AmpControls.Internal;
using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public class GridViewTransform : ControlReplacementTransformBase
    {
        public GridViewTransform(DotvvmAmpConfiguration ampConfiguration, ILogger logger) : base(ampConfiguration, logger)
        {
        }


        protected override void ValidateControl(DotvvmControl control, IDotvvmRequestContext context)
        {
            var gridview = control as GridView;
            IsPropertySupported(gridview, view => !((GridView) view).InlineEditing, view => ((GridView)view).InlineEditing = false, nameof(GridView.InlineEditing));

            IsPropertySupported(gridview, view => !((GridView)view).EditRowDecorators?.Any() ?? false, view => ((GridView)view).EditRowDecorators?.Clear(), nameof(GridView.EditRowDecorators));

            IsPropertySupported(gridview, view => ((GridView)view).SortChanged == null, view => ((GridView)view).SortChanged = null, nameof(GridView.SortChanged));
        }

        public override bool CanTransform(DotvvmControl control)
        {
            return control is GridView;
        }

        protected override DotvvmControl CreateReplacementControl(DotvvmControl control)
        {
            return new AmpGridView(AmpConfiguration.ControlTransforms);
        }
    }
}
using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public class GridViewTextColumnTransform : GridViewColumnTransformBase<GridViewTextColumn>
    {
        public GridViewTextColumnTransform(DotvvmAmpConfiguration ampConfiguration, ILogger logger) : base(ampConfiguration, logger)
        {
        }

        protected override void ValidateControl(GridViewTextColumn control, IDotvvmRequestContext context)
        {
            IsPropertySupported(control,column => column.ChangedBinding==null,column => column.ChangedBinding=null,nameof(GridViewTextColumn.ChangedBinding));
            base.ValidateControl(control, context);
        }
    }
}
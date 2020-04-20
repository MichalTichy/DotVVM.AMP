using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public class GridViewTemplateColumnTransform : GridViewColumnTransformBase<GridViewTemplateColumn>
    {
        public GridViewTemplateColumnTransform(DotvvmAmpConfiguration ampConfiguration, ILogger logger) : base(ampConfiguration, logger)
        {
        }

        protected override void ValidateControl(GridViewTemplateColumn control, IDotvvmRequestContext context)
        {
            IsPropertySupported(control,column => column.EditTemplate==null,column => column.EditTemplate = null,nameof(GridViewTextColumn.ChangedBinding));
            base.ValidateControl(control, context);
        }
    }
}
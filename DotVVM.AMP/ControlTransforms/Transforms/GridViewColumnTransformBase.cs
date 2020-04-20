using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public class GridViewColumnTransformBase<TColumn> : ControlValidatorTransformBase<TColumn> where TColumn : GridViewColumn
    {
        protected GridViewColumnTransformBase(DotvvmAmpConfiguration ampConfiguration, ILogger logger) : base(ampConfiguration, logger)
        {
        }

        protected override void ValidateControl(TColumn control, IDotvvmRequestContext context)
        {
            IsPropertySupported(control,column => !column.AllowSorting,column => control.AllowSorting=false,nameof(GridViewColumn.AllowSorting));
            IsPropertySupported(control,column => !column.IsEditable,column => control.IsEditable = false,nameof(GridViewColumn.IsEditable));
        }
    }
}
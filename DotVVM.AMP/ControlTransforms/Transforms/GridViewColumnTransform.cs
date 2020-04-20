using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public class GridViewColumnTransform : GridViewColumnTransformBase<GridViewColumn>
    {
        public GridViewColumnTransform(DotvvmAmpConfiguration ampConfiguration, ILogger logger) : base(ampConfiguration, logger)
        {
        }
    }
}
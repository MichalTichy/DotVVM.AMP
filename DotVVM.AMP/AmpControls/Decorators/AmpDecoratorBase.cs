using DotVVM.AMP.Config;
using DotVVM.Framework.Controls;

namespace DotVVM.AMP.AmpControls.Decorators
{
    public abstract class AmpDecoratorBase : Decorator
    {
        public DotvvmAmpConfiguration Configuration { get; set; }
        public bool IsInAmpPage => Configuration != null;
    }
}
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public interface IControlTransform
    {
        bool CanTransform(DotvvmControl control);
        DotvvmControl Transform(DotvvmControl control, IDotvvmRequestContext context);
    }
}
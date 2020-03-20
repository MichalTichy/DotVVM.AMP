using DotVVM.AMP.ControlTransforms.Transforms;
using DotVVM.Framework.Controls;

namespace DotVVM.AMP.ControlTransforms
{
    public interface IAmpControlTransformsRegistry
    {
        void Register(IControlTransform transform);
        IControlTransform GetTransform(DotvvmControl control);
    }
}
using DotVVM.Framework.Controls;

namespace DotVVM.AMP.ControlTransforms.Transforms
{
    public interface IControlTransform
    {
        bool CanTransform(DotvvmControl control);
        void Transform(DotvvmControl control);
    }
}
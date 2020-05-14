using DotVVM.AMP.ControlTransforms.Transforms;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.ControlTransforms
{
    public interface IAmpControlTransformsRegistry
    {
        void Register(IControlTransform transform);
        IControlTransform GetTransform(DotvvmControl control);
        void ApplyTransforms(DotvvmControl  root,IDotvvmRequestContext context);
    }
}
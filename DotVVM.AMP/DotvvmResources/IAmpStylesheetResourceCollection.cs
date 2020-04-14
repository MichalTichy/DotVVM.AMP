using System.Threading.Tasks;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.AMP.DotvvmResources
{
    public interface IAmpStylesheetResourceCollection
    {
        void Add(IResourceLocation resource);
        Task<string> GetAmpCustomCode(IDotvvmRequestContext context);
        Task<string> GetAmpKeyframesCode(IDotvvmRequestContext context);
    }
}
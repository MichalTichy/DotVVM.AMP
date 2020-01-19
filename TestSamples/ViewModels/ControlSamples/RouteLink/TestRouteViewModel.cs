using DotVVM.Framework.ViewModel;

namespace TestSamples.ViewModels.ControlSamples.RouteLink
{
    public class TestRouteViewModel
    {
        [FromRoute("Id")]
        public int Id { get; set; }
    }
}
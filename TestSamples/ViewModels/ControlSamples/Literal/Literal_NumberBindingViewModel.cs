using System.Collections.Generic;

namespace TestSamples.ViewModels.ControlSamples.Literal
{
    public class Literal_NumberBindingViewModel
    {
        public IEnumerable<int> Numbers { get; set; } = new List<int>() {
            1,2,3,4,5,6,7,8
        };
        public IEnumerable<string> Texts { get; set; } = new List<string>() {
            "a", "b", "c", "d"
        };
    }
}

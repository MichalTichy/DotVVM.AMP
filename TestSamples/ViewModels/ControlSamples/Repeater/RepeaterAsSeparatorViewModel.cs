using System.Collections.Generic;

namespace TestSamples.ViewModels.ControlSamples.Repeater
{
    public class RepeaterAsSeparatorViewModel : SeparatorViewModel
    {
        public List<string> Separators { get; set; } = new List<string>
        {
            "First separator",
            "Second separator",
            "Third separator"
        };

        public int Counter { get; set; }

        public void Increment()
        {
            Counter++;
        }
    }
}

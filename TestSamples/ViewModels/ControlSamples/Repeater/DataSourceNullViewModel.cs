using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace TestSamples.ViewModels.ControlSamples.Repeater
{
    public class DataSourceNullViewModel : DotvvmViewModelBase
    {
        public List<string> Collection { get; set; }

        public void SetCollection()
        {
            Collection = new List<string>
            {
                "First",
                "Second",
                "Third",
            };
        }
    }
}

using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace TestSamples.ViewModels.ControlSamples.Repeater
{
    public class RequiredResourceViewModel : DotvvmViewModelBase
    {
        public List<string> Items { get; set; } = new List<string>(0);
    }
}

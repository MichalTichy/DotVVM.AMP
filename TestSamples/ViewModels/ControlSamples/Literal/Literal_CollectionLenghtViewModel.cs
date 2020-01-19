using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace TestSamples.ViewModels.ControlSamples.Literal
{
    public class Literal_CollectionLengthViewModel : DotvvmViewModelBase
    {
        public List<string> MyCollection { get; set; } = new List<string>();
        public string[] TestArray { get; set; } = new string[] { };

        public void AddItemToCollection()
        {
            MyCollection.Add("Item");
            TestArray = getTestArray(MyCollection.Count);
        }

        private string[] getTestArray(int length)
        {
            var a = new string[length];
            return a;
        }
    }
}
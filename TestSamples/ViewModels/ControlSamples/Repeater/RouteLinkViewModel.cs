using System.Collections.Generic;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace TestSamples.ViewModels.ControlSamples.Repeater
{
    public class RouteLinkViewModel : DotvvmViewModelBase
    {

        public List<PageData> Pages { get; set; }

        public override Task Init()
        {
            Pages = new List<PageData>()
            {
                new PageData() { Id = 1, Name = "Humphrey" },
                new PageData() { Id = 2, Name = "Jim" },
                new PageData() { Id = 3, Name = "Bernard" }
            };

            return base.Init();
        }


        public class PageData
        {
            public string Name { get; set; }

            public int Id { get; set; }
        }
    }
}
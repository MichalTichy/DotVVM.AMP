using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace TestSamples.ViewModels.ControlSamples.Repeater
{
    public class SeparatorViewModel : DotvvmViewModelBase
    {
        public List<Card> Cards { get; set; } = new List<Card>
        {
            new Card {From = "Alaska", Sender = "Yeti"},
            new Card {From = "New Zealand ", Sender = "John"},
            new Card {From = "Minnesota", Sender = "Lou"}
         };

        public class Card
        {
            public string From { get; set; }

            public string Sender { get; set; }
        }

        public void AddItem()
        {
            Cards.Add(new Card { From = "New York", Sender = "Timmy" });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_CSharp
{
    class Card
    {
        public String Suit { get; set; }
        public String Value { get; set; }

        public Card(string suit, string value)
        {
            this.Value = value;
            this.Suit = suit;
        }

        public override string ToString()
        {
            return this.Suit.ToString() + "-" + this.Value.ToString();
        }

        public string GetValue()
        {
            return this.Value;
        }

        public string GetSuit()
        {
            return this.Suit;
        }
    }
}

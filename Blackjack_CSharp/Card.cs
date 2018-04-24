using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_CSharp
{
    class Card
    {
        public String suit { get; set; }
        public String value { get; set; }

        public Card(string suit, string value)
        {
            this.value = value;
            this.suit = suit;
        }

        override
        public string ToString()
        {
            return this.suit.ToString() + "-" + this.value.ToString();
        }

        public string GetValue()
        {
            return this.value;
        }

        public string GetSuit()
        {
            return this.suit;
        }
    }
}

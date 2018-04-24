using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_CSharp
{
    class Deck
    {
        public List<Card> cards { get; set; }
        private static string[] suits = { "CLUB", "DIAMOND", "HEART", "SPADE" };
        private static string[] values = {"ACE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "JACK", "QUEEN", "KING"};

        public Deck()
        {
            //create a new hand
            this.cards = new List<Card>();
        }

        public void CreateFullDeck()
        {
            for (int i = 0; i < 6; i++)
            {
                foreach (string suit in suits)
                {
                    foreach(string value in values)
                    {
                        this.cards.Add(new Card(suit, value));
                    }
                }
            }
        }

        public void Shuffle()
        {
            List<Card> tmpDeck = new List<Card>();
            Random random = new Random();
            int index = 0;
            int deckSize = this.DeckSize();

            for(int i = 0; i < deckSize; i++)
            {
                index = random.Next(0, this.cards.Count - 1);
                tmpDeck.Add(this.cards[index]);
                this.cards.Remove(this.cards[index]);
            }
            this.cards = tmpDeck;
        }

        public override String ToString()
        {
            String cardListOutput = "";
            foreach (Card card in this.cards)
            {
                cardListOutput += "\n" + card.ToString();
            }
            return cardListOutput;
        }   

        public void RemoveCard(int i)
        {
            this.cards.Remove(this.cards[i]);
        }

        public Card GetCard(int i)
        {
            return this.cards[i];
        }

        public void AddCard(Card card)
        {
            this.cards.Add(card);
        }

        public void Draw(Deck comingFrom)
        {
            //get the first thing in the deck
            this.cards.Add(comingFrom.GetCard(0));
            //Remove card from deck
            comingFrom.RemoveCard(0);
        }

        public int DeckSize()
        {
            return this.cards.Count;
        }

        public int CardsValue()
        {
            int totalValue = 0;
            int aces = 0;
            foreach(Card aCard in this.cards)
            {
                switch (aCard.GetValue())
                {
                    case "TWO": totalValue += 2; break;
                    case "THREE": totalValue += 3; break;
                    case "FOUR": totalValue += 4; break;
                    case "FIVE": totalValue += 5; break;
                    case "SIX": totalValue += 6; break;
                    case "SEVEN": totalValue += 7; break;
                    case "EIGHT": totalValue += 8; break;
                    case "NINE": totalValue += 9; break;
                    case "TEN": totalValue += 10; break;
                    case "JACK": totalValue += 10; break;
                    case "QUEEN": totalValue += 10; break;
                    case "KING": totalValue += 10; break;
                    case "ACE": aces += 1; break;
                    default: break;
                }
            }
            for (int i = 0; i < aces; i++)
            {
                if (totalValue > 10)
                {
                    totalValue += 1;
                }
                else
                {
                    totalValue += 11;
                }
            }
            return totalValue;
        }

        public void MoveAllToDeck(Deck moveTo)
        {
            int thisDeckSize = this.DeckSize();

            //put cards in moveTo deck
            for (int i = 0; i < thisDeckSize; i++)
            {
                moveTo.AddCard(this.GetCard(i));
            }
            for (int i = 0; i < thisDeckSize; i++)
            {
                this.RemoveCard(0);
            }
        }
    }
}

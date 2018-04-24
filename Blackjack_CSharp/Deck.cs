using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_CSharp
{
    class Deck
    {
        public List<Card> Cards { get; set; }
        public string HandWinner { get; set; }
        public bool Blackjack { get; set; }
        public bool DoubleDown { get; set; }
        public bool Split { get; set; }
        public bool SplitAces { get; set; }
        public bool NumHands { get; set; }
        public bool Bust { get; set; }
        public bool Checked { get; set; }
        public bool Stand { get; set; }
        public double Bet { get; set; }

        private static string[] suits = { "CLUB", "DIAMOND", "HEART", "SPADE" };
        private static string[] values = {"ACE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "JACK", "QUEEN", "KING"};

        public Deck()
        {
            //create a new hand
            this.Cards = new List<Card>();
            this.HandWinner = "";
            this.Blackjack = false;
            this.DoubleDown = false;
            this.Split = false;
            this.SplitAces = false;
            this.NumHands = false;
            this.Bust = false;
            this.Checked = false;
            this.Stand = false;
            this.Bet = 0.0;
            }

        public void CreateFullDeck()
        {
            for (int i = 0; i < 6; i++)
            {
                foreach (string suit in suits)
                {
                    foreach(string value in values)
                    {
                        this.Cards.Add(new Card(suit, value));
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
                index = random.Next(0, this.Cards.Count - 1);
                tmpDeck.Add(this.Cards[index]);
                this.Cards.Remove(this.Cards[index]);
            }
            this.Cards = tmpDeck;
        }

        public override String ToString()
        {
            String cardListOutput = "";
            foreach (Card card in this.Cards)
            {
                cardListOutput += "\n" + card.ToString();
            }
            return cardListOutput;
        }   

        public void RemoveCard(int i)
        {
            this.Cards.Remove(this.Cards[i]);
        }

        public bool CheckBlackjack()
        {
            bool blackjack;
            String x = this.GetCard(0).GetValue().ToString();
            String y = this.GetCard(1).GetValue().ToString();
            if (x.Equals("ACE") && (y.Equals("TEN") || y.Equals("JACK") || y.Equals("QUEEN") || y.Equals("KING")))
            {
                blackjack = true;
            }
            else if (y.Equals("ACE") && (x.Equals("TEN") || x.Equals("JACK") || x.Equals("QUEEN") || x.Equals("KING")))
            {
                blackjack = true;
            }
            else
            {
                blackjack = false;
            }
            return blackjack;
        }

        public Card GetCard(int i)
        {
            return this.Cards[i];
        }

        public void AddCard(Card card)
        {
            this.Cards.Add(card);
        }

        public void Draw(Deck comingFrom)
        {
            //get the first thing in the deck
            this.Cards.Add(comingFrom.GetCard(0));
            //Remove card from deck
            comingFrom.RemoveCard(0);
        }

        public int DeckSize()
        {
            return this.Cards.Count;
        }

        public int CardsValue()
        {
            int totalValue = 0;
            int aces = 0;
            foreach(Card aCard in this.Cards)
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

            //put Cards in moveTo deck
            for (int i = 0; i < thisDeckSize; i++)
            {
                moveTo.AddCard(this.GetCard(i));
            }
            for (int i = 0; i < thisDeckSize; i++)
            {
                this.RemoveCard(0);
            }
        }

        public void ResetDeck()
        {
            this.HandWinner = "";
            this.Blackjack = false;
            this.DoubleDown = false;
            this.Split = false;
            this.SplitAces = false;
            this.NumHands = false;
            this.Bust = false;
            this.Checked = false;
            this.Stand = false;
            this.Bet = 0.0;
        }
    }
}

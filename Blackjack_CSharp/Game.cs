using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_CSharp
{
    class Game
    {
        public static void PlayGame()
        {

            //Create our playing deck
            Deck mainDeck = new Deck();
            mainDeck.CreateFullDeck();
            mainDeck.Shuffle();

            //Create a deck for player.
            Deck playerDeck = new Deck();
            Deck dealerDeck = new Deck();

            double playerMoney = 100.00;

            Console.WriteLine("Welcome to the game!");

            //Game Loop
            //Everytime player has a turn -run loop once
            while (playerMoney > 0)
            {
                //play on!
                //Take the players bet
                Console.WriteLine("You have $" + playerMoney + ". How much would you like to bet?");
                double playerBet = double.Parse(Console.ReadLine());
                if (playerBet > playerMoney)
                {
                    Console.WriteLine("You cannot bet more money than you currently have!");
                    break;
                }

                bool endRound = false;
                //Start dealing
                //player and dealer get two cards each
                playerDeck.Draw(mainDeck);
                dealerDeck.Draw(mainDeck);
                playerDeck.Draw(mainDeck);
                dealerDeck.Draw(mainDeck);

                while (true)
                {
                    Console.WriteLine("Your hand: ");
                    Console.WriteLine(playerDeck.ToString());
                    Console.WriteLine("Your deck is value at: " + playerDeck.CardsValue());

                    //Display dealer hand
                    Console.WriteLine("Delaer hand: " + dealerDeck.GetCard(0).ToString() + " and [hidden]");

                    while (true)
                    {
                        //What does the player want to do?
                        string message = "Would you like to (1) hit or (2) stand?";
                        Console.WriteLine(message);

                        int response = int.Parse(Console.ReadLine());
                        if (response == 1)
                        {
                            playerDeck.Draw(mainDeck);
                            Console.WriteLine("You draw a: " + playerDeck.GetCard(playerDeck.DeckSize() - 1).ToString());
                            //bust if over 21
                            if (playerDeck.CardsValue() > 21)
                            {
                                Console.WriteLine("Bust. Currently valued at: " + playerDeck.CardsValue());
                                playerMoney -= playerBet;
                                endRound = true;
                                break;
                            }
                        }
                        else if (response == 2)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Response. Please try again!");
                            Console.WriteLine(message);
                        }
                    }
                    break;
                }

                //reveal dealer
                Console.WriteLine("Dealer cards " + dealerDeck.ToString());
                //see if dealer has more points than player
                if ((dealerDeck.CardsValue() > playerDeck.CardsValue()) && endRound == false)
                {
                    Console.WriteLine("Delaer beats you!");
                    playerMoney -= playerBet;
                    endRound = true;
                }
                //Dealer draws at 16. Stand at 17.
                while ((dealerDeck.CardsValue() < 17) && endRound == false)
                {
                    dealerDeck.Draw(mainDeck);
                    Console.WriteLine("Dealer Draws: " + dealerDeck.GetCard(dealerDeck.DeckSize() - 1).ToString());
                }
                //Display total value for dealer
                Console.WriteLine("Dealers hand is valued at: " + dealerDeck.CardsValue());
                //Determine if dealer busted
                if ((dealerDeck.CardsValue() > 21) && endRound == false)
                {
                    Console.WriteLine("You win");
                    playerMoney += playerBet;
                    endRound = true;
                }
                //Determin if push
                if ((playerDeck.CardsValue() == dealerDeck.CardsValue()) && endRound == false)
                {
                    Console.WriteLine("Push");
                    endRound = true;
                }
                else if ((playerDeck.CardsValue() > dealerDeck.CardsValue()) && endRound == false)
                {
                    Console.WriteLine("Player won! You win the hand.");
                    playerMoney += playerBet;
                    endRound = true;
                }
                else if (endRound == false)
                {
                    Console.WriteLine("You lose.");
                    playerMoney -= playerBet;
                    endRound = true;
                }

                playerDeck.MoveAllToDeck(mainDeck);
                dealerDeck.MoveAllToDeck(mainDeck);
                Console.WriteLine("End of hand.");
            }

            Console.WriteLine("Game over. You are out of money. Thanks for playing!");


        }

    }
}

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

            List<Deck> DeckList = new List<Deck>();
            DeckList.Add(mainDeck);
            
            double playerMoney = 100.00;

            List<double> MoneyList = new List<double>();
            MoneyList.Add(playerMoney);

            Console.WriteLine("Welcome to the game!");

            //Game Loop
            //Everytime player has a turn -run loop once
            while (playerMoney > 0)
            {
                //play on!
                //Take the players bet
                double playerBet = GetBet(playerMoney);
                MoneyList.Add(playerBet);
                bool endRound = false;
                
                //Start dealing
                //player and dealer get two cards each
                playerDeck.Draw(mainDeck);
                dealerDeck.Draw(mainDeck);
                playerDeck.Draw(mainDeck);
                dealerDeck.Draw(mainDeck);

                playerDeck.Blackjack = playerDeck.CheckBlackjack();
                dealerDeck.Blackjack = playerDeck.CheckBlackjack();

                DeckList.Add(playerDeck);
                DeckList.Add(dealerDeck);

                PlayerTurn(DeckList, MoneyList);
                //DealerTurn(DeckList, MoneyList);

                playerDeck = DeckList[1];
                dealerDeck = DeckList[DeckList.Count - 1];

                //reveal dealer
                Console.WriteLine("Dealer cards " + dealerDeck.ToString());
                if (playerDeck.Bust == true)
                {
                    Console.WriteLine("Bust. Currently valued at: " + playerDeck.CardsValue());
                }
                else
                {
                    //Dealer draws at 16. Stand at 17.
                    while ((dealerDeck.CardsValue() < 17) && endRound == false)
                    {
                        dealerDeck.Draw(mainDeck);
                        Console.WriteLine("Dealer Draws: " + dealerDeck.GetCard(dealerDeck.DeckSize() - 1).ToString());
                    }
                    //Display total value for dealer
                    Console.WriteLine("Dealers hand is valued at: " + dealerDeck.CardsValue());
                    //Determine if dealer busted
                    if ((dealerDeck.CardsValue() > 21) && playerDeck.Blackjack != true && endRound == false)
                    {
                        Console.WriteLine("You win");
                        playerMoney += playerBet;
                        endRound = true;
                    }
                }

                //Determine if push
                if ((playerDeck.CardsValue() == dealerDeck.CardsValue()) && endRound == false)
                {
                    Console.WriteLine("Push");
                    endRound = true;
                }
                else if ((playerDeck.CardsValue() > dealerDeck.CardsValue()) && endRound == false && playerDeck.Bust != true)
                {
                    Console.WriteLine("Player won! You win the hand.");
                    playerMoney += playerBet;
                    endRound = true;
                }
                else
                {
                    if (playerDeck.Bust != true) {
                        Console.WriteLine("You lose.");
                    }
                    playerMoney -= playerBet;
                    endRound = true;
                }

                //reset any properties before the next hand
                playerDeck.ResetDeck();
                dealerDeck.ResetDeck();

                playerDeck.MoveAllToDeck(mainDeck);
                dealerDeck.MoveAllToDeck(mainDeck);
                
                Console.WriteLine("End of hand.");
            }
            Console.WriteLine("Game over. You are out of money. Thanks for playing!");
        }

        static void PlayerTurn(List<Deck> dList, List<double> mList) 
        {
            Deck mainDeck = dList[0];
            //for loop for splitting hands goes here
            Deck playerDeck = dList[1];
            Deck dealerDeck = dList[dList.Count - 1];

            double playerMoney = mList[0];
            playerDeck.Bet = mList[1];
            while (true)
            {
                Console.WriteLine("Your hand: ");
                Console.WriteLine(playerDeck.ToString());
                Console.WriteLine("Your deck is valued at: " + playerDeck.CardsValue());

                //Display dealer hand
                Console.WriteLine("Dealer hand: " + dealerDeck.GetCard(0).ToString() + " and [hidden]");
                dList.Insert(dList.Count - 1, dealerDeck);

                //blackjack check
                if (playerDeck.Blackjack == true || dealerDeck.Blackjack == true)
                {
                    if (playerDeck.Blackjack && dealerDeck.Blackjack)
                    {
                        playerDeck.HandWinner = "Push";
                        break;
                    }
                    else if (playerDeck.Blackjack == true)
                    {
                        playerDeck.HandWinner = "BLACKJACK!";
                        Console.WriteLine("BLACKJACK!!!");
                        playerMoney = HandleMoney(playerDeck.HandWinner, playerMoney, playerDeck.Bet);
                        break;
                    }
                    else
                    {
                        playerDeck.HandWinner = "dealer";
                        Console.WriteLine("Dealer Blackjack!");
                        playerMoney = HandleMoney(playerDeck.HandWinner, playerMoney, playerDeck.Bet);
                        break;
                    }
                }
                else
                {
                    //no blackjack
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
                            Console.WriteLine("Your deck is valued at: " + playerDeck.CardsValue());
                            //bust if over 21
                            if (playerDeck.CardsValue() > 21)
                            {
                                mList.Insert(0, playerMoney - playerDeck.Bet);
                                dList.Insert(0, mainDeck);
                                playerDeck.Bust = true;
                                dList.Insert(1, playerDeck);
                                break;
                            }
                        }
                        else if (response == 2)
                        {
                            dList.Insert(0, mainDeck);
                            dList.Insert(1, playerDeck);
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
            }
            //end for loop for splitting hands
        }

        static double GetBet(double playerMoney)
        {
            double playerBet = 0.0;
            while (true) {
                Console.WriteLine("You have $" + playerMoney + ". How much would you like to bet?");
                playerBet = double.Parse(Console.ReadLine());
                if (playerBet.GetType() != typeof(double))
                {
                    Console.WriteLine("Please enter a number!");
                } else if (playerBet < 1.0)
                {
                    Console.WriteLine("You must bet at least $1");
                } else if (playerBet > playerMoney)
                {
                    Console.WriteLine("You cannot bet more money than you currently have!");
                } else
                {
                    return playerBet;
                }
            }
        }

        static double HandleMoney(String win, double pM, double pB)
        {
            switch (win)
            {
                case "BLACKJACK!":
                    pM += pB * 3 / 2;
                    break;
                case "push":
                    Console.WriteLine("Push!");
                    break;
                case "player":
                    Console.WriteLine("Winner!");
                    pM += pB;
                    break;
                case "dealer":
                    Console.WriteLine("You lost!");
                    pM -= pB;
                    break;
                default:
                    break;
            }
            return pM;
        }
    }
}

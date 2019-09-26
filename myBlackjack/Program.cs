using System;

namespace Blackjack
{
    class Program
    {
        static int playerCards = 0;
        static int dealerCards = 0;
        static bool gameOver = false;
        static Random randomInt = new Random();
        static bool playerNotDone = true;

        static void Main(string[] args)
        {
            intializeGame();
            while (gameOver == false)
            {
                playerDeal();
            }
            Console.ReadKey();
        }
        static void intializeGame()
        {
            for (int i = 0; i < 2; i++)
            {
                playerCards += randomInt.Next(1, 10);

            }
            Console.WriteLine(playerCards);
        }
        static void playerDeal()
        {
            while (playerNotDone == true) {
                Console.Write("Vill du ta ett nytt kort? (Ja)");
                string prompt = Console.ReadLine();
                if(prompt.ToLower() == "ja")
                {
                    int newCard = randomInt.Next(1, 10);
                    playerCards += newCard;
                    Console.WriteLine("You pulled a {0}, you now have {1}", newCard, playerCards);
                    if(playerCards > 21)
                    {
                        gameOver = true;
                        Console.WriteLine("You lost, sorry!");
                        break;
                    }
                }
                else
                {
                    playerNotDone = true;
                    dealerDeal();
                }
            }


        }

        private static void dealerDeal()
        {
            bool playerNotDone = true;
            while (playerNotDone == true)
            {
                if (dealerCards == playerCards)
                {
                    Console.WriteLine("Tie!");
                    playerNotDone = false;
                }
                if (dealerCards == 21 && playerCards != 21)
                {
                    Console.WriteLine("Blackjack! Dealer won");
                }
                else if (dealerCards > 21)
                {
                    Console.WriteLine("You won!");
                    playerNotDone = false;
                }
                else if (dealerCards < playerCards)
                {
                    int newCard = randomInt.Next(1, 10);
                    dealerCards += newCard;
                    Console.WriteLine("Dealer pulls a {0}, they now have {1}", newCard, dealerCards);

                }
                else if (dealerCards > playerCards)
                {
                    Console.WriteLine("Dealer won!");
                    playerNotDone = false;
                }
                else
                {

                }
                gameOver = true;
            }
            }
    }
}


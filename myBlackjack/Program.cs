using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Program
    {
        List<Player> players = new List<Player>();
        static int dealerCards = 0;
        static Random randomInt = new Random();
        static bool playerNotDone = true;
        static bool dealerNotDone = true;
        static bool gameOver = false;

        static void Main(string[] args)
        {
            intializeGame();
            playerDeal();
            if (gameOver != true)
            {
                dealerDeal();
            }
            Console.ReadKey();
        }
        static void intializeGame()
        {
            while (playerNotDone == true)
            {
                Console.WriteLine("Lägg till spelare?");
                Console.WriteLine("1: Ja");
                Console.WriteLine("2: Nej");
                Console.Write("");
                int choice = int.Parse(Console.ReadLine());
                switch(choice)
                {
                    case 1:
                        Console.Write("Name: ");
                        string name = Console.ReadLine();
                        Player player = new Player();
                        player.Name = name;
                        players.Add(player);
                        break;
                    case 2:
                        break;
                }
            }
            foreach(Player p in players) {
                for (int i = 0; i < 2; i++)
                {
                    p.playerCards += randomInt.Next(1, 10);

                }
            }
            Console.WriteLine(playerCards);
        }
        static void playerDeal()
        {
            foreach(Player p in players) {
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
                        playerNotDone = false;
                        gameOver = true;
                        Console.WriteLine("You lost, sorry!");
                    }
                }
                else
                {
                    playerNotDone = true;
                    break;
                }
}

            }
        }

        private static void dealerDeal()
        {
            int highestNum = 0;
            foreach(Player p in players) 
            {
                int highestNum = p.Max();
            }
            foreach(Player p in players) {
            dealerNotDone = true;
            while (dealerNotDone == true)
            {
                if (dealerCards == p.playerCards)
                {
                    Console.WriteLine("Tie!");
                    dealerNotDone = false;
                }
                else if (dealerCards == 21 && p.playerCards != 21)
                {
                    Console.WriteLine("Blackjack! Dealer won");
                }
                else if (dealerCards > 21)
                {
                    Console.WriteLine("You won!");
                    dealerNotDone = false;
                }
                else if (dealerCards < p.playerCards)
                {
                    int newCard = randomInt.Next(1, 10);
                    dealerCards += newCard;
                    Console.WriteLine("Dealer pulls a {0}, they now have {1}", newCard, dealerCards);

                }
                else if (dealerCards > p.playerCards)
                {
                    Console.WriteLine("Dealer won!");
                    dealerNotDone = false;
                }
            }
            }
        }
    }
}


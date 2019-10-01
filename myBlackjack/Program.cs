using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Program
    {
        static List<Player> players = new List<Player>();
        static Random randomInt = new Random();
        static int dealerCards = 0;
        static int maxNum = 0;
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
                switch (choice)
                {
                    case 1:
                        Console.Write("Name: ");
                        string name = Console.ReadLine();
                        Player currentPlayer = new Player();
                        currentPlayer.Name = name;
                        players.Add(currentPlayer);
                        break;
                    case 2:
                        playerNotDone = false;
                        break;
                }
            }

            for (int i = 0; i < 2; i++)
            {
                dealerCards += randomInt.Next(1, 10);

            }
            Console.WriteLine("");
            Console.WriteLine("Datorns poäng: {0}", dealerCards);
            foreach (Player p in players)
            {
                for (int i = 0; i < 2; i++)
                {
                    p.playerCards += randomInt.Next(1, 11);

                }
                Console.WriteLine("{0} Din poäng: {1}", p.Name, p.playerCards);
            }
            Console.WriteLine("");

        }
        static void playerDeal()
        {
            int i = 0;
            foreach (Player p in players)
            {
                Console.WriteLine("Playing as player {0}", p.Name);
                playerNotDone = true;
                while (playerNotDone == true)
                {
                    Console.Write("Vill du ta ett nytt kort? (Ja)");
                    string prompt = Console.ReadLine();
                    if (prompt.ToLower() == "ja")
                    {

                        int newCard = randomInt.Next(1, 11);
                        p.playerCards += newCard;

                        Console.WriteLine("Du drog en {0}, du har nu {1} poäng", newCard, p.playerCards);
                        Console.WriteLine("Datorns poäng: {0}", dealerCards);
                        if (p.playerCards > 21)
                        {
                            p.playerCards = -1;
                            Console.WriteLine("You lost, sorry!");
                            playerNotDone = false;
                        }
                    }
                    else
                    {
                        playerNotDone = false;
                        break;
                    }
                }
                i++;
            }
            maxNum = players.Max(player => player.playerCards);
        }

        private static void dealerDeal()
        {
            dealerNotDone = true;
            while (dealerNotDone == true)
            {
                if (dealerCards == maxNum)
                {
                    Console.WriteLine("Tie!");
                    dealerNotDone = false;
                }
                else if (dealerCards == 21 && maxNum != 21)
                {
                    Console.WriteLine("Blackjack! Dealer won");
                }
                else if (dealerCards > 21)
                {
                    Console.WriteLine("You won!");
                    dealerNotDone = false;
                }
                else if (dealerCards < maxNum)
                {
                    int newCard = randomInt.Next(1, 11);
                    dealerCards += newCard;
                    Console.WriteLine("Dealer pulls a {0}, they now have {1}", newCard, dealerCards);

                }
                else if (dealerCards > maxNum)
                {
                    Console.WriteLine("Dealer won!");
                    dealerNotDone = false;
                }
            }
        }
    }
}


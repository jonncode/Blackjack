using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Program
    {
        static List<Player> players = new List<Player>(); //List of all players in game except dealer
        static Random randomInt = new Random();
        static int dealerCards = 0; //Sum of all of dealer's cards 
        static int maxNum = 0; // Highest number out of all players not over 21.
        static int selectedMenuInput = 0; //input for switch case.
        static bool playerNotDone = true;
        static bool dealerNotDone = true;
        static bool gameOver = false;
        static bool exitGame = false;


        static void Main(string[] args)
        {
            while(exitGame == false)
            {
                GameMenu();
                IntializeGame();
                playerDeal();
                if (gameOver != true)
                {
                    dealerDeal();
                }
                Console.ReadKey();
            }
        }
        /// <summary>
        /// Lets main user select if they want to play or quit the game.
        /// </summary>
        static void GameMenu() 
        {
            Console.WriteLine("Välkommen till 21:an!");
            Console.WriteLine("Välj ett alternativ");
            Console.WriteLine("1. Spela 21:an");
            Console.WriteLine("2. Avsluta spelet");
            Console.Write("Skriv ditt alternativ: ");
            bool choice = int.TryParse(Console.ReadLine(), out selectedMenuInput);
            while(!choice)
            {
                Console.WriteLine("Felaktigt svar!");
                Console.Write("Skriv ditt alternativ: ");
                choice = int.TryParse(Console.ReadLine(), out selectedMenuInput);
            }
            switch(selectedMenuInput)
            {
                case 1:
                    break;
                case 2:
                    exitGame = true;
                    break;
            }  
        }
        /// <summary>
        /// Asks users to create new players until they feel satisfied, breaks out of loop.
        /// Gives each player 2 random rands including the dealer
        /// Prints every user's amount of cards.
        /// </summary>
        static void IntializeGame()
        {
            while (playerNotDone == true)
            {
                Console.WriteLine("");
                Console.WriteLine("Lägg till spelare?");
                Console.WriteLine("1: Ja");
                Console.WriteLine("2: Nej");
                Console.Write("Skriv ditt alternativ: ");
                bool choice = int.TryParse(Console.ReadLine(), out selectedMenuInput);
                while (!choice)
                {
                    Console.WriteLine("Felaktigt svar!");
                    Console.Write("Skriv ditt alternativ: ");
                    choice = int.TryParse(Console.ReadLine(), out selectedMenuInput);
                }
                switch (selectedMenuInput)
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
                    default:
                        Console.WriteLine("Felaktigt svar, försök igen!");
                        break;
                }
            }

            for (int i = 0; i < 2; i++)
            {
                dealerCards += randomInt.Next(1, 11);

            }
            Console.WriteLine("");
            Console.WriteLine("Datorns poäng: {0}", dealerCards);
            foreach (Player p in players)
            {
                for (int i = 0; i < 2; i++)
                {
                    p.playerCards += randomInt.Next(1, 11);

                }
                Console.WriteLine("[{0}] Poäng: {1}", p.Name, p.playerCards);
            }

        }
        /// <summary>
        /// Goes through list of players one by one, deals new cards until player is satisfied
        /// Player loses if sum of all cards goes above 21.
        /// When all players are satisfied, end game with function dealerDeal()
        /// </summary>
        static void playerDeal()
        {
            int i = 0;
            foreach (Player p in players)
            {
                Console.WriteLine("");
                Console.WriteLine("Playing as player {0}", p.Name);
                playerNotDone = true;
                while (playerNotDone == true)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Vill du ta ett nytt kort?");
                    Console.WriteLine("1: Ja");
                    Console.WriteLine("2: Nej");
                    Console.Write("Skriv ditt alternativ: ");
                    bool choice = int.TryParse(Console.ReadLine(), out selectedMenuInput);
                    while(!choice)
                    {
                        Console.WriteLine("Felaktigt svar!");
                        Console.Write("Skriv ditt alternativ: ");
                        choice = int.TryParse(Console.ReadLine(), out selectedMenuInput);
                    }
                    switch(selectedMenuInput)
                    {
                        case 1:
                            int newCard = randomInt.Next(1, 11);
                            p.playerCards += newCard;
                            Console.WriteLine("Du drog en {0}, du har nu {1} poäng", newCard, p.playerCards);
                            Console.WriteLine("Datorns poäng: {0}", dealerCards);
                            if (p.playerCards > 21)
                            {
                                p.playerCards = -1;
                                Console.WriteLine("You lost, sorry!");
                                Console.WriteLine("");
                                playerNotDone = false;
                            }
                            break;
                        case 2:
                            playerNotDone = false;
                            break;
                    }
                }
                i++;
            }
            maxNum = players.Max(player => player.playerCards);
        }
        /// <summary>
        /// Dealer tries to surpass/tie with player that had highest number not over 21.
        /// If dealer is above every remaining player AND not over 21, dealer wins.
        /// If there are no players, dealer wins.
        /// If dealer goes over 21, remaining players win.
        /// If dealer is still under highest player not over 21, dealer draws a new card.
        /// </summary>
        private static void dealerDeal()
        {
            dealerNotDone = true;
            while (dealerNotDone == true)
            {
                Console.WriteLine("");
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
                    Console.WriteLine("Remaining players win!");
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


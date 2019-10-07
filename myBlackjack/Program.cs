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
        static bool playerNotDone = true; //If current player is done playing (during menu or in game)
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
                bool choice = int.TryParse(Console.ReadLine(), out selectedMenuInput); //Save input to variable selectedMenuInput
                while (!choice) //if tryParse fails.
                {
                    //Continue asking for input
                    Console.WriteLine("Felaktigt svar!");
                    Console.Write("Skriv ditt alternativ: ");
                    choice = int.TryParse(Console.ReadLine(), out selectedMenuInput);
                }
                switch (selectedMenuInput)
                {
                    case 1:
                        Console.Write("Name: ");
                        string name = Console.ReadLine();
                        Player currentPlayer = new Player(); //Initialize instance player using construction Player();
                        currentPlayer.Name = name; //Add name to instance
                        players.Add(currentPlayer); //Add instance to list of player objects.
                        break;
                    case 2:
                        playerNotDone = false; //Players are ready.
                        break;
                    default:
                        Console.WriteLine("Felaktigt svar, försök igen!");
                        break;
                }
            }
            //Add two cards to dealer.
            for (int i = 0; i < 2; i++)
            {
                dealerCards += randomInt.Next(1, 11);

            }
            Console.WriteLine("");
            Console.WriteLine("Datorns poäng: {0}", dealerCards); //Display dealer's sum from cards.
            foreach (Player p in players) //Add 2 cards to every player object in list.
            {
                for (int i = 0; i < 2; i++)
                {
                    p.playerCards += randomInt.Next(1, 11);

                }
                Console.WriteLine("[{0}] Poäng: {1}", p.Name, p.playerCards); //Output each player's total sum from cards.
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
            foreach (Player p in players) //Loop through list of players.
            {
                Console.WriteLine("");
                Console.WriteLine("Playing as player {0}", p.Name);
                playerNotDone = true;
                while (playerNotDone == true) //While player is not done.
                {
                    Console.WriteLine("");
                    Console.WriteLine("Vill du ta ett nytt kort?");
                    Console.WriteLine("1: Ja");
                    Console.WriteLine("2: Nej");
                    Console.Write("Skriv ditt alternativ: ");
                    bool choice = int.TryParse(Console.ReadLine(), out selectedMenuInput); //Save input into int selectedMenuInput
                    while(!choice) //If tryParse fails
                    {
                        Console.WriteLine("Felaktigt svar!");
                        Console.Write("Skriv ditt alternativ: ");
                        choice = int.TryParse(Console.ReadLine(), out selectedMenuInput);
                    }
                    switch(selectedMenuInput)
                    {
                        case 1:
                            int newCard = randomInt.Next(1, 11); //generate new card to player.
                            p.playerCards += newCard; //Add card to player.
                            Console.WriteLine("Du drog en {0}, du har nu {1} poäng", newCard, p.playerCards); //output total points for player.
                            Console.WriteLine("Datorns poäng: {0}", dealerCards); 
                            if (p.playerCards > 21) //If player exceeds over 21, automatically lose and reset int for maxNum.
                            {
                                p.playerCards = -1; //Set player cards to -1 to avoid interference with maxNum.
                                Console.WriteLine("You lost, sorry!");
                                playerNotDone = false; //Player is done
                            }
                            break;
                        case 2:
                            playerNotDone = false;
                            break;
                    }
                }
                i++;
            }
            //Player in list with highest sum of number from cards
            //Players who have lost have negative int, thus won't interfere with dealer's decisions.
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
            while (dealerNotDone == true) //While player has not won/lost.
            {
                Console.WriteLine("");
                if (dealerCards == maxNum) //If dealer's number is the same as highest number.
                {
                    Console.WriteLine("Tie!");
                    dealerNotDone = false;
                }
                else if (dealerCards == 21 && maxNum != 21) //If dealer gets blackjack meanwhile nobody else has blackjack.
                {
                    Console.WriteLine("Blackjack! Dealer won");
                }
                else if (dealerCards > 21) //If dealer goes over 21.
                {
                    Console.WriteLine("Remaining players win!");
                    dealerNotDone = false;
                }
                else if (dealerCards < maxNum) //If dealer is still under maxNum.
                {
                    int newCard = randomInt.Next(1, 11); //Deal new card to dealer.
                    dealerCards += newCard; //Add card to dealer.
                    Console.WriteLine("Dealer pulls a {0}, they now have {1}", newCard, dealerCards);

                }
                else if (dealerCards > maxNum) //IF dealer's cards is over maxNum.
                {
                    Console.WriteLine("Dealer won!");
                    dealerNotDone = false;
                }
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.WriteLine("");
        }
    }
}


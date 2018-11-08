using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoingToBostonDiceGame
{

    public class MainClass
    {
        public static void Main(string[] args)
        {
            Random rand = new Random();

            Player player1 = new Player();
            Player player2 = new Player();

            Dice dice1 = new Dice();
            Dice dice2 = new Dice();
            Dice dice3 = new Dice();

            player1.PlayerNames();

            Game modeSelecter = new Game();
            Game gamestyle = new Game();

            Game.GameModeSelection();
            Game.MatchPlay();
            Game.ScorePlay();
            Game.ResetGame();
        }
    }

    // DICE CLASS THAT SETS THE RANDOM NUMBER BOUNDARY AND STORING AND OUTPUTTING THE VALUE
    public class Dice
    {
        int diceValue;

        public int getValue()
        {
            return diceValue;
        }

        public void setValue(int value)
        {
            diceValue = value;
        }

        public void diceRoll(Random rand)
        {
            diceValue = rand.Next(0, 7);
        }
    }

    // PLAYER CLASS START
    public class Player // PLAYER CLASS WHERE NAMES AND FUNCTIONS ARE CREATED
    {
        public static string[] playerNames = new string[2]; // STRING ARRAY TO HOLD THE PLAYERS NAMES

        public void PlayerNames() // ASK FOR PLAYERS NAMES
        {
            Console.WriteLine("Player 1, What is your name? ");
            playerNames[0] = Console.ReadLine(); // PLAYER 1 NAME

            Console.WriteLine();

            Console.WriteLine("Player 2, What is your name? ");
            playerNames[1] = Console.ReadLine(); // PLAYER 2 NAME
        }

        // GREETS THE PLAYERS WHEN THEY ENTERED THEIR NAMES
        public static void PlayerGreeting()
        {
            Console.WriteLine("\nWelcome {0} and {1}!\n", playerNames[0], playerNames[1]);
        }
    }
    // PLAYER CLASS END

    // GAME CLASS START
    public class Game // WHERE THE GAME MODES ARE CREATED AND HOW THE PLAYERS SELECT WHAT GAME MODE THEY WANT
    {
        public static bool match;
        public static bool score;

        public static int[] p1Scores = new int[5];
        public static int[] p2Scores = new int[5];

        public static void GameModeSelection()
        {
            Player.PlayerGreeting();

            Console.WriteLine("What game mode would you like to play?\n\nType 'match' for Match Play or choose 'score' for Score Play. For more inormation of the game modes, type 'info' \n");

                do
                {
                    string gameModeSelecter = Console.ReadLine();

                    if (gameModeSelecter == "score")
                    {
                        Console.WriteLine("\nYou have selected Score Play. Press Enter to begin. Good Luck!");
                        score = true;
                        Console.ReadLine();
                        break;
                    }
                    if (gameModeSelecter == "match")
                    {
                        Console.WriteLine("\nYou have selected Match Play. Press Enter to begin. Good luck!");
                        match = true;
                        Console.ReadLine();
                        break;
                    }
                    if (gameModeSelecter == "info")
                    {
                        Console.WriteLine("\nMatch Play is when a player will earn a point after each round if their score is the highest. The first to 5 points wins the game!\n");
                        Console.WriteLine("Score Play is when the total score of the players dice is added together after each round. After the fifth round the player with the highest score overall wins the game!\n");
                    }
                    else
                    {
                        Console.WriteLine("\nSorry, that was incorrect. Try again\n");
                    }
            } while (match || score == false);
        }

        public static void ResetGame()
        {
            GameModeSelection();
        }

        public static void MatchPlay()
        {
            Random rand = new Random();
            Dice dice1 = new Dice();
            Dice dice2 = new Dice();
            Dice dice3 = new Dice();

            // ROUNDS PLAYERS WON
            int player1Points = 0;
            int player2Points = 0;

            // SCORES FOR THE ROUNDS
            int player1Total;
            int player2Total;

            if (match == true)
            {
                while ((player1Points < 5) || (player2Points < 5))
                {
                    Console.WriteLine("---------- Match Game ----------\n");

                    Console.WriteLine("{0}, it is your roll! (Press Enter)\n", Player.playerNames[0]);
                    dice1.diceRoll(rand);
                    dice2.diceRoll(rand);
                    dice3.diceRoll(rand);
                    player1Total = Math.Max(Math.Max(dice1.getValue(), dice2.getValue()), dice3.getValue()); // FINDS THE HIGHEST ROLL OF THE DICE
                    Console.ReadLine();
                    Console.WriteLine("{0}, your highest number was: {1}.\n", Player.playerNames[0], player1Total); // DISPLAYS THE PLAYERS HIGHEST ROLL
                    Console.ReadLine();

                    Console.WriteLine("{0}, it is your roll! (Press Enter)\n", Player.playerNames[1]);
                    dice1.diceRoll(rand);
                    dice2.diceRoll(rand);
                    dice3.diceRoll(rand);
                    player2Total = Math.Max(Math.Max(dice1.getValue(), dice2.getValue()), dice3.getValue());
                    Console.ReadLine();
                    Console.WriteLine("{0}, your highest number was: {1}.\n", Player.playerNames[1], player2Total);
                    Console.ReadLine();

                    // ANNOUNCE WHO GOT THE HIGHEST ROLL AND DECLARE A POINT TO THE PLAYER
                    if (player1Total > player2Total)
                    {
                        player1Points++;
                        Console.WriteLine("{0}, you have the largest number, you earn 1 point.\t\t{0}: {2} || : {3}", Player.playerNames[0],
                                          Player.playerNames[1], player1Points, player2Points);
                        
                        Console.ReadLine();
                    }
                    if (player2Total > player1Total)
                    {
                        player2Points++;
                        Console.WriteLine("{0}, you have the largest number, you earn 1 point.\t\t{1}: {2} || {0}: {3}", Player.playerNames[1],
                                          Player.playerNames[0], player1Points, player2Points);
                        
                        Console.ReadLine();
                    }

                    // IF PLAYER 1 AND 2'S TOTAL ARE THE SAME, DRAW ROUND AND NO POINTS AWARDED
                    else if (player1Total == player2Total)
                    {
                        Console.WriteLine("Both players got the same number, no points awarded\n");
                        Console.ReadLine();
                    }

                    // ONCE A PLAYER REACHES THE SCORE LIMIT, ANNOUNCE THEY HAVE WON AND GO BACK TO GAME SELECTION
                    if (player1Points == 5)
                    {
                        Console.WriteLine("Congratulations {0}, you have won the game! Press enter to return to the game selections", Player.playerNames[0]);
                        Console.ReadLine();
                        break;
                    }
                    if (player2Points == 5)
                    {
                        Console.WriteLine("Congratulations {0}, you have won the game! Press enter to return to the game selections", Player.playerNames[1]);
                        Console.ReadLine();
                        break;
                    }
                }

                // RETURNS TO THE GAME SELECTION
                if ((player1Points == 5) || (player2Points == 5))
                {
                    ResetGame();
                }
            }
        }

        public static void ScorePlay()
        {
            Random rand = new Random();
            Dice dice1 = new Dice();
            Dice dice2 = new Dice();
            Dice dice3 = new Dice();

            int roundsPlayed = 0; // KEEPS TRACK OF THE AMOUNT OF ROUNDS PLAYED OUT OF 5

            if (score == true)
            {
                Console.WriteLine("---------- Score Game ----------\n\n");

                if (roundsPlayed == 0)
                {
                    Console.WriteLine("---------- Round 1 ----------\n");

                    Console.WriteLine("{0}, it is your roll! (Press Enter)", Player.playerNames[0]);
                    Console.ReadLine();
                    dice1.diceRoll(rand);
                    dice2.diceRoll(rand);
                    dice3.diceRoll(rand);
                    p1Scores[0] = dice1.getValue() + dice2.getValue() + dice3.getValue();
                    Console.WriteLine(p1Scores[0]);
                    Console.ReadLine();

                    Console.WriteLine("{0}, it is your roll! (Press Enter)", Player.playerNames[1]);
                    Console.ReadLine();
                    dice1.diceRoll(rand);
                    dice2.diceRoll(rand);
                    dice3.diceRoll(rand);
                    p2Scores[0] = dice1.getValue() + dice2.getValue() + dice3.getValue();
                    Console.WriteLine(p2Scores[0]);
                    Console.ReadLine();

                    roundsPlayed++;  
                }

                if (roundsPlayed == 1)
                {
                    Console.WriteLine("---------- Round 2 ----------\n");

                    Console.WriteLine("{0}, it is your roll! (Press Enter)", Player.playerNames[0]);
                    Console.ReadLine();
                    dice1.diceRoll(rand);
                    dice2.diceRoll(rand);
                    dice3.diceRoll(rand);
                    p1Scores[1] = dice1.getValue() + dice2.getValue() + dice3.getValue();
                    Console.WriteLine(p1Scores[1]);
                    Console.ReadLine();

                    Console.WriteLine("{0}, it is your roll! (Press Enter)", Player.playerNames[1]);
                    Console.ReadLine();
                    dice1.diceRoll(rand);
                    dice2.diceRoll(rand);
                    dice3.diceRoll(rand);
                    p2Scores[1] = dice1.getValue() + dice2.getValue() + dice3.getValue();
                    Console.WriteLine(p2Scores[1]);
                    Console.ReadLine();

                    roundsPlayed++; 
                }

                if (roundsPlayed == 2)
                {
                    Console.WriteLine("--------- Round 3 ----------\n");

                    Console.WriteLine("{0}, it is your roll! (Press Enter)", Player.playerNames[0]);
                    Console.ReadLine();
                    dice1.diceRoll(rand);
                    dice2.diceRoll(rand);
                    dice3.diceRoll(rand);
                    p1Scores[2] = dice1.getValue() + dice2.getValue() + dice3.getValue();
                    Console.WriteLine(p1Scores[2]);
                    Console.ReadLine();

                    Console.WriteLine("{0}, it is your roll! (Press Enter)", Player.playerNames[1]);
                    Console.ReadLine();
                    dice1.diceRoll(rand);
                    dice2.diceRoll(rand);
                    dice3.diceRoll(rand);
                    p2Scores[2] = dice1.getValue() + dice2.getValue() + dice3.getValue();
                    Console.WriteLine(p2Scores[2]);
                    Console.ReadLine();

                    roundsPlayed++; 
                }

                if (roundsPlayed == 3)
                {
                    Console.WriteLine("---------- Round 4 ----------\n");

                    Console.WriteLine("{0}, it is your roll! (Press Enter)", Player.playerNames[0]);
                    Console.ReadLine();
                    dice1.diceRoll(rand);
                    dice2.diceRoll(rand);
                    dice3.diceRoll(rand);
                    p1Scores[3] = dice1.getValue() + dice2.getValue() + dice3.getValue();
                    Console.WriteLine(p1Scores[3]);
                    Console.ReadLine();

                    Console.WriteLine("{0}, it is your roll! (Press Enter)", Player.playerNames[1]);
                    Console.ReadLine();
                    dice1.diceRoll(rand);
                    dice2.diceRoll(rand);
                    dice3.diceRoll(rand);
                    p2Scores[3] = dice1.getValue() + dice2.getValue() + dice3.getValue();
                    Console.WriteLine(p2Scores[3]);
                    Console.ReadLine();

                    roundsPlayed++;
                }

                if (roundsPlayed == 4)
                {
                    Console.WriteLine("---------- Round 5 ----------\n");

                    Console.WriteLine("{0}, it is your roll! (Press Enter)", Player.playerNames[0]);
                    Console.ReadLine();
                    dice1.diceRoll(rand);
                    dice2.diceRoll(rand);
                    dice3.diceRoll(rand);
                    p1Scores[4] = dice1.getValue() + dice2.getValue() + dice3.getValue();
                    Console.WriteLine(p1Scores[4]);
                    Console.ReadLine();

                    Console.WriteLine("{0}, it is your roll! (Press Enter)", Player.playerNames[1]);
                    Console.ReadLine();
                    dice1.diceRoll(rand);
                    dice2.diceRoll(rand);
                    dice3.diceRoll(rand);
                    p2Scores[4] = dice1.getValue() + dice2.getValue() + dice3.getValue();
                    Console.WriteLine(p2Scores[4]);
                    Console.ReadLine();

                    roundsPlayed++; 
                }

                int p1Total = p1Scores.Sum();
                int p2Total = p2Scores.Sum();

                if (p1Total > p2Total)
                {
                    Console.WriteLine("Congratulations {0}, you have a score of {1} against {2}'s score of {3}. You have won the game. Press enter to return to game selection.", Player.playerNames[0],
                                      p1Total, Player.playerNames[1], p2Total);
                    
                    Console.ReadLine();
                }
                if (p2Total > p1Total)
                {
                    Console.WriteLine("Congratulations {0}, you have a score of {1} against {2}'s score of {3}. You have won the game. Press enter to return to game selection.", Player.playerNames[1],
                                      p2Total, Player.playerNames[0], p1Total);
                    
                    Console.ReadLine();
                }

                // RETURN TO GAME SELECTION
                if ((p1Total > p2Total) || (p2Total > p1Total))
                {
                    ResetGame();
                }
            }
        }
    }
}
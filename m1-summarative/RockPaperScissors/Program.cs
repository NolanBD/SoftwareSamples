using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {
            string playerChoice = "";
            int playChoiceNumber = 0;
            Random randomizer = new Random();
            int computerRPS = 0;
            int howManyRounds = 0;
            string roundsInput = "";
            int roundCounter = 0;
            int wins = 0;
            int losses = 0;
            int draws = 0;
            string playAgain = "";
            bool stillPlaying = true;
            bool anotherRound = true;

            //this loop houses the entire game, while stillPlaying is true,
            //the game will continue to play
            while (stillPlaying)
            {
                //resetting anotherRound to allow multiple sessions if the
                //player chooses to play more than 1 round
                anotherRound = true;
                bool validInput = false;
                do
                {
                    Console.WriteLine("How many rounds are you playing? Choose as few as 1 and no more than 10.");
                    roundsInput = Console.ReadLine();

                    if (int.TryParse(roundsInput, out howManyRounds))
                    {
                        if (howManyRounds > 0 && howManyRounds <= 10)
                        {
                            validInput = true;
                            //break;
                        }
                        else
                        {
                            Console.WriteLine("You need to play at least 1 round and no more than 10.");
                            //Console.WriteLine("How many rounds are you playing? Choose as few as 1 and no more than 10.");
                            //roundsInput = Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Your input has to be a whole number!");
                        //Console.WriteLine("How many rounds are you playing? Choose as few as 1 and no more than 10.");
                        //roundsInput = Console.ReadLine();
                    }
                }
                while (!validInput);

                //while i is less than the ammount of rounds requested,
                //the player chooses rock, paper, or scissors
                for (int i = 0; i < howManyRounds; i++)
                {
                    validInput = false;
                    //Get User Choice
                    do
                    {
                        Console.WriteLine("Rock, Paper, or Scissors?");
                        playerChoice = Console.ReadLine();

                        if (playerChoice.ToLower() == "rock" ||
                            playerChoice.ToLower() == "paper" ||
                            playerChoice.ToUpper() == "SCISSORS")
                        {
                            validInput = true;

                            if (playerChoice.ToLower() == "rock")
                            {
                                playChoiceNumber = 0;
                            }
                            else if (playerChoice.ToLower() == "paper")
                            {
                                playChoiceNumber = 1;
                            }
                            else
                            { 
                                playChoiceNumber = 2; 
                            }
                        }
                        else
                        {
                            Console.WriteLine("You have to choose either Rock, Paper, or Scissors!");
                            //if the iterator for the game loop is greater than or equal to
                            //0, the incrementor is decremented if the input is invalid
                            /*if (i > 0 || i == 0)
                            {
                                Console.WriteLine("You have to choose either Rock, Paper, or Scissors!");
                                i--;
                                continue;
                            }
                            if (i < 0)
                            {
                                Console.WriteLine("You have to choose either Rock, Paper, or Scissors!");
                            }*/
                        }

                    } while (!validInput);

                    computerRPS = randomizer.Next(0, 3);
                    if (playChoiceNumber == computerRPS)
                    {
                        roundCounter++;
                        draws++;
                        Console.WriteLine($"This round was a draw! You both chose {playerChoice}");
                    } 
                    else if((playChoiceNumber == 0 && computerRPS == 2) ||
                            (playChoiceNumber == 1 && computerRPS == 0) ||
                            (playChoiceNumber == 2 && computerRPS == 1))
                    {
                        roundCounter++;
                        wins++;
                        Console.WriteLine("You won with " + playerChoice + "!");
                    }
                    else
                    {
                        roundCounter++;
                        losses++;
                        Console.WriteLine("You lost with " + playerChoice + "!");
                    }

                    //the input is validated and can only be rock, paper, or scissors
                    /*if (playerChoice == "Rock" || playerChoice == "rock" ||
                        playerChoice == "Paper" || playerChoice == "paper" ||
                        playerChoice == "Scissors" || playerChoice == "scissors")
                    {*/
                    //player chooses rock
                    /*if (playerChoice == "Rock" || playerChoice == "rock")
                    {
                        //the input is given a value rock = 0, paper = 1, scissors = 2
                        playChoiceNumber = 0;
                        //a random value 0 through 2 is selected and assigned to the
                        //computer's choice of rock, paper, or scissors (0 through 2)
                        computerRPS = randomizer.Next(0, 3);
                        if (playChoiceNumber == computerRPS)
                        {
                            roundCounter++;
                            draws++;
                            Console.WriteLine($"This round was a draw! You both chose {playerChoice}");
                        }
                        if (playChoiceNumber == 0 && computerRPS == 1)
                        {
                            roundCounter++;
                            losses++;
                            Console.WriteLine("You lost! Paper covers rock!");
                        }
                        if (playChoiceNumber == 0 && computerRPS == 2)
                        {
                            roundCounter++;
                            wins++;
                            Console.WriteLine("You won! Rock breaks scissors!");
                        }
                    }
                    //the player chooses paper
                    if (playerChoice == "Paper" || playerChoice == "paper")
                    {
                        playChoiceNumber = 1;
                        computerRPS = randomizer.Next(0, 3);
                        if (playChoiceNumber == computerRPS)
                        {
                            roundCounter++;
                            draws++;
                            Console.WriteLine($"This round was a draw! You both chose {playerChoice}");
                        }
                        if (playChoiceNumber == 1 && computerRPS == 2)
                        {
                            roundCounter++;
                            losses++;
                            Console.WriteLine("You lost! Scissors cut paper!");
                        }
                        if (playChoiceNumber == 1 && computerRPS == 0)
                        {
                            roundCounter++;
                            wins++;
                            Console.WriteLine("You won! Paper covers rock!");
                        }
                    }
                    //the player chooses scissors
                    if (playerChoice == "Scissors" || playerChoice == "scissors")
                    {
                        playChoiceNumber = 2;
                        computerRPS = randomizer.Next(0, 3);
                        if (playChoiceNumber == computerRPS)
                        {
                            roundCounter++;
                            draws++;
                            Console.WriteLine($"This round was a draw! You both chose {playerChoice}");
                        }
                        if (playChoiceNumber == 2 && computerRPS == 0)
                        {
                            roundCounter++;
                            losses++;
                            Console.WriteLine("You lost! Rock breaks scissors!");
                        }
                        if (playChoiceNumber == 2 && computerRPS == 1)
                        {
                            roundCounter++;
                            wins++;
                            Console.WriteLine("You won! Scissors cuts paper!");
                        }
                    }*/
                    //}
                    //the player input was invalid

                }

                if (wins > losses)
                {
                    Console.WriteLine($"Congratulations! You won {wins} games! {draws} rounds were a draw.");
                }
                else if (wins < losses)
                {
                    Console.WriteLine($"Sorry! You lost {losses} games! {draws} rounds were a draw.");
                }
                else //if (wins == losses)
                {
                    Console.WriteLine($"Looks like nobody wins, you had {draws} draws, {wins} wins, and {losses} losses.");
                }
                while (anotherRound)
                {
                    Console.WriteLine("Do you want to play again? Yes or No?");
                    playAgain = Console.ReadLine();
                    if (playAgain == "Yes" || playAgain == "yes")
                    {
                        wins = 0;
                        losses = 0;
                        draws = 0;
                        anotherRound = false;
                    }
                    if (playAgain == "No" || playAgain == "no")
                    {
                        anotherRound = false;
                        stillPlaying = false;
                    }
                    if (playAgain != "No" && playAgain != "no" && playAgain != "Yes" && playAgain != "yes")
                    {
                        Console.WriteLine("Please respond with Yes or No.");
                    }
                }
            }
            Console.WriteLine("Thanks for playing!");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
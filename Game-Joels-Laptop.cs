using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frisbeev01
{
    class Game
    {
        Random random = new Random();
        private int ScoreTeam1 {get;set;}
        private int ScoreTeam2 { get; set; }
        private int Windspeed { get; set; } // Randomly chooses from array of windspeeds during game before each point
        private Team Team1 { get; set; }
        private Team Team2 { get; set; }
        private int BetterTeam { get; set; }    // 1 means team1 is better, 2 means team2 is better
        private int BetterLine { get; set; }
        private int DifferenceInTeamOverall { get; set; }
        private int DifferenceInLineOverall { get; set; }
        private int Probability { get; set; }
        public Team Winner = new Team();
        public Game(Team t1, Team t2)
        {
            Team1 = t1;
            Team2 = t2;
            ScoreTeam1 = 0;
            ScoreTeam2 = 0;
            DifferenceInTeamOverall = CalculateDifferenceInTeamOverall();   // Changes property to difference in team overall
        }
        public int CalculateDifferenceInTeamOverall()
        {   // Calculates the difference between the team's overall skill
            int differenceInTeamOverall = 1;
            if (Team1.Overall > Team2.Overall)
            {
                BetterTeam = 1;
                differenceInTeamOverall = Team1.Overall - Team2.Overall;
            }
            else if (Team1.Overall < Team2.Overall)
            {
                BetterTeam = 2;
                differenceInTeamOverall = Team2.Overall - Team1.Overall;
            }
            else
            {
                BetterTeam = 0;
                differenceInTeamOverall = 0;
            }
            return differenceInTeamOverall;
        }
        public int CalculateDifferenceInLinesOverall()
        {   // Calculates the difference between the team's overall skill
            int differenceInTeamOverall = 1;
            if (Team1.OLineOverall > Team2.DLineOverall)
            {
                BetterLine = 1;
                DifferenceInLineOverall = Team1.OLineOverall - Team2.DLineOverall;
            }
            else if (Team1.OLineOverall < Team2.DLineOverall)
            {
                BetterTeam = 2;
                differenceInTeamOverall = Team2.Overall - Team1.Overall;
            }
            else
            {
                BetterTeam = 0;
                differenceInTeamOverall = 0;
            }
            return differenceInTeamOverall;
        }
        public void CoinFlip()
        {   // Determines who starts the game with the disc
            Team1.coinFlipChoice = 1;
            Team2.coinFlipChoice = 2;
            int coinFlip = random.Next(1, 2);
            if (coinFlip == 1)
            {
                Team1.startPointWithDisc = true;
                Team2.startPointWithDisc = false;
                if (FullProgram.Verbosity == 2)
                    Console.WriteLine($"Team 1 won the disc flip");
            }
            else
            {
                Team1.startPointWithDisc = false;
                Team2.startPointWithDisc = true;
                if (FullProgram.Verbosity ==2)
                    Console.WriteLine($"Team 2 won the disc flip");
            }
        }
        public void PrintScoreboard()
        {
            if (FullProgram.Verbosity == 2)
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine($"{Team1.Name} {Team1.Mascot} {Team1.Overall}: {ScoreTeam1}");
                Console.WriteLine($"{Team2.Name} {Team2.Mascot} {Team2.Overall}: {ScoreTeam2}");
                Console.WriteLine("----------------------------------");
            }
        }
        public void PrintRoster(Team team)
        {
            Console.WriteLine("----------------------------------"); team.printTeam();
            foreach (Player player in team.TeamOfPlayers)
            {
                player.printAllAttributes();
            }
            Console.WriteLine("----------------------------------");
        }
        public void StartGame()
        {   // Gives intro, prints roster, calculates team's overall difference, finds windspeed, then starts game
            if (FullProgram.Verbosity == 2)
            {
                Console.WriteLine($"Welcome to Tonight's Matchup Between {Team1.Name} {Team1.Mascot} and {Team2.Name} {Team2.Mascot}");
                Console.WriteLine($"We hope the matchup will be close as {Team1.Name} has an overall: {Team1.Overall} while {Team2.Name} has an overall: {Team2.Overall}");
            }
            if (FullProgram.Verbosity == 3)
            {
                PrintRoster(Team1);
                PrintRoster(Team2);
            }
            CoinFlip();
            DifferenceInTeamOverall = CalculateDifferenceInTeamOverall();
            if (FullProgram.Verbosity == 3)
                Console.WriteLine($"The better team is {BetterTeam}");
            Probability = CalculateProbabilityForWinner();
            ScoreTeam1 = 0;
            ScoreTeam2 = 0;
        }
        public void SimulateFullGame()
        {   // Takes two teams, and simulates a full game.
            // All return values are used as properties and returned there and read later
            // Returned as game, so that it can be referenced later
            StartGame();
            int winner = 0;
            while (ScoreTeam1 !=15 && ScoreTeam2 !=15)
            {
                PlayPoint();
                if (ScoreTeam1 == 15)
                {
                    if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                    {
                        Console.WriteLine($"{Team1.Name} {Team1.Mascot} Wins {ScoreTeam1}-{ScoreTeam2}");
                        Console.WriteLine("-----------------------------------------");
                    }
                    Team1.TotalPoints += ScoreTeam1;
                    Team2.TotalPoints += ScoreTeam2;
                    Team1.TotalPointsAgainst += ScoreTeam2;
                    Team2.TotalPointsAgainst += ScoreTeam1;
                    Winner = Team1;
                    winner = 1; 
                }
                if (ScoreTeam2 == 15)
                {
                    if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                    {
                        Console.WriteLine($"{Team2.Name} {Team2.Mascot} Wins {ScoreTeam2}-{ScoreTeam1}");
                        Console.WriteLine("-----------------------------------------");
                    }
                    Team1.TotalPoints += ScoreTeam1;
                    Team2.TotalPoints += ScoreTeam2;
                    Team1.TotalPointsAgainst += ScoreTeam2;
                    Team2.TotalPointsAgainst += ScoreTeam1;
                    Winner = Team2;
                    winner = 2;
                }
                    if (winner == 1)
                    {
                        Team1.Wins += 1;
                        Team2.Losses += 1;
                    break;
                    }
                    else if (winner == 2)
                    {
                        Team1.Losses += 1;
                        Team2.Wins += 1;
                    break;
                    }
            }
        }
        public bool PlayPoint()
        {   // Plays through a full point. Does calculations and chooses point winner
            bool gameOver = false;
            int winner = 0;
            PrintScoreboard();
            if (Team1.startPointWithDisc == true)
            {
                if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                    Console.WriteLine($"{Team1.Name} {Team1.Mascot} Has the Disc to Start ");
            }
            else if (Team2.startPointWithDisc == true)
            {
                if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                    Console.WriteLine($"{Team2.Name} {Team2.Mascot} Has the Disc to Start ");
            }
            if (FullProgram.Verbosity == 3)
                Console.WriteLine("Choosing Point winner in play point");
            if (FullProgram.GameType == 1)
            {
                winner = SimulatePointWinner();
            }
            else
            {
                winner = PlayPointWinner();
            }
            gameOver = DetermineNextPointsInfo(winner);
            return gameOver;
        }
        public int PlayPointWinner()
        {
            int winner = 1;
            return winner;
        }
        public bool DetermineNextPointsInfo(int winner)
        {   // this is used for changing who has posession next time
            // It also returns a boolean for determining if game is over
            bool gameOver = false;
            if (winner == 1)
            {
                Team1.startPointWithDisc = false;
                Team2.startPointWithDisc = true;
                ScoreTeam1 = ScoreTeam1 + 1;
                if (FullProgram.Verbosity == 2)
                    Console.WriteLine($"{Team1.Name} {Team1.Mascot} Scored! ");
            }
            else if (winner == 2)
            {
                Team1.startPointWithDisc = true;
                Team2.startPointWithDisc = false;
                ScoreTeam2 = ScoreTeam2 + 1;
                if (FullProgram.Verbosity == 2)
                    Console.WriteLine($"{Team2.Name} {Team2.Mascot} Scored! ");
            }
            else
            {
                Console.WriteLine("Default winner case entered.");
            }
            // 15 is when the game is over
            if (ScoreTeam1 == 15)
            {
                gameOver = true;
            }
            else if (ScoreTeam2 == 15)
            {
                gameOver = true;
            }
            else
            {
                gameOver = false;
            }
            return gameOver;
        }
        public int CalculateProbabilityForWinner()
        {
            int probability;
            return probability = DifferenceInTeamOverall + 50;
        }
        public int SimulatePointWinner()
        {
            int determiner = random.Next(1, 100);  // Will help determine winner
            int winner = 0;
            if (Team1.startPointWithDisc == true)
            {   // + 10 probability for starting with disc
                if (FullProgram.Verbosity == 3)
                    Console.WriteLine($"Determiner: {determiner} and Probability: {Probability} ");
                if (BetterTeam == 1)
                {   // If betterTeam
                    if (determiner <= (Probability + 10))
                    {   // Team 1 has better chance to hold as better team
                        winner = 1;
                        if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                        {
                            Console.WriteLine($"Probability that {Team1.Name} wins: {Probability + 10} while receiving pull as better team");
                            Console.WriteLine("winner = 1 by holding as better team");
                        }
                    }
                    else
                    {   // Team 2 breaks and wins the point as worse team
                        winner = 2;
                        if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                        {
                            Console.WriteLine($"Probability that {Team2.Name} wins: {Probability - 10} by breaking as worse team");
                            Console.WriteLine("winner = 2 by by breaking as worse team");
                        }
                    }
                }
                else if (BetterTeam == 2)
                {   // If BetterTeam = 2
                    if (determiner <= (100 - Probability)+10)
                    {   // Team 1 holds and wins the point as worse team
                        winner = 1;
                        if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                        {
                            Console.WriteLine($"Probability that {Team1.Name} wins: {100 - Probability + 10} while receiving pull as worse team");
                            Console.WriteLine("winner = 1 by holding as worse team");
                        }
                    }
                    else
                    {   // Team 2 breaks as the better team
                        winner = 2;
                        if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                        {
                            Console.WriteLine($"Probability that {Team2.Name} wins: {100 - Probability + 10} by breaking as better team");
                            Console.WriteLine("winner = 2 by breaking as better team");
                        }
                    }
                }
                else
                {   // Teams are equal
                    if (determiner <= (Probability + 10))
                    {   // Team 1 has better chance to hold as equal team
                        winner = 1;
                        if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                        {
                            Console.WriteLine($"Probability that {Team1.Name} wins: {Probability+10} while receiving pull as equal team");
                            Console.WriteLine("winner = 2 by beating the odds and breaking as an equal team");
                        }
                    }
                    else
                    {   // Team 2 breaks and wins the point as equal team
                        winner = 2;
                        if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                        {
                            Console.WriteLine($"Probability that {Team2.Name} wins: {Probability-10} while breaking as equal team");
                            Console.WriteLine("winner = 2 by beating the odds and breaking as an equal team");
                        } 
                    }
                }
            }
            else if (Team2.startPointWithDisc == true)
            {   // If team 2 starts with disc
                if (FullProgram.Verbosity == 3)
                    Console.WriteLine($"Determiner: {determiner} and Probability: {Probability} ");
                if (BetterTeam == 2)
                {
                    if (determiner <= (Probability + 10)) // Gives Team2 better chance of holding   
                    {   // Team 2 holds and wins point as better team
                        winner = 2;
                        if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                        {
                            Console.WriteLine($"Probability that {Team2.Name} wins: {Probability + 10} while receiving pull as better team");
                            Console.WriteLine($"winner = {Team2.Name} by holding as better team");
                        }
                    }
                    else
                    {   // Else Team 1 breaks and wins the point as worse team
                        winner = 1;
                        if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                        {
                            Console.WriteLine($"Probability that {Team1.Name} wins: {Probability-10} while breaking pull as worse team");
                            Console.WriteLine($"winner = {Team1.Name} by breaking as worse team");
                        }  
                    }
                }
                else if (BetterTeam == 1)
                {   // Team 2 is not Better team
                    if (determiner <= 100 - Probability + 10)
                    {   // Team 2 holds and wins the point as worse team
                        winner = 2;
                        if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                        {
                            Console.WriteLine($"Probability that {Team2.Name} wins: {100 - Probability +10} while receiving pull as worse team");
                            Console.WriteLine("winner = 2 by holding as the worse team");
                        }
                    }
                    else
                    {   // Team 1 breaks and wins the point as better team
                        winner = 1;
                        if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                        {
                            Console.WriteLine($"Probability that {Team1.Name} wins: {100 - Probability+10} by breaking as equal team");
                            Console.WriteLine("winner = 1 by breaking as an equal team");
                        }
                    }
                }
                else
                {   // Teams are equal
                    if (determiner <= (Probability + 10)) // Gives Team2 better chance of holding   
                    {   // Team 2 holds and wins point as equal team
                        winner = 2;
                        if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                        {
                            Console.WriteLine($"Probability that {Team2.Name} wins: {Probability + 10} while receiving pull as equal team");
                            Console.WriteLine("winner = 2 by holding as equal team");
                        }
                    }
                    else
                    {   // Else Team 1 breaks and wins the point as equal team
                        winner = 1;
                        if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                        {
                            Console.WriteLine($"Probability that {Team1.Name} wins: {Probability - 10} while breaking as equal team");
                            Console.WriteLine("winner = 1 by holding as equal team");
                        }
                    }
                }
            }
            return winner;
        }
    }
}

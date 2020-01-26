using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace Frisbeev01
{
    public class Game
    {
        Random random = new Random();
        private int ScoreTeam1 { get; set; }
        private int ScoreTeam2 { get; set; }
        private int Windspeed { get; set; } // Randomly chooses from array of windspeeds during game before each point
        private Team Team0 { get; set; }    // This will be used as a team for between points, so that I can exit while loops
        private Team Team1 { get; set; }
        private Team Team2 { get; set; }
        private bool Team1TurnedOver { get; set; }
        private bool Team2TurnedOver { get; set; }
        private int BetterTeam { get; set; }    // 1 means team1 is better, 2 means team2 is better
        private int BetterLine { get; set; }
        private int DifferenceInTeamOverall { get; set; }
        private int DifferenceInLineOverall { get; set; }
        private Team TeamWithDisc { get; set; }
        private int Probability { get; set; }
        public Team Loser { get; set; }
        private int DistanceThrown { get; set; }    // 1 means short throw, 2 means medium, 3 means long
        private List<Player> OLineOnField = new List<Player>();
        private List<Player> DLineOnField = new List<Player>();
        public Team Winner = new Team();
        public Team OLineTeam1 = new Team();
        public Team OLineTeam2 = new Team();
        public Team DLineTeam1 = new Team();
        public Team DLineTeam2 = new Team();
        public Team sevenOnTeam1 = new Team();  // These will be the seven players on the field for each point
        public Team sevenOnTeam2 = new Team();
        Print print = new Print();  // This class is for printing out info like rosters
        //These next properties are for the manual play point
        public int yardsGainedDownfield { get; set; }   // 20 yard endzones count as -20 -> 0. 70 yard field counts as 0->70. last endzone is 70->90
        public int discLocationY { get; set; }
        public int playerWithDisc { get; set; }
        public int gameType { get; set; }   // If 1, it is play game, 2 is simulate game
        Player p1t1 = new Player();
        Player p2t1 = new Player();
        Player p3t1 = new Player();
        Player p4t1 = new Player();
        Player p5t1 = new Player();
        Player p6t1 = new Player();
        Player p7t1 = new Player();
        Player p8t1 = new Player();
        Player p9t1 = new Player();
        Player p10t1 = new Player();
        Player p11t1 = new Player();
        Player p12t1 = new Player();
        Player p13t1 = new Player();
        Player p14t1 = new Player();
        // Team 2
        Player p1t2 = new Player();
        Player p2t2 = new Player();
        Player p3t2 = new Player();
        Player p4t2 = new Player();
        Player p5t2 = new Player();
        Player p6t2 = new Player();
        Player p7t2 = new Player();
        Player p8t2 = new Player();
        Player p9t2 = new Player();
        Player p10t2 = new Player();
        Player p11t2 = new Player();
        Player p12t2 = new Player();
        Player p13t2 = new Player();
        Player p14t2 = new Player();
        public Player Thrower { get; set; }
        public Player Catcher { get; set; }
        //
        public Game(Team t1, Team t2, int type)
        {
            Create create = new Create();
            gameType = type;
            Team1 = t1;
            Team2 = t2;
            ScoreTeam1 = 0;
            ScoreTeam2 = 0;
            DifferenceInTeamOverall = CalculateDifferenceInTeamOverall();   // Changes property to difference in team overall
            OLineTeam1 = create.CreateLineFromList(t1.OLineList, true);
            OLineTeam2 = create.CreateLineFromList(t2.OLineList, true);
            DLineTeam1 = create.CreateLineFromList(t1.DLineList, false);
            DLineTeam2 = create.CreateLineFromList(t2.DLineList, false);
        }
        // The game Progression goes like this.
        // Game.SimulateFullgame is called by other method
        // StartGame happens, then coinflip. This determines who has the disc to start
        // Next, PlayPoint is called in a while loop while the game isn't over. 
        // Playpoint calls printscoreboard, and then it sets the lines according to who starts with the disc. (If Team1 starts with disc, Team 1's Oline starts
        // If the program is simulating, then SimulatePointWinner is called. This method uses probability to determine who won the point.
        // If the program is not a simulation, then PlayPointWinner is called. This method allows the user to play the point. 
        // After the point is played, DetermineNextPointsInfo is called. This method takes the winner of the point, switches who starts with the disc, and then determines if game is over or not
        // This ends the game while loop, and then each teams info is updated in terms of score and wins and such.
        public void StartGame()
        {   // Gives intro, prints roster, calculates team's overall difference, finds windspeed, then starts game
            // It also creates two team objects of seven on o line and seven on dline
            p1t1 = OLineTeam1.OLineHandles[0];
            p2t1 = OLineTeam1.OLineHandles[1];
            p3t1 = OLineTeam1.OLineHandles[2];
            p4t1 = OLineTeam1.OLineCutters[0];
            p5t1 = OLineTeam1.OLineCutters[1];
            p6t1 = OLineTeam1.OLineCutters[2];
            p7t1 = DLineTeam1.DLineCutters[3];
            p1t2 = DLineTeam1.DLineHandles[0];
            p2t2 = DLineTeam1.DLineHandles[1];
            p3t2 = DLineTeam1.DLineHandles[2];
            p4t2 = DLineTeam1.DLineCutters[0];
            p5t2 = DLineTeam1.DLineCutters[1];
            p6t2 = DLineTeam1.DLineCutters[2];
            p7t2 = DLineTeam1.DLineCutters[3];
            if (FullProgram.Verbosity == 2)
            {
                Console.WriteLine($"Welcome to Tonight's Matchup Between {Team1.Name} {Team1.Mascot} and {Team2.Name} {Team2.Mascot}");
                Console.WriteLine($"We hope the matchup will be close as {Team1.Name} has an overall: {Team1.Overall} while {Team2.Name} has an overall: {Team2.Overall}");
            }
            if (FullProgram.Verbosity == 3)
            {
                print.PrintRoster(Team1);
                print.PrintRoster(Team2);
            }
            CoinFlip();
            DifferenceInTeamOverall = CalculateDifferenceInTeamOverall();
            if (FullProgram.Verbosity == 3)
                Console.WriteLine($"The better team is {BetterTeam}");
            Probability = CalculateProbabilityForWinner();
            ScoreTeam1 = 0;
            ScoreTeam2 = 0;
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
                if (FullProgram.Verbosity == 2)
                    Console.WriteLine($"Team 2 won the disc flip");
            }
        }
        public void FullGame()
        {   // Takes two teams, and simulates a full game.
            // All return values are used as properties and returned there and read later
            // Returned as game, so that it can be referenced later
            StartGame();
            int winner = 0;
            while (ScoreTeam1 != 15 && ScoreTeam2 != 15)
            {
                PlayPoint();
                if (ScoreTeam1 == 15)
                {
                        Console.WriteLine($"{Team1.Name} {Team1.Mascot} Wins {ScoreTeam1}-{ScoreTeam2}");
                        Console.WriteLine("-----------------------------------------");
                    Team1.TotalPoints += ScoreTeam1;
                    Team2.TotalPoints += ScoreTeam2;
                    Team1.TotalPointsAgainst += ScoreTeam2;
                    Team2.TotalPointsAgainst += ScoreTeam1;
                    Winner = Team1;
                    Loser = Team2;
                    winner = 1;
                }
                if (ScoreTeam2 == 15)
                {
                        Console.WriteLine($"{Team2.Name} {Team2.Mascot} Wins {ScoreTeam2}-{ScoreTeam1}");
                        Console.WriteLine("-----------------------------------------");
                    Team1.TotalPoints += ScoreTeam1;
                    Team2.TotalPoints += ScoreTeam2;
                    Team1.TotalPointsAgainst += ScoreTeam2;
                    Team2.TotalPointsAgainst += ScoreTeam1;
                    Winner = Team2;
                    Loser = Team1;
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
        {   // Plays through a full point.
            // If GameType is 1, it simulates it and Does calculations and chooses point winner
            // If GameType is 2, the user plays it and a winner is chosen
            bool gameOver = false;
            int winner = 0;

            PrintScoreboard();
            if (Team1.startPointWithDisc == true)
            {
                sevenOnTeam1 = OLineTeam1;
                sevenOnTeam2 = DLineTeam2;
                discLocationY = 0;
                if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                    Console.WriteLine($"{Team1.Name} {Team1.Mascot} Has the Disc to Start ");
            }
            else if (Team2.startPointWithDisc == true)
            {
                sevenOnTeam1 = DLineTeam1;
                sevenOnTeam2 = OLineTeam2;
                discLocationY = 70;
                if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                    Console.WriteLine($"{Team2.Name} {Team2.Mascot} Has the Disc to Start ");
            }
            if (FullProgram.Verbosity == 3)
                Console.WriteLine("Choosing Point winner in play point");
    
            if (gameType == 1)
            {
                winner = PlayPointWinner();
            }
            else
            {
                winner = SimulatePointWinner();
            }
            gameOver = DetermineNextPointsInfo(winner);
            return gameOver;
        }
        public int SimulatePointWinner()
        {   // Simulates who wins the point based on probability and team overall
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
                    if (determiner <= (100 - Probability) + 10)
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
                            Console.WriteLine($"Probability that {Team1.Name} wins: {Probability + 10} while receiving pull as equal team");
                            Console.WriteLine("winner = 2 by beating the odds and breaking as an equal team");
                        }
                    }
                    else
                    {   // Team 2 breaks and wins the point as equal team
                        winner = 2;
                        if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                        {
                            Console.WriteLine($"Probability that {Team2.Name} wins: {Probability - 10} while breaking as equal team");
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
                            Console.WriteLine($"Probability that {Team1.Name} wins: {Probability - 10} while breaking pull as worse team");
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
                            Console.WriteLine($"Probability that {Team2.Name} wins: {100 - Probability + 10} while receiving pull as worse team");
                            Console.WriteLine("winner = 2 by holding as the worse team");
                        }
                    }
                    else
                    {   // Team 1 breaks and wins the point as better team
                        winner = 1;
                        if (FullProgram.Verbosity == 2 || FullProgram.Verbosity == 3)
                        {
                            Console.WriteLine($"Probability that {Team1.Name} wins: {100 - Probability + 10} by breaking as equal team");
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
        public int PlayPointWinner()
        {   // This is in contrast to Simulate Point winner
            // This method allows the user to play the point.
            // This will be text based, giving the user a couple options of where to throw.
            // Probability will be used to determine whether it is a score, and then uses probability
            // This assumes the user is team1
            int winner = 1; // Placeholder until I'm finished with it
            bool pointOver = false; // This turns true when someone scores
            // Initializes 14 players
            // p stands for player t stands for team the number stands for which player and which team
            // This is the Lineup
            /*
             *    1   2   3
             *  4   5   6   7
             */
            // Team 1
            int userChoice = 0;
            yardsGainedDownfield = 0;
            /*
            Include a try catch exception here later 
            */
            int numCatcher = 0;
            if (Team1.startPointWithDisc == true)
            {
                TeamWithDisc = Team1;
                discLocationY = 0;
                Console.WriteLine($"{Team1.Name} {Team1.Overall} will receive the pull");
                bool isCorrect = false;
                while (isCorrect == false)
                {
                    Console.WriteLine("Who do you want to catch the pull: ");
                    Console.WriteLine($"1: {p1t1.FirstName} {p1t1.LastName} {p1t1.Overall}");
                    Console.WriteLine($"2: {p2t1.FirstName} {p2t1.LastName} {p2t1.Overall}");
                    Console.WriteLine($"3: {p3t1.FirstName} {p3t1.LastName} {p3t1.Overall}");
                    if (!int.TryParse(Console.ReadLine(), out userChoice))
                    {
                        Console.WriteLine("Please enter either a 1, 2 or a 3");
                        isCorrect = false;
                    }
                    else
                    {
                        isCorrect = true;
                    }
                }
                switch (userChoice)
                {
                    case 1:
                        p1t1.HasDisc = true;
                        Catcher = p1t1;
                        numCatcher = 1;
                        break;
                    case 2:
                        p2t1.HasDisc = true;
                        Catcher = p2t1;
                        numCatcher = 2;
                        break;
                    case 3:
                        p3t1.HasDisc = true;
                        Catcher = p3t1;
                        numCatcher = 3;
                        break;
                    default:
                        p1t1.HasDisc = true;
                        Catcher = p1t1;
                        numCatcher = 1;
                        break;
                }
                discLocationY = 0;
            }
            else
            {
                Console.WriteLine($"{Team2.Name} {Team2.Overall} will receive the pull");
                discLocationY = 70;
                TeamWithDisc = Team2;
                switch (userChoice)
                {
                    case 1:
                        p1t2.HasDisc = true;
                        Catcher = p1t2;
                        numCatcher = 1;
                        break;
                    case 2:
                        p2t2.HasDisc = true;
                        Catcher = p2t2;
                        numCatcher = 2;
                        break;
                    case 3:
                        p3t2.HasDisc = true;
                        Catcher = p3t2;
                        numCatcher = 3;
                        break;
                    default:
                        p1t2.HasDisc = true;
                        Catcher = p1t2;
                        numCatcher = 1;
                        break;
                }
            }
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine($"{Catcher.FirstName} {Catcher.LastName} {Catcher.Overall} caught the pull at the {discLocationY} yard line");
            while (pointOver == false)
            {
                while (TeamWithDisc == Team1)
                {
                    if (Team2TurnedOver == true)
                    {   // Earlier, there wouldnt be a catcher, because team2 had caught it. This allows for a turnover, and the team picks it up
                        Catcher = p1t1; // Later have it randomly choose who picks it up
                        numCatcher = 1;
                    }
                    Console.WriteLine($"{Catcher.FirstName} {Catcher.LastName} has the disc on the {discLocationY} yard line.");
                    Console.WriteLine($"Choose who to throw it to (each player represents a different distance thrown: ");
                    Random probability = new Random();
                    if (Catcher == p1t1)
                    {
                        numCatcher = 1;
                    }
                    else if (Catcher == p2t1)
                    {
                        numCatcher = 2;
                    }
                    else if (Catcher == p3t1)
                    {
                        numCatcher = 3;
                    }
                    else if (Catcher == p4t1)
                    {
                        numCatcher = 4;
                    }
                    else if (Catcher == p5t1)
                    {
                        numCatcher = 5;
                    }
                    else if (Catcher == p6t1)
                    {
                        numCatcher = 6;
                    }
                    else if (Catcher == p7t1)
                    {
                        numCatcher = 7;
                    }
                    switch (numCatcher)
                    {   // This makes sure they can't throw it to themselves
                        // Other options will be 1 handle, and one intermediate cutter, and one deep cutter
                        case 1:
                            Catcher = ChoosePlayerToThrowTo(p2t1, p4t1, p5t1);
                            discLocationY += DetermineYardsGained();
                            break;
                        case 2:
                            Catcher = ChoosePlayerToThrowTo(p3t1, p5t1, p6t1);
                            discLocationY += DetermineYardsGained();
                            break;
                        case 3:
                            Catcher = ChoosePlayerToThrowTo(p2t1, p6t1, p7t1);
                            discLocationY += DetermineYardsGained();
                            break;
                        case 4:
                            Catcher = ChoosePlayerToThrowTo(p1t1, p3t1, p5t1);
                            discLocationY += DetermineYardsGained();
                            break;
                        case 5:
                            Catcher = ChoosePlayerToThrowTo(p2t1, p4t1, p6t1);
                            discLocationY += DetermineYardsGained();
                            break;
                        case 6:
                            Catcher = ChoosePlayerToThrowTo(p2t1, p5t1, p7t1);
                            discLocationY += DetermineYardsGained();
                            break;
                        case 7:
                            Catcher = ChoosePlayerToThrowTo(p3t1, p5t1, p6t1);
                            discLocationY += DetermineYardsGained();
                            break;
                        default:
                            Catcher = ChoosePlayerToThrowTo(p2t1, p4t1, p5t1);
                            discLocationY += DetermineYardsGained();
                            break;
                    }
                    if (Team1TurnedOver == true)
                    {
                        Console.WriteLine($"User turnover on the {discLocationY} yard line");
                    }
                    else
                    {
                        Console.WriteLine($"The disc is on the {discLocationY} yard line");
                        if (discLocationY > 90)
                        {   // Turnover because it is out of bounds
                            TeamWithDisc = Team2;
                            Console.WriteLine("Team 1 turned it out of the other endzone!");

                        }
                        else if (discLocationY < -20)
                        {   // If it goes out of other endzone
                            TeamWithDisc = Team2;
                            Console.WriteLine("Team 1 turned it out of their own endzone!");

                        }
                        else if (70 < discLocationY && discLocationY <= 90 && Team1TurnedOver == false)
                        {   // PointScored for Team1
                            Catcher.GameGoals += 1;
                            Thrower.GameAssists += 1;
                            Console.WriteLine($"{Thrower.FirstName} {Thrower.LastName} threw it to {Catcher.FirstName} {Catcher.LastName} for the score!");
                            Console.WriteLine($"{Thrower.FirstName} {Thrower.LastName} has {Thrower.GameAssists} assists for the game.");
                            Console.WriteLine($"{ Catcher.FirstName} {Catcher.LastName} has {Catcher.GameGoals} goals for the game.");
                            winner = 1;
                            pointOver = true;
                            TeamWithDisc = Team0;   // Exits while loop
                            Team1.startPointWithDisc = false;
                            Team2.startPointWithDisc = true;
                        }
                    }
                    
                    Console.WriteLine("--------------------------------------------------------");
                }
                while (TeamWithDisc == Team2)
                {
                    if (Team1TurnedOver == true)
                    {   // Earlier, there wouldnt be a catcher, because team2 had caught it. This allows for a turnover, and the team picks it up
                        Catcher = p1t2; // Later have it randomly choose who picks it up
                    }
                    if (discLocationY > 70 )
                    {   // If the disc is turned over in the endzone, or beyond the endzone, set it on goal line
                        discLocationY = 70;
                    }
                    if (discLocationY < 0)
                    {   // If disc is turned over in other endzone, it is brought up to goal line
                        discLocationY = 0;
                    }
                    Console.WriteLine("******************************************************");
                    Console.WriteLine($"{Catcher.FirstName} {Catcher.LastName} has the disc on the {discLocationY} yard line.");
                    Random probability = new Random();
                    //Console.WriteLine($"numCatcher {numCatcher}");
                    if (Catcher == p1t2)
                    {
                        numCatcher = 1;
                    }
                    else if (Catcher == p2t2)
                    {
                        numCatcher = 2;
                    }
                    else if (Catcher == p3t2)
                    {
                        numCatcher = 3;
                    }
                    else if (Catcher == p4t2)
                    {
                        numCatcher = 4;
                    }
                    else if (Catcher == p5t2)
                    {
                        numCatcher = 5;
                    }
                    else if (Catcher == p6t2)
                    {
                        numCatcher = 6;
                    }
                    else if (Catcher == p7t2)
                    {
                        numCatcher = 7;
                    }
                    switch (numCatcher)   // This only allows three options for user
                    //switch (Catcher)
                    {   // This makes sure they can't throw it to themselves
                        // Other options will be 1 handle, and one intermediate cutter, and one deep cutter
                        case 1:
                            Catcher = DeterminePlayerToThrowTo(p2t2, p4t2, p5t2);
                            discLocationY -= DetermineYardsGained();
                            break;
                        case 2:
                            Catcher = DeterminePlayerToThrowTo(p3t2, p5t2, p6t2);
                            discLocationY -= DetermineYardsGained();
                            break;
                        case 3:
                            Catcher = DeterminePlayerToThrowTo(p2t2, p6t2, p7t2);
                            discLocationY -= DetermineYardsGained();
                            break;
                        case 4:
                            Catcher = DeterminePlayerToThrowTo(p1t2, p3t2, p5t2);
                            discLocationY -= DetermineYardsGained();
                            break;
                        case 5:
                            Catcher = DeterminePlayerToThrowTo(p2t2, p4t2, p6t2);
                            discLocationY -= DetermineYardsGained();
                            break;
                        case 6:
                            Catcher = DeterminePlayerToThrowTo(p2t2, p5t2, p7t2);
                            discLocationY -= DetermineYardsGained();
                            break;
                        case 7:
                            Catcher = DeterminePlayerToThrowTo(p3t2, p5t2, p6t2);
                            discLocationY -= DetermineYardsGained();
                            break;
                        default:
                            Console.WriteLine("Entered default in switch case for determining player to throw");
                            Catcher = DeterminePlayerToThrowTo(p2t2, p4t2, p5t2);
                            discLocationY -= DetermineYardsGained();
                            break;
                    }
                    if (Team2TurnedOver == true)
                    {
                        Console.WriteLine($"Computer turnover on the {discLocationY} yard line");
                    }
                    else
                    {
                        if (discLocationY > 90)
                        {   // Turnover because it is out of bounds
                            TeamWithDisc = Team1;
                            discLocationY = 70;
                            Console.WriteLine("Team 2 turned it out of the other endzone!");

                        }
                        else if (discLocationY < -20)
                        {   // If it goes out of other endzone
                            TeamWithDisc = Team1;
                            discLocationY = 0;
                            Console.WriteLine("Team 2 turned it out of their own endzone!");

                        }
                        else if (-20 < discLocationY && discLocationY <= 0 && Team2TurnedOver == false)
                        {   // PointScored for Team1
                            Thrower.GameAssists += 1;
                            Catcher.GameGoals += 1;
                            Console.WriteLine($"{Thrower.FirstName} {Thrower.LastName} threw it to {Catcher.FirstName} {Catcher.LastName} for the score!");
                            Console.WriteLine($"{Thrower.FirstName} {Thrower.LastName} has {Thrower.GameAssists} assists for the game.");
                            Console.WriteLine($"{ Catcher.FirstName} {Catcher.LastName} has {Catcher.GameGoals} goals for the game.");
                            winner = 2;
                            pointOver = true;
                            TeamWithDisc = Team0;
                        }
                        else
                        {
                            Console.WriteLine($"Pass Completed to {Catcher.FirstName} {Catcher.LastName} on the {discLocationY} yard line");
                        }
                    }
                    Console.WriteLine("******************************************************");
                }
                if (TeamWithDisc == Team0)
                {
                    return winner;
                }
            }
            return winner;
        }
        public Player DeterminePlayerToThrowTo(Player p1, Player p2, Player p3)
        {   // this simulates the point for the computer, which the user is facing
            Console.WriteLine($"1: {p1.FirstName} {p1.LastName} {p1.Overall} for a handle reset");
            Console.WriteLine($"2: {p2.FirstName} {p2.LastName} {p2.Overall} for an intermediate cutter throw");
            Console.WriteLine($"3: {p3.FirstName} {p3.LastName} {p3.Overall} for a deep cutter throw");
            Random probability = new Random();
            int potentialCatcher = probability.Next(1, 3);
            switch (potentialCatcher)
            {   // DistanceThrown is backwards from other ChoosePlayer DistanceThrown, because other team is going the other direction
                case 1:
                    int probailityOfCatch = probability.Next(0, 100);
                    if (probailityOfCatch > 10)
                    {   // Then the catch is complete, and yardage gained will
                        Thrower = Catcher;
                        Catcher = p1;
                        DistanceThrown = -1;
                        //Console.WriteLine($"Pass Completed to {Catcher.FirstName} {Catcher.LastName}");
                        Team2TurnedOver = false;
                    }
                    else
                    {   // Turnover
                        Thrower = Catcher;
                        // Add a turnover to their stats right here
                        TeamWithDisc = Team1;
                        DistanceThrown = 1;
                        //Console.WriteLine($"Computer Turnover on the {discLocationY} yard line.");
                        Team2TurnedOver = true;
                    }
                    break;
                case 2:
                    probailityOfCatch = probability.Next(0, 100);
                    if (probailityOfCatch > 25)
                    {   // Then the catch is complete, and yardage gained will
                        Thrower = Catcher;
                        Catcher = p2;
                        DistanceThrown = -2;
                        //Console.WriteLine($"Pass Completed to {Catcher.FirstName} {Catcher.LastName}");
                        Team2TurnedOver = false;
                    }
                    else
                    {   // Turnover
                        Thrower = Catcher;
                        // Add a turnover to their stats right here
                        TeamWithDisc = Team1;
                        DistanceThrown = 2;
                        //Console.WriteLine($"Computer Turnover on the {discLocationY} yard line.");
                        Team2TurnedOver = true;
                    }
                    break;
                case 3:
                    probailityOfCatch = probability.Next(0, 100);
                    if (probailityOfCatch > 45)
                    {   // Then the catch is complete, and yardage gained will
                        Thrower = Catcher;
                        Catcher = p3;
                        DistanceThrown = -3;
                        //Console.WriteLine($"Pass Completed to {Catcher.FirstName} {Catcher.LastName}");
                        Team2TurnedOver = false;
                    }
                    else
                    {   // Turnover
                        Thrower = Catcher;
                        // Add a turnover to their stats right here
                        TeamWithDisc = Team1;
                        DistanceThrown = 3;
                        //Console.WriteLine($"Computer Turnover on the {discLocationY} yard line.");
                        Team2TurnedOver = true;
                    }
                    break;
                default:
                    probailityOfCatch = probability.Next(0, 100);
                    if (probailityOfCatch > 10)
                    {   // Then the catch is complete, and yardage gained will
                        Thrower = Catcher;
                        Catcher = p1;
                        DistanceThrown = -1;
                        //Console.WriteLine($"Pass Completed to {Catcher.FirstName} {Catcher.LastName} on the {discLocationY");
                        Team2TurnedOver = false;
                    }
                    else
                    {   // Turnover
                        Thrower = Catcher;
                        // Add a turnover to their stats right here
                        TeamWithDisc = Team1;
                        DistanceThrown = 1;
                        Team2TurnedOver = true;
                        //Console.WriteLine($"Computer Turnover on the {discLocationY} yard line.");
                    }
                    break;
            }
            Thread.Sleep(1000); // Pauses program for 0.1 seconds so that user can see what computer does
            return Catcher;
        }
        public Player ChoosePlayerToThrowTo(Player p1, Player p2, Player p3)
        {
            Console.WriteLine($"1: {p1.FirstName} {p1.LastName} {p1.Overall} for a handle reset");
            Console.WriteLine($"2: {p2.FirstName} {p2.LastName} {p2.Overall} for an intermediate cutter throw");
            Console.WriteLine($"3: {p3.FirstName} {p3.LastName} {p3.Overall} for a deep cutter throw");
            int potentialCatcher = int.Parse(Console.ReadLine());
            Random probability = new Random();
            switch (potentialCatcher)
            {
                case 1:
                    int probailityOfCatch = probability.Next(0, 100);
                    if (probailityOfCatch > 10)
                    {   // Then the catch is complete, and yardage gained will
                        Thrower = Catcher;
                        Catcher = p1;
                        DistanceThrown = 1;
                        Console.WriteLine($"Pass Completed to {Catcher.FirstName} {Catcher.LastName}");
                    }
                    else
                    {   // Turnover
                        Thrower = Catcher;
                        // Add a turnover to their stats right here
                        Team1TurnedOver = true;
                        TeamWithDisc = Team2;
                        DistanceThrown = -1;
                        Console.WriteLine("User Turnover.");
                    }
                    break;
                case 2:
                    probailityOfCatch = probability.Next(0, 100);
                    if (probailityOfCatch > 25)
                    {   // Then the catch is complete, and yardage gained will
                        Thrower = Catcher;
                        Catcher = p2;
                        DistanceThrown = 2;
                        Console.WriteLine($"Pass Completed to {Catcher.FirstName} {Catcher.LastName}");
                    }
                    else
                    {   // Turnover
                        Thrower = Catcher;
                        // Add a turnover to their stats right here
                        TeamWithDisc = Team2;
                        DistanceThrown = -2;
                        Console.WriteLine("User Turnover.");
                        Team1TurnedOver = true;
                    }
                    break;
                case 3:
                    probailityOfCatch = probability.Next(0, 100);
                    if (probailityOfCatch > 40)
                    {   // Then the catch is complete, and yardage gained will
                        Thrower = Catcher;
                        Catcher = p3;
                        DistanceThrown = 3;
                        Console.WriteLine($"Pass Completed to {Catcher.FirstName} {Catcher.LastName}");
                    }
                    else
                    {   // Turnover
                        Thrower = Catcher;
                        // Add a turnover to their stats right here
                        TeamWithDisc = Team2;
                        DistanceThrown = -3;
                        Console.WriteLine("User Turnover.");
                        Team1TurnedOver = true;
                    }
                    break;
                default:
                    probailityOfCatch = probability.Next(0, 100);
                    if (probailityOfCatch > 10)
                    {   // Then the catch is complete, and yardage gained will
                        Thrower = Catcher;
                        Catcher = p1;
                        DistanceThrown = 1;
                        Console.WriteLine($"Pass Completed to {Catcher.FirstName} {Catcher.LastName}");
                    }
                    else
                    {   // Turnover
                        Thrower = Catcher;
                        // Add a turnover to their stats right here
                        TeamWithDisc = Team2;
                        DistanceThrown = -1;
                        Console.WriteLine("User Turnover.");
                        Team1TurnedOver = true;
                    }
                    break;
            }
            return Catcher;
        }
        public int DetermineYardsGained()
        {
            int numYardsGained = 0;
            Random probability = new Random();
            switch (DistanceThrown)
            {
                case 1:
                    numYardsGained = probability.Next(-5, 5);
                    discLocationY += numYardsGained;
                    break;
                case -1:
                    numYardsGained = probability.Next(-5, 5);
                    discLocationY -= numYardsGained;
                    break;
                case 2:
                    numYardsGained = probability.Next(5, 12);
                    discLocationY += numYardsGained;
                    break;
                case -2:
                    numYardsGained = probability.Next(5, 12);
                    discLocationY -= numYardsGained;
                    break;
                case 3:
                    numYardsGained = probability.Next(12, 50);
                    discLocationY += numYardsGained;
                    break;
                case -3:
                    numYardsGained = probability.Next(12, 50);
                    discLocationY -= numYardsGained;
                    break;
                default:
                    numYardsGained = probability.Next(1, 10);
                    discLocationY += numYardsGained;
                    break;
            }
            return numYardsGained;
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
                discLocationY = 70;
                if (FullProgram.Verbosity == 1)
                    Console.WriteLine($"{Team1.Name} {Team1.Mascot} Scored! ");
            }
            else if (winner == 2)
            {
                Team1.startPointWithDisc = true;
                Team2.startPointWithDisc = false;
                ScoreTeam2 = ScoreTeam2 + 1;
                discLocationY = 0;
                if (FullProgram.Verbosity == 1)
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
        public void PrintScoreboard()
        {   // Prints the score of the current game
            if (FullProgram.Verbosity == 1)
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine($"{Team1.Name} {Team1.Mascot} {Team1.Overall}: {ScoreTeam1}");
                Console.WriteLine($"{Team2.Name} {Team2.Mascot} {Team2.Overall}: {ScoreTeam2}");
                Console.WriteLine("----------------------------------");
            }
        }
    }
}

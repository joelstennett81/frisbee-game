using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frisbeev01
{
    public class Test
    {
        public static void Main()
        {
            //FrisbeeGui.MainWindow;
            //Create create = new Create();
            //Team team1 = create.GenerateRandomTeam();
            //Team team2 = create.GenerateRandomTeam();
            //CheckTeams check = new CheckTeams();
            //team2.Name = check.checkNames(team1, team2);
            //team2.Mascot = check.checkMascots(team1, team2);
            //create.FillTeamWithRandomPlayers(team1);
            //create.FillTeamWithRandomPlayers(team2);
            //Game game1 = new Game(team1, team2);
            //game1.startGame();
            MainMenu menu = new MainMenu();
            Season season = new Season();
            Create create = new Create();
            int endProgram = 1;
            while (endProgram == 1)
            {
                int userChoice = menu.printMenu();
                switch (userChoice)
                {
                    case 1:
                        // User chooses to Play a single game
                        Game game1 = create.GenerateRandomGame(1);
                        game1.FullGame();
                        break;
                    case 2:
                        // User chooses to Simulate a single game
                        Game game2 = create.GenerateRandomGame(2);
                        game2.FullGame();
                        break;
                    case 3:
                        // User chooses to play a full season
                        season.SimulateSeason(1);
                        //season.SimulateTournament(userChoice);
                        break;
                    case 4:
                        // User chooses to simulate a full season
                        season.SimulateSeason(2);
                        //season.SimulateTournament(userChoice);
                        break;
                    default:
                        break;
                }
                bool isCorrect = false;
                while (isCorrect == false)
                {
                    Console.WriteLine("Would you like to re run the program (1 for yes or 2 for no)");
                    if (!int.TryParse(Console.ReadLine(), out endProgram))
                    {
                        Console.WriteLine("Please enter a 1 to run the program again, or a 2 to stop the program");
                        isCorrect = false;
                    }
                    else
                    {
                        isCorrect = true;
                    }
                }
            }
            //season.CreateFreeAgents();
            //season.PrintOutFreeAgents();
        }
    }
    public class CheckTeams
    {
        Random random = new Random(DateTime.Now.Ticks.GetHashCode());
        Create create = new Create();
        Test test = new Test();
        public string checkNames(Team team1, Team team2)
        {// Checks to make sure that both teams don't have the same name
            string name = team2.Name;
            while (team1.Name == team2.Name)
            {   // Makes sure that both teams aren't the same name
                // Changes team 2 because we are okay with team1 being the sme
                int index = random.Next(1, 24);
                name = create.GenerateTeamName(index);
            }
            if (FullProgram.Verbosity == 3)
                Console.WriteLine($"Check Mascots {team2.Name} {team2.Mascot}");
                return name;
        }
        public string checkMascots(Team team1, Team team2)
        {   // Checks to make sure teams dont have same mascot
            string mascot = team2.Mascot;
            while (team1.Mascot == team2.Mascot)
            {
                int index = random.Next(1, 24);
                mascot = create.GenerateTeamName(index);
            }
            if (FullProgram.Verbosity == 3)
                Console.WriteLine($"Check Mascots {team2.Name} {team2.Mascot}");
                return mascot;
        }
    }
}

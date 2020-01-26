using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frisbeev01
{
    public class Create
    {
        Player player = new Player();
        Team team = new Team();
        //private int seed = DateTime.Now.Ticks.GetHashCode();
        //public int seed = 10000;
        Random random = new Random();
        Season season = new Season();
        public List<Player> FreeAgents = new List<Player>();
        public Player UserCreatePlayer()
        {   // Allows thee user to create their own player, with their own abilities
            Console.WriteLine("First name: ");
            string fn = Console.ReadLine();
            Console.WriteLine("Last name: ");
            string ln = Console.ReadLine();
            Console.WriteLine("Jersey Number: ");
            string jn = Console.ReadLine();
            Console.WriteLine("Speed: ");
            int s = int.Parse(Console.ReadLine());
            Console.WriteLine("Jumping: ");
            int j = int.Parse(Console.ReadLine());
            Console.WriteLine("Flick Distance: ");
            int fd = int.Parse(Console.ReadLine());
            Console.WriteLine("Flick Accuracy: ");
            int fa = int.Parse(Console.ReadLine());
            Console.WriteLine("Backhand Accuracy: ");
            int ba = int.Parse(Console.ReadLine());
            Console.WriteLine("Backhand Distance: ");
            int bd = int.Parse(Console.ReadLine());
            Console.WriteLine("Cutter Defense: ");
            int cd = int.Parse(Console.ReadLine());
            Console.WriteLine("Handle Defense: ");
            int hd = int.Parse(Console.ReadLine());
            Console.WriteLine("Agility: ");
            int ag = int.Parse(Console.ReadLine());
            Console.WriteLine("Handle Cuts: ");
            int hc = int.Parse(Console.ReadLine());
            Console.WriteLine("Under Cuts: ");
            int uc = int.Parse(Console.ReadLine());
            Console.WriteLine("Deep Cuts: ");
            int dc = int.Parse(Console.ReadLine());
            Player player1 = new Player(fn, ln, jn, s, j, fd, fa, ba, bd, cd, hd, ag, hc, uc, dc);
            FreeAgents.Add(player1);
            //Console.WriteLine("Create RandomPlayer finished");
            return player1;
        }
        public Team UserCreateTeam()
        {   // User creates a team
            Console.WriteLine("Team Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Team Mascot: ");
            string mascot = Console.ReadLine();
            Team team = new Team(name, mascot);
            return team;
        }
        public void UserFillTeam()
        {   // User chooses players to fill team with
            ;
        }
        public void UserCreateFullTeam()
        {   // puts players into full team. This calls create and fill team funcitons
            ;
        }
        public List<Player> FillFreeAgents()
        {
            return FreeAgents;
        }
        public Player CreateRandomHandle()
        {
            string fn = GenerateRandomFirstName();
            string ln = GenerateRandomLastName();
            string jn = Convert.ToString(random.Next(0, 100));
            int s = random.Next(40, 80);
            int j = random.Next(40, 80);
            int fd = random.Next(60, 80);
            int fa = random.Next(60, 90);
            int ba = random.Next(60, 90);
            int bd = random.Next(60, 90);
            int cd = random.Next(40, 80);
            int hd = random.Next(40, 80);
            int ag = random.Next(40, 80);
            int hc = random.Next(60, 90);
            int uc = random.Next(40, 60);
            int dc = random.Next(40, 60);
            Player player1 = new Player(fn, ln, jn, s, j, fd, fa, ba, bd, cd, hd, ag, hc, uc, dc);
            player1.IsHandle = true;
            if (FullProgram.Verbosity == 3)
                Console.WriteLine("Create RandomPlayer finished");
            return player1;
        }
        public Player CreateRandomCutter()
        {
            string fn = GenerateRandomFirstName();
            string ln = GenerateRandomLastName();
            string jn = Convert.ToString(random.Next(0, 100));
            int s = random.Next(40, 80);
            int j = random.Next(40, 80);
            int fd = random.Next(40, 70);
            int fa = random.Next(40, 70);
            int ba = random.Next(40, 70);
            int bd = random.Next(40, 70);
            int cd = random.Next(40, 80);
            int hd = random.Next(40, 80);
            int ag = random.Next(40, 80);
            int hc = random.Next(40, 80);
            int uc = random.Next(60, 90);
            int dc = random.Next(60, 90);
            Player player1 = new Player(fn, ln, jn, s, j, fd, fa, ba, bd, cd, hd, ag, hc, uc, dc);
            player1.IsHandle = false;
            if (FullProgram.Verbosity == 3)
                Console.WriteLine("Create RandomPlayer finished");
            return player1;
        }
        public Player CreateRandomPlayer()
        {   // Creates a player with good attributes
            string fn = GenerateRandomFirstName();
            string ln = GenerateRandomLastName();
            string jn = Convert.ToString(random.Next(0, 100));
            int s = random.Next(40, 80);
            int j = random.Next(40, 80);
            int fd = random.Next(40, 80);
            int fa = random.Next(40, 80);
            int ba = random.Next(40, 80);
            int bd = random.Next(40, 80);
            int cd = random.Next(40, 80);
            int hd = random.Next(40, 80);
            int ag = random.Next(40, 80);
            int hc = random.Next(40, 80);
            int uc = random.Next(40, 80);
            int dc = random.Next(40, 80);
            Player player1 = new Player(fn, ln, jn, s, j, fd, fa, ba, bd, cd, hd, ag, hc, uc, dc);
            player1.GameAssists = 0;
            player1.GameGoals = 0;
            player1.SeasonAssists = 0;
            player1.SeasonGoals = 0;
            if (FullProgram.Verbosity == 3)
                Console.WriteLine("Create RandomPlayer finished");
            return player1;
        }
        public string GenerateRandomFirstName()
        {
            List<string> listFirst = player.ReadInFirstNames();
            int index = random.Next(1, 1000);
            string firstName = listFirst[index];
            return firstName;
        }
        public string GenerateRandomLastName()
        {
            List<string> listLast = player.ReadInLastNames();
            int index1 = random.Next(1, 1000);
            string lastName = listLast[index1];
            return lastName;
        }
        public Team GenerateRandomTeam()
        {
            int index = random.Next(1, 24);
            string teamName = GenerateTeamName(index);
            string mascotName = GenerateTeamMascot(index);
            Team team1 = new Team(teamName, mascotName);
            if (FullProgram.Verbosity == 3)
            {
                Console.WriteLine("Generate RandomTeam finished");
                Console.WriteLine($"team.Name = {team1.Name} , mascot = {team1.Mascot}");
            }
                return team1;
        }
        public Game GenerateRandomGame(int userChoice)
        {
            Team team1 = ConstructFullTeam();
            Team team2 = ConstructFullTeam();
            Game game1 = new Game(team1, team2, userChoice);
            return game1;
        }
        public string GenerateTeamName(int index)
        {
            //Random random = new Random(DateTime.Now.Ticks.GetHashCode());
            List<string> listTeams = team.ReadInTeams();
            string teamName = listTeams[index];
            if (FullProgram.Verbosity == 3)
            {
                Console.WriteLine("Generate Team name finished");
                Console.WriteLine($"teamName = {teamName}");
            }
                return teamName;
        }
        public string GenerateTeamMascot(int index)
        {
            //Random random = new Random(DateTime.Now.Ticks.GetHashCode());
            List<string> listMascots = team.ReadInMascots();
            string teamMascot = listMascots[index];
            if (FullProgram.Verbosity == 3)
            {
                Console.WriteLine("Generate Team mascot finished");
                Console.WriteLine($"teamMascot = {teamMascot}");
            }
            return teamMascot;

        }
        public int GenerateTeamOverall(int index)
        {
            int overall = random.Next(0, 100);
            return overall;
        }
        public Team ConstructFullTeam()
        {
            team = GenerateRandomTeam();
            team.FillTeamWithRandomPlayers();
            team.CompCreateLines();
            return team;
        }
        public Team CreateLineFromList(List<Player> players,bool isOline)
        {
            Team team = new Team();
            for (int i = 0; i < 7; i++)
            {
                team.AddPlayer(players[i]);
                if (isOline == true)
                {
                    if (players[i].IsHandle == true)
                    {
                        team.OLineHandles.Add(players[i]);
                    }
                    else
                    {
                        team.OLineCutters.Add(players[i]);
                    }
                }
                else
                {
                    if (players[i].IsHandle == true)
                    {
                        team.DLineHandles.Add(players[i]);
                    }
                    else
                    {
                        team.DLineCutters.Add(players[i]);
                    }
                }
                
            }
            return team;
        }
        public Team CreateTeamFromList(List<Player> players)
        {
            Team team = new Team();
            for (int i = 0; i < 14; i++)
            {
                team.AddPlayer(players[i]);
            }
            return team;
        }
    }
}

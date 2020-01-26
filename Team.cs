using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Frisbeev01
{
    public class Team
    {
        public string Name { get; set; }
        public string Mascot { get; set; }
        public List<Player> TeamOfPlayers = new List<Player>();
        private string filename = "TeamList.csv";
        public List<string> teamDatabase = new List<string>(); 
        public List<string> mascotDatabase = new List<string>();
        public List<string> firstNameDatabase = new List<string>();
        public List<string> lastNameDatabase = new List<string>();
        public List<Player> OLineList = new List<Player>();
        public List<Player> DLineList = new List<Player>();
        public List<Player> SortedOLine = new List<Player>();
        public List<Player> SortedDLine = new List<Player>();
        public List<Player> OLineHandles = new List<Player>();
        public List<Player> OLineCutters = new List<Player>();
        public List<Player> DLineHandles = new List<Player>();
        public List<Player> DLineCutters = new List<Player>();
        public int Overall { get; set; }    // How good a team is (0-100)
        public int OLineOverall { get; set; }
        public int DLineOverall { get; set; }
        public bool startPointWithDisc { get; set; }     // Determines who starts each point on offense
        public int coinFlipChoice { get; set; }  // Either 1 or 2. 1 is heads, 2 is tails 
        public bool WonPreviousPoint { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int TotalPoints { get; set; }
        public int TotalPointsAgainst { get; set; }
        public int TotalPointDifferential { get; set; }
        // Constructors
        public Team()
        {
            ;
        }
        public Team(string n, string m)
        {
            Name = n;
            Mascot = m;
            TotalPoints = 0;
            TotalPointsAgainst = 0;
        }
        public void CompCreateLines()
        {   // Computer creates an O line and D line. There are 7 on each line, 3 handles and four cutters.
            // Each team has fourteen people
            int countHandlesO = 0;  // When there are three handles on O line, then i will put handles on d line
            int countCuttersO = 0;  // When there are 4 cutters on O line, then I will put cutters on d line
            for (int i = 0; i < 14; i++)
            {   // Adds 3 handles to OLine
                Player player = TeamOfPlayers[i];
                if (player.IsHandle == true)
                {
                    if (countHandlesO == 3)
                    {
                        DLineList.Add(player);
                        DLineHandles.Add(player);
                    }
                    else
                    {   // Adds handles 3 times
                        OLineList.Add(player);
                        OLineHandles.Add(player);
                        countHandlesO++;
                    }
                }
                else
                {
                    if (countCuttersO == 4)
                    {
                        DLineList.Add(player);
                        DLineCutters.Add(player);
                    }
                    else
                    {   // Adds cutters 4 times
                        OLineList.Add(player);
                        OLineCutters.Add(player);
                        countCuttersO++;
                    }
                }
            }
            // SortedStandings = Standings.OrderBy(Team => Team.Wins).ThenBy(Team => Team.TotalPointDifferential).ToList();
            //SortedOLine.OrderBy(Player => Player.IsHandle).ThenBy(Player => Player.ThrowAbility);
            CalculateLinesOverall();
        }
        public void AddPlayer(Player player)
        {   // Adds a player to a team
            TeamOfPlayers.Add(player);
        }
        public List<string> ReadInTeams()
        {   // This reads in the list of teams from a .csv file
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    // Read in individual lines, and then separate by commas
                    string line = reader.ReadLine();
                    string[] fields = line.Split(',');
                    // Add these fields to a job object, which is added to list
                    teamDatabase.Add(fields[0]);
                }
                return teamDatabase;
            }
        }
        public List<string> ReadInMascots()
        {   // This reads in a list of mascots from a .csv file
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    // Read in individual lines, and then separate by commas
                    string line = reader.ReadLine();
                    string[] fields = line.Split(',');
                    // Add these fields to a job object, which is added to list
                    mascotDatabase.Add(fields[1]);
                }
                return mascotDatabase;
            }
        }
        public void printTeam()
        {
            Console.WriteLine($"{Name} {Mascot} {Overall}");
        }
        public void FillTeamWithRandomPlayers()
        {   // Creates 14 random players, 6 of which are handles and 8 of which are cutters
            // It then puts them into a team. 
            Create create = new Create();
            Player player = new Player();
            Season season = new Season();
            for (int i = 0; i < 6; i++)
            {   // Add 6 handles
                AddPlayer(player = create.CreateRandomHandle());
            }
            for (int i = 0; i < 8; i++)
            {
                AddPlayer(player = create.CreateRandomCutter());
            }
            CalculateTeamOverall(); // Gives the team an overall rating based on each player
        }
        public List<Player> ChooseLine(bool isOLine)
        {   // If on offense, then OLine is sent out, if on defense, DLine is sent out
            List<Player> sevenOnField = new List<Player>();
            if (isOLine == true)
            {
                sevenOnField = OLineList;
            }
            else
            {
                sevenOnField = DLineList;
            }
            return sevenOnField;
        }
        public void CalculateTeamOverall()
        {
            int sum = 0;
            int count = 0;
            foreach (Player player in TeamOfPlayers)
            {
                sum = sum + player.Overall;
                count = count + 1;
            }
            Overall = (sum / count);
        }
        public void CalculateLinesOverall()
        {
            int sum = 0;
            int count = 0;
            foreach (Player player in OLineList)
            {
                sum = sum + player.Overall;
                count = count + 1;
            }
            OLineOverall = (sum / count);
            sum = 0;
            count = 0;
            foreach (Player player in DLineList)
            {
                sum += player.Overall;
                count += 1;
            }
            DLineOverall = (sum / count);
        }
        public void CalculateTeamPointDifferential()
        {
            TotalPointDifferential = TotalPoints - TotalPointsAgainst;
        }
    }
}

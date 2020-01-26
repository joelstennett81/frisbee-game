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
        public List<Player> OLine = new List<Player>();   // The bool if true, 
        public List<Player> DLine = new List<Player>();
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
        public Team()
        {
            ;
        }
        public void CompCreateLines()
        {
            for (int i = 0; i < 7; i++)
            {
                OLine.Add(TeamOfPlayers[i]);
            }
            for (int i = 7; i < 14; i++)
            {
                DLine.Add(TeamOfPlayers[i]);
            }
            CalculateLinesOverall();
        }
        public Team(string n, string m)
        {
            Name = n;
            Mascot = m;
            TotalPoints = 0;
            TotalPointsAgainst = 0;
        }
        public void AddPlayer(Player player)
        {
            TeamOfPlayers.Add(player);
        }
        public List<string> ReadInTeams()
        {
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
        {
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
        {
            Create create = new Create();
            Player player = new Player();
            Season season = new Season();
            for (int i = 0; i < 14; i++)
            {
                AddPlayer(player = create.CreateRandomPlayer());
            }
            CalculateTeamOverall(); // Gives the team an overall rating based on each player
            //Console.WriteLine("FillTeamWithRandomPlayers finished");
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
            foreach (Player player in OLine)
            {
                sum = sum + player.Overall;
                count = count + 1;
            }
            OLineOverall = (sum / count);
            sum = 0;
            count = 0;
            foreach (Player player in DLine)
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

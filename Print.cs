using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frisbeev01
{
    public class Print
    {
        public void PrintRoster(Team team)
        {   // Prints Roster. Sometimes the OLine or DLine will be passed in, and it will be used to print out the line
            Console.WriteLine("----------------------------------");
            team.printTeam();
            foreach (Player player in team.TeamOfPlayers)
            {
                player.printAllAttributes();
            }
            Console.WriteLine("----------------------------------");
        }
        public void PrintOLine(Team team)
        {
            Console.WriteLine("----------------------------------");
            team.printTeam();
            foreach (Player player in team.OLineList)
            {
                player.printBasicInfo();
            }
            Console.WriteLine("----------------------------------");
        }
        public void PrintDLine(Team team)
        {
            Console.WriteLine("----------------------------------");
            team.printTeam();
            foreach (Player player in team.DLineList)
            {
                player.printBasicInfo();
            }
            Console.WriteLine("----------------------------------");
        }
    }
}

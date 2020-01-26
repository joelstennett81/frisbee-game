using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frisbeev01
{
    public class SortStandings : IComparer<Team>
    {   // This is how I will sort my teams to determine the final standings for the season
        public int Compare(Team team1, Team team2)
        {   // returns 1 if team1 is better, 2 if team2 is better
            if (team1.Wins > team2.Wins)
            {
                return 1;
            }
            else if (team2.Wins > team1.Wins)
            {
                return 2;
            }
            else
            {   // If teams are equal in terms of wins
                if (team1.TotalPointDifferential > team2.TotalPointDifferential)
                {
                    return 1;
                }
                else if (team2.TotalPointDifferential > team1.TotalPointDifferential)
                {
                    return 2;
                }
                else
                {   // If they have the same point differential as well as wins
                    if (team1.Overall > team2.Overall)
                    {   // Checks team overall as last step, and then gives underdog the win
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
        }
    }
}

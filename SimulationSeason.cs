using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frisbeev01
{
    public class Season
    {
        public List<Team> ListTeams = new List<Team>(10);
        public List<Player> FreeAgents = new List<Player>();
        public List<Team> Standings = new List<Team>();
        public List<Team> SortedStandings = new List<Team>();
        public void FillLeagueWithRandomTeams()
        {
            Create create = new Create();
            for (int i = 0; i < 10; i++)
            {
                Team team = create.ConstructFullTeam();
                if (FullProgram.Verbosity == 2)
                {
                    Console.WriteLine($"Team Name: {team.Name} Team Overall: {team.Overall}");
                    Console.WriteLine($"OLine Overall: {team.OLineOverall}, DLine Overall: {team.DLineOverall}");
                }
                ListTeams.Add(team);
            }
        }
        public void FillLeagueWithChosenTeams()
        {
            ;
        }
        public void SimulateSeason()
        {
            FillLeagueWithRandomTeams();
            for (int i = 0; i < 10; i++)
            {
                Standings.Add(ListTeams[i]);
            }
            for (int i = 1; i < 10; i++) // Play each week of the 9 game season
            {
                PlayWeek(i);
            }
            for (int i = 0; i < 10; i++)
            {   // Calculates Point Differential at the end of the season
                Team team = ListTeams[i];
                team.CalculateTeamPointDifferential();
            }
            PrintStandings();
            //Team champion = PlayTournament();
            //PrintTourneyResults(champion);
        }
        public void PrintStandings()
        {
            Console.WriteLine("Final Standings: ");
            CalculateStandings();
            for (int i = 0; i < 10; i++)
            {
                Team team = SortedStandings[i];
                Console.WriteLine($"{i+1}| {team.Name} {team.Mascot} {team.Overall} Wins: {team.Wins}, Losses: {team.Losses}, PointsFor: {team.TotalPoints}, PointsAgainst: {team.TotalPointsAgainst}, PointDiff: {team.TotalPointDifferential}");
            }
        }
        public void CalculateStandings()
        {
            SortedStandings = Standings.OrderBy(Team => Team.Wins).ThenBy(Team => Team.TotalPointDifferential).ToList();
            SortedStandings.Reverse();  // Flips list around, because for some reason, it was backwards
        }
        public Team PlayTournament()
        {
            Game game1 = new Game(SortedStandings[6], SortedStandings[9]);  // Round 1 play in game 
            game1.SimulateFullGame();
            Game game2 = new Game(SortedStandings[7], SortedStandings[8]); // Round 1 play in game
            game2.SimulateFullGame();
            Game game3 = new Game(SortedStandings[0], game2.Winner);    // Round 2 Left Top Bracket
            game3.SimulateFullGame();
            Game game4 = new Game(SortedStandings[1], game1.Winner);    // Round 2  Right Top Bracket
            game4.SimulateFullGame();
            Game game5 = new Game(SortedStandings[2], SortedStandings[5]);  // Round 2  // Right Bottom Bracket
            game5.SimulateFullGame();
            Game game6 = new Game(SortedStandings[3], SortedStandings[4]);  // Round 2  // Left Bottom Bracket
            game6.SimulateFullGame();
            Game game7 = new Game(game3.Winner, game6.Winner);  // Semifinals Left Bracket
            game7.SimulateFullGame();
            Game game8 = new Game(game4.Winner, game5.Winner);  // Semifinals Right Bracket
            game8.SimulateFullGame();
            Game game9 = new Game(game7.Winner, game8.Winner);  // Championship Game
            game9.SimulateFullGame();
            return game9.Winner;
        }
        public void PrintTourneyResults(Team team)
        {
            Console.WriteLine($"The Tournament Winner is: {team.Name} {team.Mascot}! Congratulations!");
        }
        public void PlayWeek(int week)
        {
            switch (week)
            {   // Simulates 5 games for 9 weeks, so everyone plays each other once
                case 1:
                    Game game1 = new Game(ListTeams[0], ListTeams[9]);
                    Game game2 = new Game(ListTeams[1], ListTeams[8]);
                    Game game3 = new Game(ListTeams[2], ListTeams[7]);
                    Game game4 = new Game(ListTeams[3], ListTeams[6]);
                    Game game5 = new Game(ListTeams[4], ListTeams[5]);
                    game1.SimulateFullGame();
                    game2.SimulateFullGame();
                    game3.SimulateFullGame();
                    game4.SimulateFullGame();
                    game5.SimulateFullGame();
                    break;
                case 2:
                    Game game6 = new Game(ListTeams[6], ListTeams[1]);
                    Game game7 = new Game(ListTeams[5], ListTeams[2]);
                    Game game8 = new Game(ListTeams[4], ListTeams[3]);
                    Game game9 = new Game(ListTeams[9], ListTeams[8]);
                    Game game10 = new Game(ListTeams[7], ListTeams[0]);
                    game6.SimulateFullGame();
                    game7.SimulateFullGame();
                    game8.SimulateFullGame();
                    game9.SimulateFullGame();
                    game10.SimulateFullGame();
                    break;
                case 3:
                    Game game11 = new Game(ListTeams[8], ListTeams[7]);
                    Game game12 = new Game(ListTeams[0], ListTeams[6]);
                    Game game13 = new Game(ListTeams[5], ListTeams[1]);
                    Game game14 = new Game(ListTeams[4], ListTeams[2]);
                    Game game15 = new Game(ListTeams[3], ListTeams[9]);
                    game11.SimulateFullGame();
                    game12.SimulateFullGame();
                    game13.SimulateFullGame();
                    game14.SimulateFullGame();
                    game15.SimulateFullGame();
                    break;
                case 4:
                    Game game16 = new Game(ListTeams[3], ListTeams[2]);
                    Game game17 = new Game(ListTeams[9], ListTeams[7]);
                    Game game18 = new Game(ListTeams[8], ListTeams[6]);
                    Game game19 = new Game(ListTeams[5], ListTeams[0]);
                    Game game20 = new Game(ListTeams[1], ListTeams[4]);
                    game16.SimulateFullGame();
                    game17.SimulateFullGame();
                    game18.SimulateFullGame();
                    game19.SimulateFullGame();
                    game20.SimulateFullGame();
                    break;
                case 5:
                    Game game21 = new Game(ListTeams[9], ListTeams[5]);
                    Game game22 = new Game(ListTeams[6], ListTeams[4]);
                    Game game23 = new Game(ListTeams[1], ListTeams[0]);
                    Game game24 = new Game(ListTeams[7], ListTeams[3]);
                    Game game25 = new Game(ListTeams[8], ListTeams[2]);
                    game21.SimulateFullGame();
                    game22.SimulateFullGame();
                    game23.SimulateFullGame();
                    game24.SimulateFullGame();
                    game25.SimulateFullGame();
                    break;
                case 6:
                    Game game26 = new Game(ListTeams[4], ListTeams[8]);
                    Game game27 = new Game(ListTeams[7], ListTeams[5]);
                    Game game28 = new Game(ListTeams[6], ListTeams[9]);
                    Game game29 = new Game(ListTeams[2], ListTeams[1]);
                    Game game30 = new Game(ListTeams[0], ListTeams[3]);
                    game26.SimulateFullGame();
                    game27.SimulateFullGame();
                    game28.SimulateFullGame();
                    game29.SimulateFullGame();
                    game30.SimulateFullGame();
                    break;
                case 7:
                    Game game31 = new Game(ListTeams[2], ListTeams[0]);
                    Game game32 = new Game(ListTeams[8], ListTeams[3]);
                    Game game33 = new Game(ListTeams[7], ListTeams[4]);
                    Game game34 = new Game(ListTeams[6], ListTeams[5]);
                    Game game35 = new Game(ListTeams[9], ListTeams[1]);
                    game31.SimulateFullGame();
                    game32.SimulateFullGame();
                    game33.SimulateFullGame();
                    game34.SimulateFullGame();
                    game35.SimulateFullGame();
                    break;
                case 8:
                    Game game36 = new Game(ListTeams[5], ListTeams[3]);
                    Game game37 = new Game(ListTeams[4], ListTeams[9]);
                    Game game38 = new Game(ListTeams[0], ListTeams[8]);
                    Game game39 = new Game(ListTeams[1], ListTeams[7]);
                    Game game40 = new Game(ListTeams[2], ListTeams[6]);
                    game36.SimulateFullGame();
                    game37.SimulateFullGame();
                    game38.SimulateFullGame();
                    game39.SimulateFullGame();
                    game40.SimulateFullGame();
                    break;
                case 9:
                    Game game41 = new Game(ListTeams[7], ListTeams[6]);
                    Game game42 = new Game(ListTeams[3], ListTeams[1]);
                    Game game43 = new Game(ListTeams[9], ListTeams[2]);
                    Game game44 = new Game(ListTeams[0], ListTeams[4]);
                    Game game45 = new Game(ListTeams[5], ListTeams[8]);
                    game41.SimulateFullGame();
                    game42.SimulateFullGame();
                    game43.SimulateFullGame();
                    game44.SimulateFullGame();
                    game45.SimulateFullGame();
                    break;
                default:
                    Console.WriteLine("Default Case entered");
                    break;
            }
        }
    }
    
}

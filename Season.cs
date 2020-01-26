using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Frisbeev01
{
    public class Season
    {
        public List<Team> ListTeams = new List<Team>(10);
        public List<Team> Standings = new List<Team>();
        public List<Team> SortedStandings = new List<Team>();
        public List<Team> SortedTourneyStandings = new List<Team>();
        public List<Player> FreeAgents = new List<Player>();
        public Team Team1 = new Team();
        public Team Team2 = new Team();
        public Team Team3 = new Team();
        public Team Team4 = new Team();
        public Team Team5 = new Team();
        public Team Team6 = new Team();
        public Team Team7 = new Team();
        public Team Team8 = new Team();
        public Team Team9 = new Team();
        public Team Team10 = new Team();
        public void FillLeagueWithRandomTeams()
        {
            Create create = new Create();
            ListTeams.Add(Team1 = create.ConstructFullTeam());
            ListTeams.Add(Team2 = create.ConstructFullTeam());
            ListTeams.Add(Team3 = create.ConstructFullTeam());
            ListTeams.Add(Team4 = create.ConstructFullTeam());
            ListTeams.Add(Team5 = create.ConstructFullTeam());
            ListTeams.Add(Team6 = create.ConstructFullTeam());
            ListTeams.Add(Team7 = create.ConstructFullTeam());
            ListTeams.Add(Team8 = create.ConstructFullTeam());
            ListTeams.Add(Team9 = create.ConstructFullTeam());
            ListTeams.Add(Team10 = create.ConstructFullTeam());
            //for (int i = 0; i < 10; i++)
            //{
            //    Team team = create.ConstructFullTeam();
            //    if (FullProgram.Verbosity == 2)
            //    {
            //        Console.WriteLine($"Team Name: {team.Name} Team Overall: {team.Overall}");
            //        Console.WriteLine($"OLine Overall: {team.OLineOverall}, DLine Overall: {team.DLineOverall}");
            //    }
            //    ListTeams.Add(team);
            //}
        }
        public void SimulateSeason(int userChoice)
        {
            FillLeagueWithRandomTeams();
            ListTeams.Add(Team1);
            ListTeams.Add(Team2);
            ListTeams.Add(Team3);
            ListTeams.Add(Team4);
            ListTeams.Add(Team5);
            ListTeams.Add(Team6);
            ListTeams.Add(Team7);
            ListTeams.Add(Team8);
            ListTeams.Add(Team9);
            ListTeams.Add(Team10);
            for (int i = 0; i < 10; i++)
            {
                Standings.Add(ListTeams[i]);
            }
            for (int i = 1; i < 10; i++) // Play each week of the 9 game season
            {
                SimulateWeek(i, userChoice);
            }
            // Creates teams for keeping track of stats and info between games
            Team1.CalculateTeamPointDifferential();
            Team2.CalculateTeamPointDifferential();
            Team3.CalculateTeamPointDifferential();
            Team4.CalculateTeamPointDifferential();
            Team5.CalculateTeamPointDifferential();
            Team6.CalculateTeamPointDifferential();
            Team7.CalculateTeamPointDifferential();
            Team8.CalculateTeamPointDifferential();
            Team9.CalculateTeamPointDifferential();
            Team10.CalculateTeamPointDifferential();
            //PrintStandings();
            FinishAndPrintSeasonResults(userChoice);
        }
        public Team SimulateTournament(int userChoice)
        {
            Game game1 = new Game(SortedStandings[6], SortedStandings[9], userChoice);  // Round 1 play in game 
            game1.FullGame();
            SortedTourneyStandings.Add(game1.Loser);
            Game game2 = new Game(SortedStandings[7], SortedStandings[8], userChoice); // Round 1 play in game
            game2.FullGame();
            SortedTourneyStandings.Add(game2.Loser);
            Game game3 = new Game(SortedStandings[0], game2.Winner, userChoice);    // Round 2 Left Top Bracket
            game3.FullGame();
            SortedTourneyStandings.Add(game3.Loser);
            Game game4 = new Game(SortedStandings[1], game1.Winner, userChoice);    // Round 2  Right Top Bracket
            game4.FullGame();
            SortedTourneyStandings.Add(game4.Loser);
            Game game5 = new Game(SortedStandings[2], SortedStandings[5], userChoice);  // Round 2  // Right Bottom Bracket
            game5.FullGame();
            SortedTourneyStandings.Add(game5.Loser);
            Game game6 = new Game(SortedStandings[3], SortedStandings[4], userChoice);  // Round 2  // Left Bottom Bracket
            game6.FullGame();
            SortedTourneyStandings.Add(game6.Loser);
            Game game7 = new Game(game3.Winner, game6.Winner, userChoice);  // Semifinals Left Bracket
            game7.FullGame();
            SortedTourneyStandings.Add(game7.Loser);
            Game game8 = new Game(game4.Winner, game5.Winner, userChoice);  // Semifinals Right Bracket
            game8.FullGame();
            SortedTourneyStandings.Add(game8.Loser);
            Game game9 = new Game(game7.Winner, game8.Winner, userChoice);  // Championship Game
            game9.FullGame();
            SortedTourneyStandings.Add(game9.Loser);
            SortedTourneyStandings.Add(game9.Winner);
            SortedTourneyStandings.Reverse();
            return game9.Winner;

        }
        public void SimulateWeek(int week, int userChoice)
        {
            switch (week)
            {   // Simulates 5 games for 9 weeks, so everyone plays each other once

                case 1:
                    Game game1 = new Game(Team1, Team10, userChoice);
                    Game game2 = new Game(Team2, Team9, userChoice);
                    Game game3 = new Game(Team3, Team8, userChoice);
                    Game game4 = new Game(Team4, Team7, userChoice);
                    Game game5 = new Game(Team5, Team6, userChoice);
                    game1.FullGame();
                    game2.FullGame();
                    game3.FullGame();
                    game4.FullGame();
                    game5.FullGame();
                    break;
                case 2:
                    Game game6 = new Game(Team7, Team2, userChoice);
                    Game game7 = new Game(Team6, Team3, userChoice);
                    Game game8 = new Game(Team5, Team4, userChoice);
                    Game game9 = new Game(Team10, Team9, userChoice);
                    Game game10 = new Game(Team8, Team1, userChoice);
                    game6.FullGame();
                    game7.FullGame();
                    game8.FullGame();
                    game9.FullGame();
                    game10.FullGame();
                    break;
                case 3:
                    Game game11 = new Game(Team9, Team8, userChoice);
                    Game game12 = new Game(Team1, Team7, userChoice);
                    Game game13 = new Game(Team6, Team2, userChoice);
                    Game game14 = new Game(Team5, Team3, userChoice);
                    Game game15 = new Game(Team4, Team10, userChoice);
                    game11.FullGame();
                    game12.FullGame();
                    game13.FullGame();
                    game14.FullGame();
                    game15.FullGame();
                    break;
                case 4:
                    Game game16 = new Game(Team4, Team3, userChoice);
                    Game game17 = new Game(Team10, Team8, userChoice);
                    Game game18 = new Game(Team9, Team7, userChoice);
                    Game game19 = new Game(Team6, Team1, userChoice);
                    Game game20 = new Game(Team2, Team5, userChoice);
                    game16.FullGame();
                    game17.FullGame();
                    game18.FullGame();
                    game19.FullGame();
                    game20.FullGame();
                    break;
                case 5:
                    Game game21 = new Game(Team10, Team6, userChoice);
                    Game game22 = new Game(Team7, Team5, userChoice);
                    Game game23 = new Game(Team2, Team1, userChoice);
                    Game game24 = new Game(Team8, Team4, userChoice);
                    Game game25 = new Game(Team9, Team3, userChoice);
                    game21.FullGame();
                    game22.FullGame();
                    game23.FullGame();
                    game24.FullGame();
                    game25.FullGame();
                    break;
                case 6:
                    Game game26 = new Game(Team5, Team9, userChoice);
                    Game game27 = new Game(Team8, Team6, userChoice);
                    Game game28 = new Game(Team7, Team10, userChoice);
                    Game game29 = new Game(Team3, Team2, userChoice);
                    Game game30 = new Game(Team1, Team4, userChoice);
                    game26.FullGame();
                    game27.FullGame();
                    game28.FullGame();
                    game29.FullGame();
                    game30.FullGame();
                    break;
                case 7:
                    Game game31 = new Game(Team3, Team1, userChoice);
                    Game game32 = new Game(Team9, Team4, userChoice);
                    Game game33 = new Game(Team8, Team5, userChoice);
                    Game game34 = new Game(Team7, Team6, userChoice);
                    Game game35 = new Game(Team10, Team2, userChoice);
                    game31.FullGame();
                    game32.FullGame();
                    game33.FullGame();
                    game34.FullGame();
                    game35.FullGame();
                    break;
                case 8:
                    Game game36 = new Game(Team6, Team4, userChoice);
                    Game game37 = new Game(Team5, Team10, userChoice);
                    Game game38 = new Game(Team1, Team9, userChoice);
                    Game game39 = new Game(Team2, Team8, userChoice);
                    Game game40 = new Game(Team3, Team7, userChoice);
                    game36.FullGame();
                    game37.FullGame();
                    game38.FullGame();
                    game39.FullGame();
                    game40.FullGame();
                    break;
                case 9:
                    Game game41 = new Game(Team8, Team7, userChoice);
                    Game game42 = new Game(Team4, Team2, userChoice);
                    Game game43 = new Game(Team10, Team3, userChoice);
                    Game game44 = new Game(Team1, Team5, userChoice);
                    Game game45 = new Game(Team6, Team9, userChoice);
                    game41.FullGame();
                    game42.FullGame();
                    game43.FullGame();
                    game44.FullGame();
                    game45.FullGame();
                    break;
                default:
                    Console.WriteLine("Default Case entered");
                    break;
            }
        }
        public void PrintTourneyResults(Team team)
        {
            Console.WriteLine($"The Tournament Winner is: {team.Name} {team.Mascot}! Congratulations!");
            for (int i = 0; i < 10; i++)
            {
                Team team1 = SortedTourneyStandings[i];
                Console.WriteLine($"{i + 1}| {team1.Name} {team1.Mascot} {team1.Overall} Wins: {team1.Wins}, Losses: {team1.Losses}, PointsFor: {team1.TotalPoints}, PointsAgainst: {team1.TotalPointsAgainst}, PointDiff: {team1.TotalPointDifferential}");
            }
        }
        public void FinishAndPrintSeasonResults(int userChoice)
        {
            Console.WriteLine("Regular Season Results: ");
            CalculateStandings();
            for (int i = 0; i < 10; i++)
            {
                Team team = SortedStandings[i];
                Console.WriteLine($"{i + 1}| {team.Name} {team.Mascot} {team.Overall} Wins: {team.Wins}, Losses: {team.Losses}, PointsFor: {team.TotalPoints}, PointsAgainst: {team.TotalPointsAgainst}, PointDiff: {team.TotalPointDifferential}");
            }
            Thread.Sleep(5000); // Pauses program for 0.1 seconds so that user can see what computer does
            Team team1 = SimulateTournament(userChoice);

            PrintTourneyResults(team1);
        }
        public void PrintTourneyStandings()
        {

        }
        public void PrintStandings()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Final Standings: ");
            CalculateStandings();
            for (int i = 0; i < 10; i++)
            {
                Team team = SortedStandings[i];
                Console.WriteLine($"{i + 1}| {team.Name} {team.Mascot} {team.Overall} Wins: {team.Wins}, Losses: {team.Losses}, PointsFor: {team.TotalPoints}, PointsAgainst: {team.TotalPointsAgainst}, PointDiff: {team.TotalPointDifferential}");
            }
        }
        public void CalculateStandings()
        {   // Calculates the end of the year standings based on wins and then point differential. This determines the seeding for the tourney
            SortedStandings = Standings.OrderBy(Team => Team.Wins).ThenBy(Team => Team.TotalPointDifferential).ToList();
            SortedStandings.Reverse();  // Flips list around, because for some reason, it was backwards
        }
        public void CreateFreeAgents()
        {   // Creates 50 random players for there to be free agents
            // Someday, users will be able to add free agents to their team
            Create create = new Create();
            Player player = new Player();
            for (int i = 0; i < 50; i++)
            {
                player = create.CreateRandomPlayer();
                FreeAgents.Add(player);
            }
        }
        public void PrintOutFreeAgents()
        {   // Prints out the players that are not on a team
            FreeAgents = FreeAgents.OrderBy(Player => Player.Overall).ToList();
            FreeAgents.Reverse();
            foreach (Player player in FreeAgents)
            {
                player.printAllAttributes();
            }
        }
        public void FillLeagueWithManualTeams()
        {   // Allow user to create teams and then create a league of them
            ;
        }
    }

}

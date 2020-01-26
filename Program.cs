using System;
using System.Collections.Generic;
namespace FrisbeeGui
{
    public class Player
    {
        // Player Traits
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string JerseyNumber { get; set; }
        private int Height { get; set; }    // In inches
        private int Weight { get; set; }    // In pounds

        public Player(string fn, string ln, string jn)
        {
            FirstName = fn;
            LastName = ln;
            JerseyNumber = jn;
        }
    }
    public class Attributes : Player
    {
        private int Speed { get; set; }
        private int Jumping { get; set; }
        private int FlickDistance { get; set; }
        private int FlickAccuracy { get; set; }
        private int BackhandAccuracy { get; set; }
        private int BackhandDistance { get; set; }
        private int CutterDefense { get; set; }
        private int HandlerDefense { get; set; }
        private int Agility { get; set; }
        private int HandleCuts { get; set; }
        private int UnderCuts { get; set; }
        private int DeepCuts { get; set; }
        public Attributes(int s, int j, int fd, int fa, int ba, int bd, int cd, int hd, int ag, int hc, int uc, int dc)
        {   // Constructor
            Speed = s;
            Jumping = j;
            FlickDistance = fd;
            FlickAccuracy = fa;
            BackhandAccuracy = ba;
            BackhandDistance = bd;
            CutterDefense = cd;
            HandlerDefense = hd;
            Agility = ag;
            UnderCuts = uc;
            DeepCuts = dc;
        }
    }
        // Player Abilities
        
    }

    public class Team
    {
        private string Name;
        private string Mascot;
        private Player[] TeamOfPlayers = new Player[10];
        public Team(string n, string m)
        {
            Name = n;
            Mascot = m;
        }
    }

    public class RunProgram
    {
        Player player1 = new Player("Joel", "Stennett", "3", 92, 84, 74, 61, 82, 82, false, 0, 0);
        Player player2 = new Player("David", "Belvardi", "0", 82, 94, 81, 51, 72, 92, false, 0, 0);
        Player player3 = new Player("Jon", "Hinerman", "1", 62, 74, 94, 91, 92, 42, false, 0, 0);
        Team Jazz = new Team("Utah", "Jazz");


    }
}

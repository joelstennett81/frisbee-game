﻿using System;
using System.Collections.Generic;
using System.IO;
namespace Frisbeev01
{
    public class Player
    {
        // Player Traits
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JerseyNumber { get; set; }
        public int Overall { get; set; }
        private int Height { get; set; }    // In inches
        private int Weight { get; set; }    // In pounds
        private string filename2 = "Names.csv";
        public List<string> firstNameDatabase = new List<string>();
        public List<string> lastNameDatabase = new List<string>();
        public bool IsHandle { get; set; } // If is true, they are a handler
        public bool HasDisc { get; set; }   // If true, they have the disc
        private int Speed { get; set; }
        private int Jumping { get; set; }
        private int FlickDistance { get; set; }
        private int FlickAccuracy { get; set; }
        private int BackhandAccuracy { get; set; }
        private int BackhandDistance { get; set; }
        private int CutterDefense { get; set; }
        private int HandlerDefense { get; set; }
        private int Agility { get; set; }
        private int HandleCuts { get; set; }    // Not sure how to use it
        private int UnderCuts { get; set; } // Under and deep cuts determine cut ability
        private int DeepCuts { get; set; }
        public int ThrowAbility { get; set; }
        public int GameGoals { get; set; }
        public int SeasonGoals { get; set; }
        public int GameAssists { get; set; }
        public int SeasonAssists { get; set; }
        public int CutAbility { get; set; }
        public string TeamNameAndMascot { get; set; }
        public Player()
        {   // Empty Constructor
            ;
        }
        public Player(string fn, string ln, string jn, int s, int j, int fd, int fa, int ba, int bd, int cd, int hd, int ag, int hc, int uc, int dc)
        {   // Constructor 
            FirstName = fn;
            LastName = ln;
            JerseyNumber = jn;
            Speed = s;
            Jumping = j;
            FlickDistance = fd;
            FlickAccuracy = fa;
            BackhandAccuracy = ba;
            BackhandDistance = bd;
            CutterDefense = cd;
            HandlerDefense = hd;
            Agility = ag;
            HandleCuts = hc;
            UnderCuts = uc;
            DeepCuts = dc;
            ThrowAbility = (fd + fa + bd + ba) / 4;
            CutAbility = (uc + dc) / 2;
            Overall = (s + j + fd + fa + ba + bd + cd + hd + ag + hc + uc + dc) / 12;
        }
        public void printAbout(Player player)
        {
            Console.Write($"{FirstName} {LastName}, {JerseyNumber} ");
        }
        public void printAllAttributes()
        {
            Console.WriteLine("-------------------------------");
            Console.Write($"First Name: {FirstName} \nLast Name: {LastName} \nJersey Number: {JerseyNumber} \nOverall: {Overall} \n");
            Console.Write($"Speed: {Speed} \nJumping: {Jumping} \nFlick Distance: {FlickDistance} \nFlick Accuracy: {FlickAccuracy}\n");
            Console.Write($"Backhand Accuracy: {BackhandAccuracy} \nBackhand Distance: {BackhandDistance} \nCutter Defense: {CutterDefense} \nHandler Defense: {HandlerDefense}\n");
            Console.Write($"Agility: {Agility} \nHandle Cuts: {HandleCuts} \nUnder Cuts: {UnderCuts} \nDeep Cuts: {DeepCuts}\n");
        }
        public void printBasicInfo()
        {
            Console.WriteLine($"{FirstName} {LastName} {JerseyNumber} {Overall} ");
        }
        public List<string> ReadInFirstNames()
        {
            using (StreamReader reader = new StreamReader(filename2))
            {
                while (!reader.EndOfStream)
                {
                    // Read in individual lines, and then separate by commas
                    string line = reader.ReadLine();
                    string[] fields = line.Split(',');
                    // Add these fields to a job object, which is added to list
                    firstNameDatabase.Add(fields[0]);
                }
                return firstNameDatabase;
            }
        }
        public List<string> ReadInLastNames()
        {
            using (StreamReader reader = new StreamReader(filename2))
            {
                while (!reader.EndOfStream)
                {
                    // Read in individual lines, and then separate by commas
                    string line = reader.ReadLine();
                    string[] fields = line.Split(',');
                    // Add these fields to a job object, which is added to list
                    lastNameDatabase.Add(fields[1]);
                }
                return lastNameDatabase;
            }
        }
    }
}

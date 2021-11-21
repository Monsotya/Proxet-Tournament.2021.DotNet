using System;
using System.Collections.Generic;
using System.IO;

namespace Proxet.Tournament
{
    public class TeamGenerator
    {
        public (string[] team1, string[] team2) GenerateTeams(string filePath)
        {
            int counter = 0;
            bool condition = false;
            List<string> firstTeam = new List<string>(); 
            List<string> secondTeam = new List<string>(); 
            List<string[]> gamers = new List<string[]>();
            string[] gamer = new string[3];
            bool[][] checking = new bool[2][];
            checking[0] = new bool[9];
            checking[1] = new bool[9];

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    gamer = line.Split('	');
                    gamers.Add(gamer);
                }
            }

            gamers.Sort((x, y) => Int32.Parse(y[1]).CompareTo(Int32.Parse(x[1])));

            while (CheckGamers(checking))
            {
                int buffer = Int32.Parse(gamers[counter][2]);
                for (int j = 3 * buffer - 3; j < 3 * buffer; j++)
                {
                    if (!checking[0][j])
                    {
                        firstTeam.Add(gamers[counter][0]);
                        checking[0][j] = true;
                        condition = true;
                        break;
                    }
                }
                if (condition)
                {
                    condition = false;
                    counter++;
                    continue;
                }
                for (int j = 3 * buffer - 3; j < 3 * buffer; j++)
                {
                    if (!checking[1][j]) { 
                        secondTeam.Add(gamers[counter][0]);
                        checking[1][j] = true;
                        break;
                    }
                }
                counter++;
            }
            return (firstTeam.ToArray(), secondTeam.ToArray());
        }

        public static bool CheckGamers(bool[][] checking)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!checking[i][j])
                        return true;
                }
            }
            return false;
        }
    }
}
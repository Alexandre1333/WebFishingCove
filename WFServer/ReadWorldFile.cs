﻿using System.Text.RegularExpressions;

namespace WFServer
{
    internal class ReadWorldFile
    {

        // for reading the point positions from a .tscn file (main_zone.tscn)
        public static List<Vector3> readPoints(string nodeGroup, string file)
        {
            List<Vector3> points = new List<Vector3>();

            // split the file into lines
            string[] lines = file.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {

                Match isFishPoint = Regex.Match(lines[i], @"groups=\[([^\]]*)\]");
                if (isFishPoint.Success && isFishPoint.Groups[1].Value == $"\"{nodeGroup}\"")
                {
                    string transformPattern = @"Transform\(.*?,\s*(-?\d+\.?\d*),\s*(-?\d+\.?\d*),\s*(-?\d+\.?\d*)\s*\)";
                    Match match = Regex.Match(lines[i + 1], transformPattern);

                    string x = match.Groups[1].Value;
                    string y = match.Groups[2].Value;
                    string z = match.Groups[3].Value;

                    Vector3 thisPoint = new Vector3(float.Parse(x), float.Parse(y), float.Parse(z));
                    points.Add(thisPoint);
                }
            }

            Console.WriteLine($"Found {points.Count} points of group \"{nodeGroup}\"");

            return points;
        }

    }
}

using System.Collections.Generic;
using UnityEngine;

namespace GeekBrains.Helpers
{
    public static class ColorList
    {
        public static List<Color> ListColors = new List<Color>();

        static ColorList()
        {
            ListColors.Add(Color.black);
            ListColors.Add(Color.blue);
            ListColors.Add(Color.clear);
            ListColors.Add(Color.cyan);
            ListColors.Add(Color.gray);
            ListColors.Add(Color.green);
            ListColors.Add(Color.grey);
            ListColors.Add(Color.magenta);
            ListColors.Add(Color.red);
            ListColors.Add(Color.white);
            ListColors.Add(Color.yellow);
            ListColors.Add(Color.blue);
        }
    }
}
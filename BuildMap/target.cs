using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BuildMap
{
    public class Target : Point
    {
        public string name;
        public string description;
        public Target(string sourceStr)
        {
            string[] fields = sourceStr.Split(',');
            id = Convert.ToInt32(fields[0]);
            x = Convert.ToInt32(fields[1]);
            y = Convert.ToInt32(fields[2]);
            name = fields[3];
            description = fields[4];
        }
        public void print()
        {
            Console.WriteLine($"id = {id}, x = {x}, y = {y}, name = {name}, discription = {description}");
        }
    }
}

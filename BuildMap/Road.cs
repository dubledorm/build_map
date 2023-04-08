using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BuildMap
{
    public class Road
    {
        public int idS;
        public int idE;
        public int weight;
        public Road(string sourceStr)
        {
            string[] fields = sourceStr.Split(',');
            idS = Convert.ToInt32(fields[0]);
            idE = Convert.ToInt32(fields[1]);
            weight = Convert.ToInt32(fields[2]);
        }
        //public List<Roads> conectedLines(List<Roads> roads, Roads firstRoadP)
        //{
        //    List<Roads> result = new List<Roads>();
        //    foreach (var road in roads)
        //    {
        //        if(!(firstRoadP == road))
        //        {
        //            if(road.idS == firstRoadP.idS | road.idE == firstRoadP.idE)
        //            {
        //                result.Add(road);
        //                Console.WriteLine(road);
        //            }
        //        }
        //    }

        //    return result;
        //}

        public void print()
        {
            Console.WriteLine($"idS = {idS}, idE = {idE}, weight = {weight}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BuildMap
{
    public class PathStorage
    {
        List<Road> roads;
        List<List<Road>> pathes = new List<List<Road>>();
        PointIdSMapping point_ids_mapping;

        public List<List<Road>> Pathes
        {
            get { return pathes; }
        }
        public PathStorage(List<Road> roads, PointIdSMapping point_ids_mapping)
        {
            this.roads = roads;
            this.point_ids_mapping = point_ids_mapping;
            for (int i = 0; i < point_ids_mapping.Dimension; i++)
            {
                pathes.Add(new List<Road>());
            }
        }

        public void print()
        {
            Console.WriteLine();
            for(int i = 0; i < pathes.Count; i++)
            {
                Console.Write($"in ID {point_ids_mapping.point_id(i)}:\t");
                foreach(Road road in pathes[i])
                {
                    Console.Write($"{road.idS} <-> {road.idE}({road.weight}) | ");
                }
                Console.WriteLine();
            }
        }

        public void add(int start_index, int end_index, int weihgt, int target_index)
        {
            pathes[target_index].Add(findRoad(point_ids_mapping.point_id(start_index), point_ids_mapping.point_id(end_index), weihgt));
        }

        public void replace(int target_index, List<Road> source)
        {
            pathes[target_index] = new List<Road>(source);
        }

        private Road findRoad(int start_id, int end_id, int weight)
        {
            foreach(Road road in roads)
            {
                if(road.weight == weight && 
                    (road.idS == start_id && road.idE == end_id) || 
                    (road.idS == end_id && road.idE == start_id))
                {
                    return road;
                }
            }
            throw new Exception($"Не могу найти дугу с параметрами start_id - {start_id}, end_id {end_id}, weight {weight}");
        }
    }
}

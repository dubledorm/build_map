using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildMap
{
    public class PointEnumerator
    {
        int current_point_id;
        int current_road_index;
        List<Road> roads;
        List<Target> targets;
        int start_point_id;
        public PointEnumerator(List<Road> roads, List<Target> targets, int start_point_id)
        {
            this.roads = roads;
            this.targets = targets;
            this.start_point_id = start_point_id;
            current_point_id = -1;
        }

        public Point next()
        {
            if(current_point_id == -1)
            {
                current_point_id = start_point_id;
                current_road_index = 0;
                return find_point_by_id(start_point_id);
            }
            if (roads[current_road_index].idS == start_point_id)
                return find_point_by_id(roads[current_road_index].idE);
            else
                return find_point_by_id(roads[current_road_index].idS);

        }

        private Point find_point_by_id(int id)
        {
            return null;
        }
    }
}

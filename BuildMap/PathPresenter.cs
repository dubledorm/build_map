using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BuildMap
{
    public class PathPresenter
    {
        private Building building;
        private List<Road> result_roads;
        private int start_point_id;
        public PathPresenter(Building building, List<Road> result_roads, int start_point_id)
        {
            this.building = building;
            this.result_roads = result_roads;
            this.start_point_id = start_point_id;
        }
        public string toTargetList(List<Target> targets)
        {
            string result = $"<option value=\"http://127.0.0.1:8888/mapping/buildings/{building.id}/points/{targets[0]}\">Выберите точку</option>\n";
            foreach (Target target in targets)
            {
                result += $"<option value=\"{link(target)}\">{target.name}</option>\n";
            }
            return result;
            //http://127.0.0.1:8888/mapping/buildings/1/points/2/path?target=2
            //result += $"<option value=\"http://127.0.0.1:8888/mapping/buildings/{building}>{target.name}</option>";
        }
        public string toPoint()
        {
            string result = $"<circle cx=\"{building.targetByPointId(start_point_id).x}\" cy=\"{building.targetByPointId(start_point_id).y}\" r=\"10\" stroke=\"\" stroke-width=\"\" fill=\"red\" />";
            return result;
            //<circle cx="50" cy="50" r="10" stroke="" stroke-width="" fill="red" />
        }
        public string toPolyline()
        {
            int current_point_id = start_point_id;
            string result = "<polyline points=\"";
            Target target;
            foreach (Road road in result_roads)
            {
                target = building.targetByPointId(current_point_id);
                result += target.x + "," + target.y + " ";
                current_point_id = selectRemainingPoint(road, current_point_id);
            }
            result += building.targetByPointId(current_point_id).x + "," + building.targetByPointId(current_point_id).y + "\" stroke=\"red\" stroke-width=\"20\" stroke-linecap=\"round\" fill=\"none\" stroke-linejoin=\"round\"/>";
            return result;
        }//34 122 31 153 11 31 65
        private string link(Target target)
        {
            return $"http://127.0.0.1:8888/mapping/buildings/{building.id}/points/{0}/path?target={target.id}";
        }
        private int selectRemainingPoint(Road road, int current_point_id)
        {
            return road.idS != current_point_id ? road.idS : road.idE;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildMap
{
    public class TargetListPresenter
    {
        private Building building;
        private int start_point_id;
        private string host_name_with_port;

        public TargetListPresenter(Building building, int start_point_id, string host_name_with_port)
        {
            this.building = building;
            this.start_point_id = start_point_id;
            this.host_name_with_port = host_name_with_port;
        }
        public string toTargetList()
        {
            string result = "<option>Выберите точку</option>\n";
            foreach (Target target in building.possibleTargets(start_point_id))
            {   
                result += $"<option value=\"{link(target)}\">{target.name}</option>\n";
            }
            return result;
            //http://127.0.0.1:8888/mapping/buildings/1/points/2/path?target=2
            //result += $"<option value=\"http://127.0.0.1:8888/mapping/buildings/{building}>{target.name}</option>";
        }

        private string link(Target target)
        { 
            return $"http://{host_name_with_port}/mapping/buildings/{building.id}/points/{start_point_id}/path?target_id={target.id}";
        }
    }
}

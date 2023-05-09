using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BuildMap
{
    public class TextPresenter
    {
        List<Road> result_roads;
        List<Target> targets;
        public TextPresenter(List<Road> result_roads, List<Target> targets, int start_point_id)
        {
            this.result_roads = result_roads;
            this.targets = targets;
        }

        public string way_by_text()
        {
            string result = string.Empty;
            for (int i = 0; i < result_roads.Count; i++)
            {
                if (targets[result_roads[i].idS].x < targets[result_roads[i].idE].x)
                {
                    result += "поверните налево и дойдите до ";
                }
            }
            return result;
        }
        private string store_or_crossroad(int idE)
        {
            string result = string.Empty;
            if (targets[idE].name == "nothing+")
            {

            }
            return ""; 
        }

    }
}

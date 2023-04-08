using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildMap
{
    public class PointPresenter
    {
        private Building building;
        private int start_point_id;

        public PointPresenter(Building building, int start_point_id)
        {
            this.building = building;
            this.start_point_id = start_point_id;
        }

        public string toPoint()
        {
            string result = $"<circle cx=\"{building.targetByPointId(start_point_id).x}\" cy=\"{building.targetByPointId(start_point_id).y}\" r=\"10\" stroke=\"\" stroke-width=\"\" fill=\"red\" />";
            return result;
        }
    }
}

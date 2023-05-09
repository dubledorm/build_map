
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
            result += building.targetByPointId(current_point_id).x + "," + building.targetByPointId(current_point_id).y + "\" stroke=\"red\" stroke-width=\"10\" stroke-linecap=\"round\" fill=\"none\" stroke-linejoin=\"round\"/>";
            return result;
        }//34 122 31 153 11 31 65

        private int selectRemainingPoint(Road road, int current_point_id)
        {
            return road.idS != current_point_id ? road.idS : road.idE;
        }
    }
}

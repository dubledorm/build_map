using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BuildMap
{
    public class Building
    {
        const string directName = @"C:\Buildings\";
        const string baseHtmlName = "base.html";
        const string svgName = "buildingMapSvg.svg";
        const string targetName = "targetsCSV.csv";
        const string roadsName = "roads.csv";
        public Svg svg;// исходная картинка помещения
        public List<Target> targets;
        public List<Road> roads;
        public int id;
        public Building(int building_id)
        {
            this.id = building_id;
            svg = new Svg(svgFilePath());
            targets = new List<Target>();
            roads = new List<Road>();
            fillTargetList();
            fillRoadsList();
        }
        public List<Target> possibleTargets(int start_point_id)
        {
            List<Target> result = new List<Target>();
            foreach(Target target in targets)
                if(target.name != " NOTSHOP" && target.id != start_point_id)
                    result.Add(target);
            return result;
        }
        public string baseHtmlPath()
        {
            return Path.Combine(directName, baseHtmlName);
        }
        public Target targetByPointId(int point_id)
        {
            foreach (Target target in targets)
            {
                if(target.id == point_id)
                {
                    return target;
                }
            }
            throw new Exception($"Не могу найти target с id = {point_id}");
        }
        private void fillTargetList()
        {
            StreamReader sr = new StreamReader(targetsFilePath());

            string str = sr.ReadLine();

            while (str != null)
            {
                Target target = new Target(str);

                targets.Add(target);
                str = sr.ReadLine();
            }
        }
        private void fillRoadsList()
        {
            StreamReader sr = new StreamReader(roadsFilePath());

            string str = sr.ReadLine();

            while (str != null)
            {
                Road road = new Road(str);

                roads.Add(road);
                str = sr.ReadLine();
            }
        }
        public string svgFilePath()
        {
            return Path.Join(directName, Convert.ToString(id), svgName);
        }
        private string targetsFilePath()
        {
            return Path.Combine(directName, Convert.ToString(id), targetName);
        }
        private string roadsFilePath()
        {
            return Path.Combine(directName, Convert.ToString(id), roadsName);
        }

    }
}

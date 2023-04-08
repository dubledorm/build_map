using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BuildMap
{
    public class RequestHandler
    {
        public const string base_url = "mapping";
        public enum request_type_value
        {
            point_request,
            path_request
        }
        private request_type_value request_type;
        private int building_id;
        private int start_point_id;
        private int end_point_id;
        private Building building;
        public RequestHandler(HttpListenerRequest request)
        {
            requestParse(request);
            building = new Building(building_id);
        }
        
        private string build_html_with_point()
        {
            PathFinder path_finder = new PathFinder(building.roads);
            PathPresenter path_presenter = new PathPresenter(building, path_finder.find(start_point_id, end_point_id), start_point_id);
            string svg = algorithm.addPointInSvg(building.svgFilePath(), path_presenter);
            string html_with_point = File.ReadAllText(building.baseHtmlPath());
            html_with_point = html_with_point.Replace("#[svgKey]", svg);
            html_with_point = html_with_point.Replace("#[selectionKey]", path_presenter.toTargetList(building.possibleTargets(start_point_id)));
            return html_with_point;
        }
        private string build_html_with_path()
        {
            PathFinder pathFinder = new PathFinder(building.roads);
            PathPresenter path_presenter = new PathPresenter(building, null, start_point_id);
            string svg = algorithm.addPathInSvg(building.svgFilePath(), path_presenter);
            string html_with_path = File.ReadAllText(building.baseHtmlPath());
            //html_with_path.Replace("  <img src=\"buildingMapSvg.svg\">", svg);
            html_with_path = html_with_path.Replace("#[svgKey]", svg);
            html_with_path = html_with_path.Replace("#[selectionKey]", path_presenter.toTargetList(building.possibleTargets(start_point_id)));
            return html_with_path;
        }

        public string handler()
        {
            switch (request_type)
            {
                case request_type_value.point_request:
                        return build_html_with_point();
                case request_type_value.path_request:
                        return build_html_with_path();
                default:
                        throw new Exception("Получен неизвестный тип запроса");
            }
        }
        private void requestParse(HttpListenerRequest request)
        {
            if (request.Url.Segments.Length < 6 || request.Url.Segments[2].ToLower() != "buildings/" || request.Url.Segments[4].ToLower() != "points/")
            {
                throw new Exception($"Неправильно передан Url ");
            }

            building_id = int.Parse(request.Url.Segments[3].TrimEnd('/'));
            end_point_id = int.Parse(request.Url.Segments[5].TrimEnd('/'));
            request_type = request_type_value.point_request;

            for (int i = 0; i < request.Url.Segments.Length; i++)
            {
                Console.WriteLine($"request.Url.Segments[{i}] = {request.Url.Segments[i]}");
            }

            if (request.Url.Segments.Length < 7 || request.Url.Segments[6].ToLower() != "path")
                return;

            string target_id = parseParams(request.Url.Query);
            end_point_id = int.Parse(target_id);
            request_type = request_type_value.path_request;
        }

        private string parseParams(string param_string)
        {
            string pattern = @"\?target=(?<id>\d+)";
            MatchCollection match_collection = Regex.Matches(param_string, pattern, RegexOptions.IgnoreCase);

            if (match_collection.Count == 0)
            {
                return "";
            }

            return match_collection[0].Groups[1].Value;
        }

        //http://127.0.0.1:8888/mapping/buildings/1/points/2/path?target=2
    }
}

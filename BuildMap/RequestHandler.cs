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
        public const string prefix_url = "http://*:8080";
        public const string point_url_regexp = @"^\/mapping\/buildings\/(?<build_id>\d+)\/points\/(?<point_id>\d+)$";
        public const string path_url_regexp = @"^\/mapping\/buildings\/(?<build_id>\d+)\/points\/(?<point_id>\d+)\/path\?target_id=(?<target_id>\d+)$";
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
        private string host_name_with_port;


        public RequestHandler(HttpListenerRequest request)
        {
            requestParse(request);
            building = new Building(building_id);
            host_name_with_port = request.UserHostName;
        }
        
        private string build_html_with_point()
        {
            string svg = building.svg.ReplaceKeyWithString(new PointPresenter(building, start_point_id).toPoint());

            string html = File.ReadAllText(building.baseHtmlPath());
            html = html.Replace("#[svgKey]", svg);
            html = html.Replace("#[targetListKey]", new TargetListPresenter(building, start_point_id, host_name_with_port).toTargetList());
            return html;
        }
        private string build_html_with_path()
        {
            PathFinder path_finder = new PathFinder(building.roads);
            PathPresenter path_presenter = new PathPresenter(building, path_finder.find(start_point_id, end_point_id), start_point_id);
            string svg = algorithm.addPathInSvg(building.svgFilePath(), path_presenter);

            return build_html(svg);
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

        private string build_html(string svg)
        {
            string html = File.ReadAllText(building.baseHtmlPath());
            html = html.Replace("#[svgKey]", svg);
            html = html.Replace("#[targetListKey]", new TargetListPresenter(building, start_point_id, host_name_with_port).toTargetList());
            return html;
        }


        private void requestParse(HttpListenerRequest request)
        {
            if (request.RawUrl == null)
                throw new Exception($"Неправильно передан Url ");

            Match match = Regex.Match(request.RawUrl, path_url_regexp, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                building_id = int.Parse(match.Groups["build_id"].Value);
                start_point_id = int.Parse(match.Groups["point_id"].Value);
                end_point_id = int.Parse(match.Groups["target_id"].Value);
                request_type = request_type_value.path_request;
                return;
            }

            match = Regex.Match(request.RawUrl, point_url_regexp, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                building_id = int.Parse(match.Groups["build_id"].Value);
                start_point_id = int.Parse(match.Groups["point_id"].Value);
                return;
            }

            throw new Exception($"Неправильно передан Url ");
        }

        //http://127.0.0.1:8888/mapping/buildings/1/points/2/path?target=2
    }
}

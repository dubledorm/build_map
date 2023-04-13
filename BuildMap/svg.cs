
using System.Text.RegularExpressions;


namespace BuildMap
{
    public class Svg
    {
        private string svg_file_path;

        public Svg(string svg_file_path) { 
            this.svg_file_path = svg_file_path;
        }
        public string ReplaceKeyWithString(string value_for_replace) // Функция
        {
            string svg = File.ReadAllText(svg_file_path);
            svg = svg.Replace("#[key]", value_for_replace);
            svg = Regex.Replace(svg, @"svg\s*width=""\d+""\s*height=""\d+""", @"svg width=""100%"" height=""auto""");
            return svg;
        }
    }
}

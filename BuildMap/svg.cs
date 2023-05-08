
using System.Text.RegularExpressions;


namespace BuildMap
{
    public class Svg
    {
        private string svg_file_path;
        private const string height_width_regexp = @"svg\s*width=""(?<width>\d+)""\s*height=""(?<height>\d+)""";
        private const string view_box_regexp = @"viewBox=""\d+ \d+ \d+ \d+""";

        public Svg(string svg_file_path) { 
            this.svg_file_path = svg_file_path;
        }

        // Функция для вставки в svg файл новых элементов, таких как точки и линии маршрута
        public string ReplaceKeyWithString(string value_for_replace)
        {
            string svg = File.ReadAllText(svg_file_path);
            svg = NormalizeSvg(svg);
            svg = svg.Replace("#[key]", value_for_replace);
            return svg;
        }

        private string NormalizeSvg(string svg)
        {
            string result = svg;

            Match match = Regex.Match(svg, height_width_regexp, RegexOptions.IgnoreCase);

            if (!match.Success)
                return result;

            // Получаем ширину и высоту 
            string width = match.Groups["width"].Value;
            string height = match.Groups["height"].Value;

            // Проверяем, что есть viewBox и удаляем его
            result = Regex.Replace(result, view_box_regexp, "");

            // Заменяем ширину и высоту на 100% и пишем новый viewBox
            result = Regex.Replace(result, height_width_regexp, @$"svg width=""100%"" height=""100%"" viewBox=""0 0 {width} {height}""");
            
            return result;
        }

    }
}

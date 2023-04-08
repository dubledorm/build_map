using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return svg;
        }
    }
}

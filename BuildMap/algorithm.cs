using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildMap
{
    public class algorithm
    {
        public static pathnweight goThroughGraph(int id1, int id2, Road[] roads)
        {
            //
            return new pathnweight();
        }

        public List<Road> graph(List<Road> roads, Road firstRoadP, int ids, int ide)
        {
            List<Road> result = new List<Road>();
            foreach (var road in roads)
            {
                if (!(firstRoadP == road))
                {
                    if (road.idS == ids | road.idE == ide)
                    {
                        result.Add(road);
                        Console.WriteLine(road);
                    }
                }
            }
            return result;
        }

        public static string addPathInSvg(string svg_file_path, PathPresenter path_presenter) // Функция
        {
            string svg = File.ReadAllText(svg_file_path);
            svg = svg.Replace("#[key]", path_presenter.toPolyline());
            return svg;
        }
        public static string addPointInSvg(string svg_file_path, PathPresenter path_presenter) // Функция
        {
            string svg = File.ReadAllText(svg_file_path);
            svg = svg.Replace("#[key]", path_presenter.toPoint());
            return svg;
        }
    }
}
//public static string[] drawOnMap(List<int> coordinatesList, string path) // Функция
//{
//    string coordinates = string.Empty; // Пустая переменная
//    foreach (int j in coordinatesList) // Конвертировать массив в string
//    {
//        coordinates += j; // Добавить координату
//        coordinates += " "; // Добавить пробел
//    }
//    coordinates = coordinates.Trim(); // Удалить лишний пробел
//    bool i = false; // Нет полилинии в конце?
//    int len = File.ReadAllLines(path).Length; // Кол-во строк в файле
//    string[] arrLine = File.ReadAllLines(path); // Прочитать файл
//    if (!arrLine[len - 2].Contains("polyline")) { arrLine[len - 1] = $"<polyline points=\"{coordinates}\" stroke=\"red\" stroke-width=\"20\" stroke-linecap=\"round\" fill=\"none\" stroke-linejoin=\"round\"/>"; i = true; } // Полилиния в конце не обнаружена; создание новой полилинии на месте </svg>
//    else { arrLine[len - 2] = $"<polyline points=\"{coordinates}\" stroke=\"red\" stroke-width=\"20\" stroke-linecap=\"round\" fill=\"none\" stroke-linejoin=\"round\"/>"; } // Полилиния в конце обнаружена; перезапись старой полилинии
//    if (i == true) { arrLine[len - 1] += "\n</svg>"; } // Добавляет </svg>, если был заменён новой полилинией
//    File.WriteAllLines(path, arrLine); // Запись строк в файл
//                                       //if (i == true) { File.AppendAllLines(path, new[] { "</svg>" }); } // Добавляет </svg>, если был заменён новой полилинией
//    return arrLine;
//}
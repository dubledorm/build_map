using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BuildMap
{
    public class RoadMatrix
    {
        private cell[,] matrix;
        public RoadMatrix(int dimension, List<Road> roads)
        {
            matrix = new cell[dimension, dimension];
            fillMatrixIn(roads);
            print(dimension);
        }
        private void fillMatrixIn(List<Road> roads)
        {
            foreach (Road r in roads)
            {
                matrix[r.idS - 1, r.idE - 1] = new cell(r.weight);
                matrix[r.idE - 1, r.idS - 1] = new cell(r.weight);
            }
        }
        private void print(int dimension)
        {
            for(int i = 0; i < dimension; i++)
            {
                for(int j = 0; j < dimension; j++)
                {
                    if (matrix[i, j] == null)
                        Console.Write("0\t");
                    else
                        Console.Write($"{matrix[i, j].weight}\t");
                }Console.WriteLine();
            }
        }

        public static List<int> findOptimalRoad(int fR, int sR, int dimension)
        {
            const int UNAVAILABLE = 1000;
            const int length = 4;
            int[,] WEIGHTS = new int[length,length] { { 0, 2, UNAVAILABLE, UNAVAILABLE },
                                            { 2, 0, 4, UNAVAILABLE },
                                            { UNAVAILABLE, 4, 0, 6 },
                                            { UNAVAILABLE, UNAVAILABLE, 6, 0 } };

            int START = 2; // Номер вершины от которой ищем маршруты
            
            bool[] marks = new bool[length];// Здесь помечаем пройденные вершины
            int[] results = new int[length];// Сюда записываются минимальные веса от стартовой вершины до всех остальных
            List<List<int>> path = new List<List<int>>();// Сюда записываем найденные маршруты

            // Определяем сколько у нас всего точек
            int points_quantity = length;

            // Инициализируем массив меток
            for(int i = 0; i < points_quantity - 1; i++)
            {
                marks[i] = false;
            }

            // Инициализируем массив маршрутов
            for (int i = 0; i < points_quantity - 1; i++)
            {
                path.Add(new List<int>());
            }

            // Помечаем стартовую точку как пройденную
            marks[START] = true;

            // Инициализируем массив результатов (копируем сюда стартовые веса)
            for (int i = 0; i < length; i++)
                for (int j = 0; j < length; j++)
                    results[i] = WEIGHTS[j, i];

            // Инициализируем массив маршрутов (копируем сюда первый переход из стартовой точки в существующего соседа)
            for (int i = 0; i < length - 1; i++)
                if (results[i] != UNAVAILABLE)
                {
                    path[i].Add(START);
                    path[i].Add(i);
                }

            // Цикл пока есть не помеченные вершины
            while (Array.Exists(marks, element => element.Equals(false)))
            {
                // Ищем минимальный посчитанный маршрут среди не помеченных точек
                int min_result_point_number = -1;
                for(int number_in_result = 0; number_in_result < points_quantity - 1; number_in_result++)
                {
                    if (marks[number_in_result])
                        continue;
                    if(min_result_point_number == -1 || results[min_result_point_number] > results[number_in_result])
                    {
                        min_result_point_number = number_in_result;
                    }
                }
                // Помечаем найденную вершину как посещённую
                marks[min_result_point_number] = true;

                // Проверяем, что от неё нет более короткого пути к найденным
                for (int number_in_result = 0; number_in_result < points_quantity - 1; number_in_result++)
                {
                    if (results[number_in_result] > results[min_result_point_number] + WEIGHTS[min_result_point_number, number_in_result])
                    {
                        path[number_in_result] = new List<int>(path[min_result_point_number]);
                        path[number_in_result].Add(number_in_result);
                    }
                }
            }
            //
                List<int> bebe = new List<int>();
            return bebe;
        }
    }
}

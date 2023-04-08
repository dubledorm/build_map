using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BuildMap
{
    public class PathFinder
    {
        private int[,] weights_matrix; // Таблица весов дуг для расчёта
        private PointIdSMapping point_ids_mapping; // Список id точек
        public int Dimension { get { return point_ids_mapping.Dimension; } }
        private List<Road> roads;
        const int UNAVAILABLE = 100000;
        private PathStorage path_storage; // Сюда сохраняем пройденные маршруты
        public PathFinder(List <Road> roads)
        {
            this.roads = roads;

            point_ids_mapping = new PointIdSMapping(roads);

            path_storage = new PathStorage(roads, point_ids_mapping);

            // Создаём и заполняем матрицу весов
            weights_matrix = new int[Dimension, Dimension];
            fillWeightMatrix();
            printWeightMatrix();
        }

        public List<Road> find(int start_point_id, int end_point_id)
        {
            int start_index = point_ids_mapping.map(start_point_id);
            int end_index = point_ids_mapping.map(end_point_id);

            bool[] marks = new bool[Dimension];// Здесь помечаем пройденные вершины
            int[] result_weights = new int[Dimension];// Сюда записываются минимальные веса от стартовой вершины до всех остальных

            // Инициализируем массив меток
            for (int i = 0; i < Dimension - 1; i++)
            {
                marks[i] = false;
            }

            // Помечаем стартовую точку как пройденную
            marks[start_index] = true;

            // Инициализируем массив результатов (копируем сюда стартовые веса)
            for (int j = 0; j < Dimension; j++)
                result_weights[j] = weights_matrix[start_index, j];

            // Инициализируем массив маршрутов (копируем сюда первый переход из стартовой точки в существующего соседа)
            for (int i = 0; i < Dimension; i++)
                if (result_weights[i] != UNAVAILABLE && result_weights[i] != 0)
                {
                    path_storage.add(start_index, i, result_weights[i], i);
                }

            // Цикл пока есть не помеченные вершины
            while (Array.Exists(marks, element => element.Equals(false)))
            {
                // Ищем минимальный посчитанный маршрут среди не помеченных точек
                int min_result_point_number = -1;
                for (int number_in_result = 0; number_in_result < Dimension; number_in_result++)
                {
                    if (marks[number_in_result])
                        continue;
                    if (min_result_point_number == -1 || result_weights[min_result_point_number] > result_weights[number_in_result])
                    {
                        min_result_point_number = number_in_result;
                    }
                }
                // Помечаем найденную вершину как посещённую
                marks[min_result_point_number] = true;

                // Проверяем, что от неё нет более короткого пути к найденным
                for (int number_in_result = 0; number_in_result < Dimension; number_in_result++)
                {
                    if (result_weights[number_in_result] > result_weights[min_result_point_number] + weights_matrix[number_in_result, min_result_point_number])
                    {
                        result_weights[number_in_result] = result_weights[min_result_point_number] + weights_matrix[min_result_point_number, number_in_result];
                        path_storage.replace(number_in_result, path_storage.Pathes[min_result_point_number]);
                        path_storage.add(min_result_point_number, number_in_result, weights_matrix[min_result_point_number, number_in_result], number_in_result);
                    }
                }
            }
          //  printResult(result_weights);
            return path_storage.Pathes[end_index];
        }

        private void printResult(int[] result_weights)
        {
            for (int i = 0; i < Dimension; i++)
            {
                Console.WriteLine($"id - {point_ids_mapping.point_id(i)} result weight {i}: {result_weights[i]}");
            }
            path_storage.print();
        }
        private void printWeightMatrix()
        {
            Console.Write("\n\t");
            for(int i = 0; i < Dimension; i++)
                Console.Write($"{point_ids_mapping.point_id(i)}\t");
            Console.WriteLine();
            for (int i = 0; i < Dimension; i++)
            {
                Console.Write($"{point_ids_mapping.point_id(i)}\t");
                for (int j = 0; j < Dimension; j++)
                {
                    Console.Write($"{weights_matrix[i, j]}\t");
                }
                Console.WriteLine();
            }
        }

        private void fillWeightMatrix()
        {
            for (int i = 0; i < Dimension; i++)
                for (int j = 0; j < Dimension; j++)
                {
                    if (i == j)
                        weights_matrix[i, i] = 0;
                    else
                        weights_matrix[i, j] = UNAVAILABLE;
                }
            foreach (Road road in roads)
            {
                weights_matrix[point_ids_mapping.map(road.idS), point_ids_mapping.map(road.idE)] = road.weight;
                weights_matrix[point_ids_mapping.map(road.idE), point_ids_mapping.map(road.idS)] = road.weight;
            }
        }
    }
}

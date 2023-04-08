using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildMap
{
    // задачей класса является сохранение массива id точек и преобразования id к индексу в этом массиве
    public class PointIdSMapping
    {
        private int dimension; // Количество точек
        public int Dimension { get { return dimension; } } // Определяет dimension только для чтения
        private int[] point_ids; // Список id иочек для преобразования к индексу в weights_matrix
        public PointIdSMapping(List<Road> roads)
        {
            // Считаем точки и заносим из в point_ids
            point_ids = new int[roads.Count * 2];
            fillPointIdsFromRoads(roads);

            //
            Console.Write($"dimension: {dimension}\npoints id:");
            for (int i = 0; i < dimension; i++)
            {
                Console.Write($" {point_ids[i]}");
            }
            //
        }
        // Вернуть индекс id в массиве point_ids
        public int map(int point_id)
        {
            for(int i = 0; i < dimension; i++)
                if (point_ids[i] == point_id)
                    return i;
            throw new Exception($"Передан неизвестный id точки: {point_id}");
        }

        public int point_id(int index)
        {
            return point_ids[index];
        }
        private void fillPointIdsFromRoads(List<Road> roads)
        {
            dimension = 0;
            foreach (Road road in roads)
            {
                addToPointIdS(road.idS);
                addToPointIdS(road.idE);
            }
        }
        private void addToPointIdS(int id)
        {
            for (int i = 0; i < dimension; i++)
                if (point_ids[i] == id)
                    return;
            point_ids[dimension] = id;
            dimension++;
        }
    }
}

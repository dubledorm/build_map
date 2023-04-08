using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildMap
{
    public class cell
    {
        public int weight;
        public int rights = 0;//0 == NULL, 1 == start
        public cell(int weight)
        {
            this.weight = weight;
        }
    }
}

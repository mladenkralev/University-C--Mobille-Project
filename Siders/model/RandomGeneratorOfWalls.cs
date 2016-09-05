using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siders
{
    class RandomGeneratorOfWalls
    {
        private Random random = new Random();

        public RandomGeneratorOfWalls()
        {
        }

        public int generateNumberOfWalls(int buttom , int upper)
        {
            int value = random.Next(buttom, upper);
            return value;
        }

        public int generateTypeOfWalls(int buttom, int upper)
        {
            int value = random.Next(buttom, upper);
            return value;
        }



        public int  generateTopOfHorizontalWalls(int buttom, int upper)
        {
            int value = random.Next(buttom, upper);
            return value;
        }

        public int generateLeftOfHorizontalWalls(int buttom, int upper)
        {
            int value = random.Next(buttom, upper);
            return value;
        }



        public int generateTopOfVerticalWalls(int buttom, int upper)
        {
            int value = random.Next(buttom, upper);
            return value;
        }

        public int generateLeftOfVerticalWalls(int buttom, int upper)
        {
            int value = random.Next(buttom, upper);
            return value;
        }
    }
}

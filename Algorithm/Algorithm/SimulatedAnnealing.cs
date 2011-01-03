using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithm
{
    class SimulatedAnnealing
    {
        public string StartAnnealing()
        {
            TspDataReader.computeData();
            ArrayList list = new ArrayList();
            //primary configuration of cities
            int[] current = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            //the next configuration of cities to be tested
            int[] next = new int[15];
            int iteration = -1;
            //the probability
            double proba;
            double alpha = 0.999;
            double temperature = 400.0;
            double epsilon = 0.001;
            double delta;
            double distance = TspDataReader.computeDistance(current);

            //while the temperature did not reach epsilon
            while (temperature > epsilon)
            {
                iteration++;

                //get the next random permutation of distances 
                computeNext(current, next);
                //compute the distance of the new permuted configuration
                delta = TspDataReader.computeDistance(next) - distance;
                //if the new distance is better accept it and assign it
                if (delta < 0)
                {
                    assign(current, next);
                    distance = delta + distance;
                }
                else
                {
                    proba = rnd.Next();
                    //if the new distance is worse accept 
                    //it but with a probability level
                    //if the probability is less than 
                    //E to the power -delta/temperature.
                    //otherwise the old value is kept
                    if (proba < Math.Exp(-delta / temperature))
                    {
                        assign(current, next);
                        distance = delta + distance;
                    }
                }
                //cooling process on every iteration
                temperature *= alpha;
                //print every 400 iterations
                if (iteration % 400 == 0)
                    Console.WriteLine(distance);
            }
            try
            {
                return "best distance is" + distance;
            }
            catch
            {
                return "error";
            }
        }

        /// <summary>
        /// compute a new next configuration
        /// and save the old next as current
        /// </summary>
        /// <param name="c">current configuration</param>
        /// <param name="n">next configuration</param>
        void computeNext(int[] c, int[] n)
        {
            for (int i = 0; i < c.Length; i++)
                n[i] = c[i];
            int i1 = (int)(rnd.Next(14)) + 1;
            int i2 = (int)(rnd.Next(14)) + 1;
            int aux = n[i1];
            n[i1] = n[i2];
            n[i2] = aux;
        }
    }
}

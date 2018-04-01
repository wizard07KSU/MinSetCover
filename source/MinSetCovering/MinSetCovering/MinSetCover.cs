using MinSetCoveringUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinSetCovering
{
    class MinSetCover
    {
        static void Main(string[] args)
        {
            int[] set;
            List<ICollection<int>> coverings = new List<ICollection<int>>();
            int minSetSize = 1000;
            int maxSetSize = 100000;
            double lowerRangeBound = 0.6d;
            double upperRangeBound = 0.8d;

            Random rand = new Random();

            int setSize = rand.Next(minSetSize, maxSetSize);
            set = new int[setSize];

            // Init set with values
            for (int i = 0; i < set.Length; i++)
            {
                set[i] = i;
            }

            // Generate random coverings
            int numCoveringSets = rand.Next(5000, 50000);
            int globalMinValue = (int)Math.Round(setSize * lowerRangeBound);
            int globalMaxValue = (int)Math.Round(setSize * upperRangeBound);
            for (int i = 0; i < numCoveringSets; i++)
            {
                // each covering will cover between 60% and 80%, rounded to the nearest integers
                int randomSetSize = rand.Next(globalMinValue, globalMaxValue);

                // Clone the set, then randomly select from the clone without replacement.
                List<int> setList = set.ToList();

                int[] covering = new int[randomSetSize];
                for (int j = 0; j < randomSetSize; j++)
                {
                    int randomIndex = rand.Next(setList.Count);
                    int element = setList[randomIndex];
                    setList.RemoveAt(randomIndex);
                    covering[j] = element;
                }
                List<int> coveringList = covering.ToList();
                coveringList.Sort();
                coverings.Add(coveringList);
            }

            // Build data structures
            MscPriorityQueue<int, int, ICollection<ICollection<int>>> priorityQueue =
                new MscPriorityQueue<int, int, ICollection<ICollection<int>>>();

            foreach (int i in set)
            {
                Dictionary<int, ICollection<ICollection<int>>> elementToCoveringMap = 
                    new Dictionary<int, ICollection<ICollection<int>>>();
                List<ICollection<int>> specificCoverings = new List<ICollection<int>>();

                // Find all coverings which contain the current node.
                foreach (List<int> iCovering in coverings)
                {
                    if (iCovering.BinarySearch( i ) >= 0)
                    {
                        specificCoverings.Add(iCovering);
                    }
                }

                priorityQueue.Add(
                    sortKey  : specificCoverings.Count,
                    lookupKey: i,
                    data     : specificCoverings );
            }


        }
    }
}

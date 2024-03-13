using System;
using ASD.Graphs;
using System.Collections.Generic;

namespace ASD
{
    public class Lab04 : MarshalByRefObject
    {
        /// <summary>
        /// Etap 1 - wyznaczanie numerów grup, które jest w stanie odwiedzić Karol, zapisując się na początku do podanej grupy
        /// </summary>
        /// <param name="graph">Ważony graf skierowany przedstawiający zasady dołączania do grup</param>
        /// <param name="start">Numer grupy, do której początkowo zapisuje się Karol</param>
        /// <returns>Tablica numerów grup, które może odwiedzić Karol, uporządkowana rosnąco</returns>
        public int[] Lab04Stage1(DiGraph<int> graph, int start)
        {
            // TODO
            List<int> visited = new List<int>();
            Queue<(int vertex, int prev)> queue = new Queue<(int vertix, int prev)>();
            
            queue.Enqueue((start, -1));
            while (queue.Count != 0)
            {
                var vert = queue.Dequeue();
                foreach (var edge in graph.OutEdges(vert.vertex))
                {
                    if (vert.prev == edge.Weight)
                    {
                        queue.Enqueue((edge.To, vert.vertex));
                    }
                }
                if (!visited.Contains(vert.vertex))
                    visited.Add(vert.vertex);
            }
            visited.Sort();
            return visited.ToArray();
        }

        /// <summary>
        /// Etap 2 - szukanie możliwości przejścia z jednej z grup z `starts` do jednej z grup z `goals`
        /// </summary>
        /// <param name="graph">Ważony graf skierowany przedstawiający zasady dołączania do grup</param>
        /// <param name="starts">Tablica z numerami grup startowych (trasę należy zacząć w jednej z nich)</param>
        /// <param name="goals">Tablica z numerami grup docelowych (trasę należy zakończyć w jednej z nich)</param>
        /// <returns>(possible, route) - `possible` ma wartość true gdy istnieje możliwość przejścia, wpp. false, 
        /// route to tablica z numerami kolejno odwiedzanych grup (pierwszy numer to numer grupy startowej, ostatni to numer grupy docelowej),
        /// jeżeli possible == false to route ustawiamy na null</returns>
        public (bool possible, int[] route) Lab04Stage2(DiGraph<int> graph, int[] starts, int[] goals)
        {
            // TODO
            return (false, null);
        }
    }
}

using System;
using System.Collections;
using ASD.Graphs;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
// using System.Reflection.Metadata;

namespace ASD
{
    public class Lab04 : MarshalByRefObject
    {
        /// <summary>
        /// Etap 1 - Szukanie mozliwych do odwiedzenia miast z grafu skierowanego
        /// przy zalozeniu, ze pociagi odjezdzaja co godzine.
        /// </summary>
        /// <param name="graph">Graf skierowany przedstawiający siatke pociagow</param>
        /// <param name="miastoStartowe">Numer miasta z ktorego zaczyna sie podroz pociagiem</param>
        /// <param name="K">Godzina o ktorej musi zakonczyc sie nasza podroz</param>
        /// <returns>Tablica numerow miast ktore mozna odwiedzic. Posortowana rosnaco.</returns>
        public int[] Lab04Stage1(DiGraph graph, int miastoStartowe, int K)
        {
            // TODO
            List<int> visited = new List<int>();
            bool[] ifVisited = new bool[graph.VertexCount];
            
            Queue< (int city, int hour)> q = new Queue<(int city, int hour)>();
            q.Enqueue((miastoStartowe, 8));
            while (q.Count != 0)
            {
                var vert = q.Dequeue();
                
                if (!ifVisited[vert.city])
                {
                    ifVisited[vert.city] = true;
                    visited.Add(vert.city);
                }
                if (vert.hour == K)
                    continue;
                
                foreach (var neighbour in graph.OutNeighbors(vert.city))
                {
                    q.Enqueue((neighbour, vert.hour + 1));
                }
            }
            var res = visited.ToArray();
            Array.Sort(res);
            
            return res;
        }

        /// <summary>
        /// Etap 2 - Szukanie mozliwych do odwiedzenia miast z grafu skierowanego.
        /// Waga krawedzi oznacza, ze pociag rusza o tej godzinie
        /// </summary>
        /// <param name="graph">Wazony graf skierowany przedstawiający siatke pociagow</param>
        /// <param name="miastoStartowe">Numer miasta z ktorego zaczyna sie podroz pociagiem</param>
        /// <param name="K">Godzina o ktorej musi zakonczyc sie nasza podroz</param>
        /// <returns>Tablica numerow miast ktore mozna odwiedzic. Posortowana rosnaco.</returns>
        public int[] Lab04Stage2(DiGraph<int> graph, int miastoStartowe, int K)
        {
            // TODO
            int it = 0;
            List<int> visited = new List<int>();
            bool[] ifVisited = new bool[graph.VertexCount];
            int[] hourVisited = new int[graph.VertexCount];
            for (int i = 0; i < hourVisited.Length; i++)
                hourVisited[i] = Int32.MaxValue;

            DiGraph<int> copy = new DiGraph<int>(graph.VertexCount, graph.Representation);
            foreach (var edge in graph.BFS().SearchAll())
            {
                copy.AddEdge(edge.From, edge.To, edge.Weight);
            }
            
            Queue< (int city, int hour)> q = new Queue<(int city, int hour)>();
            
            q.Enqueue((miastoStartowe, 8));
            hourVisited[miastoStartowe] = 8;
            while (q.Count != 0)
            {
                var vert = q.Dequeue();
                
                if (!ifVisited[vert.city])
                {
                    ifVisited[vert.city] = true;
                    visited.Add(vert.city);
                }
                if (vert.hour == K)
                    continue;
                
                foreach (var edge in copy.OutEdges(vert.city))
                {
                    if (edge.Weight >= K)
                    {
                        copy.RemoveEdge(edge.From, edge.To);
                        continue;
                    }
                    
                    if (edge.Weight >= vert.hour)
                    {
                        if (edge.Weight + 1 < hourVisited[edge.To])
                        {
                            hourVisited[edge.To] = edge.Weight + 1;
                            q.Enqueue((edge.To, edge.Weight + 1));
                        }
                        else
                            copy.RemoveEdge(edge.From, edge.To);
                    }

                    it++;
                }
            }

            visited.Sort();
            
            return visited.ToArray();
        }
    }
}

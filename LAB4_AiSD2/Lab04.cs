﻿using System;
using System.Collections;
using ASD.Graphs;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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
            int[] hourVisited = new int[graph.VertexCount];
            for (int i = 0; i < hourVisited.Length; i++)
                hourVisited[i] = Int32.MaxValue;
            
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
                    if (vert.hour + 1 < hourVisited[neighbour])
                    {
                        hourVisited[neighbour] = vert.hour + 1;
                        q.Enqueue((neighbour, vert.hour + 1));
                    }
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
            List<int> visited = new List<int>();
            bool[] ifVisited = new bool[graph.VertexCount];
            Queue< (int city, int hour)> q = new Queue<(int city, int hour)>();
            DiGraph<int> edgesGraph = new DiGraph<int>(graph.VertexCount, graph.Representation);
            int[] hourVisited = new int[graph.VertexCount];
            for (int i = 0; i < hourVisited.Length; i++)
                hourVisited[i] = Int32.MaxValue;
            
            q.Enqueue((miastoStartowe, 8));
            while (q.Count != 0)
            {
                var vert = q.Dequeue();
                
                if (!ifVisited[vert.city])
                {
                    ifVisited[vert.city] = true;
                    visited.Add(vert.city);
                }
                if (vert.hour >= K)
                    continue;
                
                foreach (var edge in graph.OutEdges(vert.city))
                {
                    if (edge.Weight >= vert.hour && edge.Weight < K)
                        if (edge.Weight < hourVisited[edge.To])
                            if (edgesGraph.AddEdge(edge.From, edge.To, edge.Weight))
                            {
                                hourVisited[vert.city] = edge.Weight + 1;
                                q.Enqueue((edge.To, edge.Weight + 1));
                            }
                }
            }

            visited.Sort();
            
            return visited.ToArray();
        }
    }
}

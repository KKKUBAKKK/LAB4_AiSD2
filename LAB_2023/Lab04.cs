using System;
using ASD.Graphs;
using System.Collections.Generic;
using System.Linq;

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
            // CO ZE ZLOZONOSCIA???
            
            // WERSJA Z BFS I Z LISTA (MOGLABY BYC Z HASHSET)
            // List<int> visited = new List<int>();
            // Queue<(int vertex, int prev)> queue = new Queue<(int vertix, int prev)>();
            //
            // queue.Enqueue((start, -1));
            // while (queue.Count != 0)
            // {
            //     var vert = queue.Dequeue();
            //     foreach (var edge in graph.OutEdges(vert.vertex))
            //     {
            //         if (vert.prev == edge.Weight)
            //             queue.Enqueue((edge.To, vert.vertex));
            //     }
            //     if (!visited.Contains(vert.vertex))
            //         visited.Add(vert.vertex);
            //
            //     // Czy potrzebne???
            //     if (visited.Count == graph.VertexCount)
            //         break;
            // }
            // visited.Sort();
            //
            // return visited.ToArray();

            // WERSJA Z DFS I HASHSET (MOGLOBY BYC Z LISTA)
            HashSet<int> visited = new HashSet<int>();
            Stack<(int vertex, int prev)> stack = new Stack<(int vertex, int prev)>();
            
            stack.Insert((start, -1));
            while (stack.Count != 0)
            {
                var vert = stack.Extract();
                foreach (var edge in graph.OutEdges(vert.vertex))
                {
                    if (vert.prev == edge.Weight)
                        stack.Insert((edge.To, vert.vertex));
                }
                visited.Add(vert.vertex);

                // Czy potrzebne???
                if (visited.Count == graph.VertexCount)
                    break;
            }
            var res = visited.ToArray();
            Array.Sort(res);
            
            return res;
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
            
            // TO NIE DZIALA, ALE MOZE DALOBY SIE POPRAWIC (ROBIENIE SCIEZKI W CZASIE SZUAKANIA)
            //     bool possible = false;
            //     List<int> finalPath = new List<int>();
            //     foreach (var start in starts)
            //     {
            //         List<int> path = new List<int>();
            //         Stack<(int vertex, int prev)> stack = new Stack<(int vertex, int prev)>();
            //         
            //         stack.Insert((start, -1));
            //         while (stack.Count != 0)
            //         {
            //             var vert = stack.Extract();
            //             foreach (var edge in graph.OutEdges(vert.vertex))
            //             {
            //                 if (vert.prev == edge.Weight)
            //                     stack.Insert((edge.To, vert.vertex));
            //             }
            //
            //             if (path.Count > 0 && vert.prev != path[path.Count - 1])
            //             {
            //                 // znajduje pierewsze wystapienie a ja chce ostatnie/niewiadomo jakie???
            //                 int ind = path.FindIndex((int i) =>
            //                 {
            //                     // if (i != 0 && path[i - 1] == graph.GetEdgeWeight(path[i], vert.prev))
            //                     //     return false;
            //                     return i == vert.prev;
            //                 });
            //                 path.RemoveRange(ind + 1, path.Count  - ind - 1);
            //             }
            //             
            //             path.Add(vert.vertex);
            //
            //             if (goals.Contains(path[path.Count - 1]))
            //             {
            //                 possible = true;
            //                 finalPath = path;
            //                 break;
            //             }
            //         }
            //
            //         if (possible)
            //             break;
            //     }
            //     
            //     if (!possible)
            //         return (false, null);
            //
            //     return (true, finalPath.ToArray());

            // WERSJA Z ODTWARZANIEM SCIEZKI
            bool possible = false;
            int prev = -1;
            List<int> visited = new List<int>();
            List<int> path = new List<int>();
            
            foreach (var start in starts)
            {
                visited = new List<int>();
                Stack<(int vertex, int prev)> stack = new Stack<(int vertex, int prev)>();

                stack.Insert((start, -1));
                while (stack.Count != 0)
                {
                    var vert = stack.Extract();
                    foreach (var edge in graph.OutEdges(vert.vertex))
                    {
                        if (vert.prev == edge.Weight)
                            stack.Insert((edge.To, vert.vertex));
                    }

                    if (!visited.Contains(vert.vertex))
                        visited.Add(vert.vertex);

                    if (goals.Contains(visited[visited.Count - 1]))
                    {
                        prev = vert.prev;
                        possible = true;
                        break;
                    }

                    // Czy potrzebne???
                    if (visited.Count == graph.VertexCount)
                        break;
                }

                if (possible)
                    break;
            }

            if (!possible)
                return (false, null);

            var curr = visited[visited.Count - 1];
            while (prev != -1)
            {
                var pprev= graph.GetEdgeWeight(prev, curr);
                path.Add(curr);
                curr = prev;
                prev = pprev;
            }
            path.Add(curr);
            path.Reverse();
            
            return (true, path.ToArray());
        }
    }
}

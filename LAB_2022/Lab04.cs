using System;
using ASD.Graphs;
using ASD;
using System.Collections.Generic;

namespace ASD
{

    public class Lab04 : System.MarshalByRefObject
    {
        /// <summary>
        /// Etap 1 - szukanie trasy z miasta start_v do miasta end_v, startując w dniu day
        /// </summary>
        /// <param name="g">Ważony graf skierowany będący mapą</param>
        /// <param name="start_v">Indeks wierzchołka odpowiadającego miastu startowemu</param>
        /// <param name="end_v">Indeks wierzchołka odpowiadającego miastu docelowemu</param>
        /// <param name="day">Dzień startu (w tym dniu należy wyruszyć z miasta startowego)</param>
        /// <param name="days_number">Liczba dni uwzględnionych w rozkładzie (tzn. wagi krawędzi są z przedziału [0, days_number-1])</param>
        /// <returns>(result, route) - result ma wartość true gdy podróż jest możliwa, wpp. false, 
        /// route to tablica z indeksami kolejno odwiedzanych miast (pierwszy indeks to indeks miasta startowego, ostatni to indeks miasta docelowego),
        /// jeżeli result == false to route ustawiamy na null</returns>
        public (bool result, int[] route) Lab04_FindRoute(DiGraph<int> g, int start_v, int end_v, int day, int days_number)
        {
            // TODO
            System.Collections.Generic.List<int> route = new List<int>();
            Stack<(int city, int day, int parents)> stack = new Stack<(int, int, int)>();
            
            stack.Insert((start_v, day, 0));
            while (stack.Count != 0)
            {
                var stop = stack.Extract();

                if (route.Count > stop.parents)
                    route.RemoveRange(stop.parents - 1, route.Count - stop.parents);
                
                route.Add(stop.city);
                if (route[^1] == end_v)
                    break;
                
                foreach (var edge in g.OutEdges(stop.city))
                {
                    if ((stop.parents == 0 && edge.Weight <= stop.day) || (stop.parents != 0 && edge.Weight > stop.day))
                        stack.Insert((edge.To, edge.Weight, stop.parents + 1));
                }
            }

            if (route[^1] != end_v)
                return (false, null);

            // List<int> route = new List<int>();
            // var city = stops[^1].city;
            // var prev = stops[^1].prevCity;
            // route.Add(city);
            // while (prev != -1)
            // {
            //     
            // }
            // route.Reverse();
            
            return (true, route.ToArray());
        }

        /// <summary>
        /// Etap 2 - szukanie trasy z jednego z miast z tablicy start_v do jednego z miast z tablicy end_v (startować można w dowolnym dniu)
        /// </summary>
        /// <param name="g">Ważony graf skierowany będący mapą</param>
        /// <param name="start_v">Tablica z indeksami wierzchołków startowych (trasę trzeba zacząć w jednym z nich)</param>
        /// <param name="end_v">Tablica z indeksami wierzchołków docelowych (trasę trzeba zakończyć w jednym z nich)</param>
        /// <param name="days_number">Liczba dni uwzględnionych w rozkładzie (tzn. wagi krawędzi są z przedziału [0, days_number-1])</param>
        /// <returns>(result, route) - result ma wartość true gdy podróż jest możliwa, wpp. false, 
        /// route to tablica z indeksami kolejno odwiedzanych miast (pierwszy indeks to indeks miasta startowego, ostatni to indeks miasta docelowego),
        /// jeżeli result == false to route ustawiamy na null</returns>
        public (bool result, int[] route) Lab04_FindRouteSets(DiGraph<int> g, int[] start_v, int[] end_v, int days_number)
        {
            // TODO
            return (false, null);
        }
    }
}

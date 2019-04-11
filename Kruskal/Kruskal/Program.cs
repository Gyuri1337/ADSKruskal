using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Kurskal_SpanningTree
{
    struct Edge //struktúra na hrany orientované
    {
        public int source;
        public int destination;
        public int weight;
    }
    class Program  // hlavny program
    {
        static void Main(string[] args)
        {
            Graph mygraph = new Graph();
            mygraph.LoadAndCreateGraph("input.txt");
            mygraph.KruskalsSpanningTree();
        }

        public class Graph  //clasa na graf
        {
            public Edge[] edges;
            public int numberofvertices;
            public int[] father;
            public void LoadAndCreateGraph(String dir) // nacita graf z vstupneho subor input.txt
            {
                using (var sr = new StreamReader(dir))
                {
                    String line = sr.ReadLine();  //**************** Nacitanie*******************
                    String[] data = line.Split(' ');
                    int[] dataasint = new int[2];
                    int o = 0;
                    for (int z = 0; z < data.Length; z++)
                    {
                        if (data[z] != "")
                        {
                            dataasint[o] = int.Parse(data[z]);
                            o++;
                        }
                    }
                    numberofvertices = dataasint[0];
                    edges = new Edge[dataasint[1]];
                    dataasint = new int[3];
                    for (int i = 0; i < edges.Length; i++)
                    {
                        line = sr.ReadLine();
                        data = line.Split(' ');
                        int j = 0;
                        for (int z = 0; z < data.Length; z++)
                        {
                            if (data[z] != "")
                            {
                                dataasint[j] = int.Parse(data[z]);
                                j++;
                            }
                        }
                        edges[i].source = dataasint[0];
                        edges[i].destination = dataasint[1];
                        edges[i].weight = dataasint[2];
                    }
                    father = new int[numberofvertices]; //************koniec nacitanie**********
                }
            }
            public void KruskalsSpanningTree() //Kruskal...
            {
                for (int j = 0; j < numberofvertices; j++)//priradim kazdemu vrcholu otca sameho seba
                {
                    father[j] = j;
                }
                Array.Sort<Edge>(edges, (x, y) => x.weight.CompareTo(y.weight)); // sort hran
                List<Edge> Ready = new List<Edge>();
                int i = 0;
                while (Ready.Count < numberofvertices - 1) // vyskusaj vsetky hrany 
                {
                    if (FatherOf(edges[i].source) != FatherOf(edges[i].destination))// aknie su v jednom komponente tak prirad
                    {
                        Ready.Add(edges[i]);
                        father[FatherOf(edges[i].destination)] = edges[i].source;
                    }
                    i++;
                }
                VypisGraph(Ready);//vypisanie
            }
            private void VypisGraph(List<Edge> edges) // funkcia na vypisanie (metoda)
            {
                for (int i = 0; i < numberofvertices - 1; i++)
                {
                    Console.Write(edges[i].source + " " + edges[i].destination + " -- " + edges[i].weight);
                    Console.WriteLine();
                }
            }
            private int FatherOf(int x) // rekurzivne zisti otca a podla toho vieme ci su v jednom komponente, kazda komponenta ma jedneho "korenoveho otca"

            {
                if (father[x] == x)
                {
                    return x;
                }
                else
                    return FatherOf(father[x]);
            }

        }
    }
}
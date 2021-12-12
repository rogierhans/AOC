using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2
{
    class Grid
    {

        //public static List<(int,int)> NeighborList(int i, int j) { }


        List<List<string>> Cells = new List<List<string>>();
        List<List<bool>> Valid = new List<List<bool>>();






        public Grid(List<List<string>> cells, Dictionary<string, bool> Walkable)
        {
            Cells = cells;
            Valid = Cells.GridSelect(x => Walkable.ContainsKey(x) && Walkable[x]);
        }

        public void BFS(int startX, int startY)
        {
            Queue<(int, int)> q = new Queue<(int, int)>();
            q.Enqueue((startX, startY));
            while (q.Count > 0)
            {
                var position = q.Dequeue();
                //
            }
        }

    }
}

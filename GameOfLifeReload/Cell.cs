using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeReload
{
    public class Cell
    {
        public Cell(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }
        public void SetLive()
        {
            IsLive = true;
        }

        public bool IsLive { get; protected set; }
        public int PositionX { get; protected set; }
        public int PositionY { get; protected set; }
        public int NeighborsLive { get; protected set; }

        public void Search(Cell[][] word)
        {
            NeighborsLive = word[PositionX][PositionY - 1].IsLive ? NeighborsLive + 1 : NeighborsLive;
            NeighborsLive = word[PositionX][PositionY + 1].IsLive ? NeighborsLive + 1 : NeighborsLive;
        }
    }
}

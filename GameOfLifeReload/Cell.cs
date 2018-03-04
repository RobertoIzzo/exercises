using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeReload
{

    public interface ICell
    {
        void Search(Cell[][] word);
        void Convert(Cell[][] word);
    }

    public class Cell
    {

        public bool IsLive { get; protected set; }
        public int PositionX { get; protected set; }
        public int PositionY { get; protected set; }
        public int NeighborsLive { get; protected set; }

        public Cell(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }

        public void SetLive()
        {
            IsLive = true;
        }

        public void Search(Cell[][] word)
        {
            //top
            if (word[PositionX - 1][PositionY] != null)
                NeighborsLive = word[PositionX - 1][PositionY].IsLive ? NeighborsLive + 1 : NeighborsLive;

            //bottom
            if (word[PositionX + 1][PositionY] != null)
                NeighborsLive = word[PositionX + 1][PositionY].IsLive ? NeighborsLive + 1 : NeighborsLive;

            //sin
            if (word[PositionX][PositionY - 1] != null)
                NeighborsLive = word[PositionX][PositionY - 1].IsLive ? NeighborsLive + 1 : NeighborsLive;

            //dest
            if (word[PositionX][PositionY + 1] != null)
                NeighborsLive = word[PositionX][PositionY + 1].IsLive ? NeighborsLive + 1 : NeighborsLive;

            //top sx 
            if (word[PositionX - 1][PositionY - 1] != null)
                NeighborsLive = word[PositionX - 1][PositionY - 1].IsLive ? NeighborsLive + 1 : NeighborsLive;

            //bottom sx 
            if (word[PositionX + 1][PositionY - 1] != null)
                NeighborsLive = word[PositionX + 1][PositionY - 1].IsLive ? NeighborsLive + 1 : NeighborsLive;

            //top dx 
            if (word[PositionX - 1][PositionY + 1] != null)
                NeighborsLive = word[PositionX - 1][PositionY + 1].IsLive ? NeighborsLive + 1 : NeighborsLive;

            //bottom dx 
            if (word[PositionX + 1][PositionY + 1] != null)
                NeighborsLive = word[PositionX + 1][PositionY - +1].IsLive ? NeighborsLive + 1 : NeighborsLive;

        }

        public void Convert(Cell[][] word)
        {
            Search(word);
            if (!IsLive && NeighborsLive == 3)
                IsLive = true;
            if (IsLive && NeighborsLive < 2)
                IsLive = false;
            if (IsLive && NeighborsLive > 3)
                IsLive = false;
        }
    }
}

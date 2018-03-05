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
        void Convert();
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
            CheckTop(word);

            //bottom
            CheckBottom(word);

            //sin
            ChecKLeft(word);

            //dest
            CheckRight(word);

            //top sx 
            CheckTopLeft(word);

            //bottom sx 
            CheckBottomLeft(word);

            //top dx 
            CheckTopRight(word);

            //bottom dx 
            CheckBottomRight(word);

        }

        private void CheckBottomRight(Cell[][] word)
        {
            try
            {
                if (word[PositionX + 1][PositionY + 1] != null)
                    NeighborsLive = word[PositionX + 1][PositionY + 1].IsLive ? NeighborsLive + 1 : NeighborsLive;
            }
            catch (Exception)
            {
                    
                return;
            }
           
        }

        private void CheckTopRight(Cell[][] word)
        {
            try
            {
                if (word[PositionX - 1][PositionY + 1] != null)
                    NeighborsLive = word[PositionX - 1][PositionY + 1].IsLive ? NeighborsLive + 1 : NeighborsLive;
            }
            catch (Exception)
            {

                return;
            }
           
        }

        private void CheckBottomLeft(Cell[][] word)
        {
            try
            {
                if (word[PositionX + 1][PositionY - 1] != null)
                    NeighborsLive = word[PositionX + 1][PositionY - 1].IsLive ? NeighborsLive + 1 : NeighborsLive;
            }
            catch (Exception)
            {

                return;
            }
            
        }

        private void CheckTopLeft(Cell[][] word)
        {
            try
            {
                if (word[PositionX - 1][PositionY - 1] != null)
                    NeighborsLive = word[PositionX - 1][PositionY - 1].IsLive ? NeighborsLive + 1 : NeighborsLive;
            }
            catch (Exception)
            {

                return;
            }
            
        }

        private void CheckRight(Cell[][] word)
        {
            try
            {
                if (word[PositionX][PositionY + 1] != null)
                    NeighborsLive = word[PositionX][PositionY + 1].IsLive ? NeighborsLive + 1 : NeighborsLive;
            }
            catch (Exception)
            {

                return;
            }
           
        }

        private void ChecKLeft(Cell[][] word)
        {
            try
            {
                if (word[PositionX][PositionY - 1] != null)
                    NeighborsLive = word[PositionX][PositionY - 1].IsLive ? NeighborsLive + 1 : NeighborsLive;
            }
            catch (Exception)
            {

                return;
            }
         
        }

        private void CheckBottom(Cell[][] word)
        {
            try
            {
                if (word[PositionX + 1][PositionY] != null)
                    NeighborsLive = word[PositionX + 1][PositionY].IsLive ? NeighborsLive + 1 : NeighborsLive;
            }
            catch (Exception)
            {

                return;
            }
           
        }

        private void CheckTop(Cell[][] word)
        {
            try
            {
                if (word[PositionX - 1][PositionY] != null)
                    NeighborsLive = word[PositionX - 1][PositionY].IsLive ? NeighborsLive + 1 : NeighborsLive;
            }
            catch (Exception)
            {

                return;
            }
           
        }

        public void Convert()
        {
            if (!IsLive && NeighborsLive == 3)
                IsLive = true;
            if (IsLive && NeighborsLive < 2)
                IsLive = false;
            if (IsLive && NeighborsLive > 3)
                IsLive = false;
        }
    }
}

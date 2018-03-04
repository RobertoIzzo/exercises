using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeReload;
using Xunit;

namespace GameOfLifeReloadTest
{
    [Category("Unit")]
    public class GameTest
    {
        [Fact]
        public void GivenLiveCell_WhenSetLive_ReturnLive()
        {
            //ARRANGE
            Cell cell = new Cell(1,1);

            //ACT
            cell.SetLive();

            //ASSERT   
            Assert.True(cell.IsLive);
        }

        [Fact]
        public void Given2LiveNeighbors_WhenCellSearchNeighborsLive_Return2LiveNeighbors()
        {
            //ARRANGE
            Cell sut = new Cell(1,1);
            Cell[][] word = Mother.Given2LiveNeighbors(sut);

            //ACT
            sut.Search(word);

            //ASSERT
            Assert.True(sut.NeighborsLive == 2);
        }

        [Fact]
        public void Given3LiveNeighbors_WhenCellSearchNeighborsLive_Return3LiveNeighbors()
        {
            //ARRANGE
            Cell sut = new Cell(1, 1);
            Cell[][] word = Mother.Given3LiveNeighbors(sut);

            //ACT
            sut.Search(word);

            //ASSERT
            Assert.True(sut.NeighborsLive == 3);
        }

        [Fact]
        public void GivenDeadCellWith3LiveNeighbors_WhenConvert_ReturnIsAlveTrue()
        {
            //ARRANGE
            Cell sut = new Cell(1, 1);
            Cell[][] word = Mother.Given3LiveNeighbors(sut);
            
            //ACT
            sut.Convert(word);

            //ASSERT
            Assert.True(sut.IsLive);
        }

        [Fact]
        public void GivenLiveCellWith1LiveNeighbors_WhenConvert_ReturnIsAlveFalse()
        {
            //ARRANGE
            Cell sut = new Cell(1, 1);
            Cell[][] word = Mother.Given1LiveNeighbors(sut);
            sut.SetLive();

            //ACT
            sut.Convert(word);

            //ASSERT
            Assert.False(sut.IsLive);
        }

        [Fact]
        public void GivenLiveCellWith4LiveNeighbors_WhenConvert_ReturnIsAlveFalse()
        {
            //ARRANGE
            Cell sut = new Cell(1, 1);
            Cell[][] word = Mother.Given4LiveNeighbors(sut);
            sut.SetLive();

            //ACT
            sut.Convert(word);

            //ASSERT
            Assert.False(sut.IsLive);
        }

    }

    public class Mother
    {
        public static Cell[][] Given1LiveNeighbors(Cell cell)
        {
            Cell[][] word = new Cell[3][];
            word[0] = new Cell[3];
            word[1] = new Cell[3];
            word[2] = new Cell[3];
            word[1][1] = cell;

            Cell left = new Cell(1, 0);
            left.SetLive();
            word[1][0] = left;
            Cell right = new Cell(1, 2);
            word[1][2] = right;
            return word;
        }

        public static Cell[][] Given2LiveNeighbors(Cell cell)
        {
            Cell[][] word = new Cell[3][];
            word[0] = new Cell[3];
            word[1] = new Cell[3];
            word[2] = new Cell[3];
            word[1][1] = cell;

            Cell left = new Cell(1,0);
            left.SetLive();
            word[1][0] = left;
            Cell right = new Cell(1,2);
            right.SetLive();
            word[1][2] = right;
           return word;
        }

        public static Cell[][] Given3LiveNeighbors(Cell cell)
        {
            Cell[][] word = new Cell[3][];
            word[0] = new Cell[3];
            word[1] = new Cell[3];
            word[2] = new Cell[3];
            word[1][1] = cell;

            Cell bottomsx = new Cell(2, 0);
            bottomsx.SetLive();
            word[2][0] = bottomsx;

            Cell left = new Cell(1,0);
            left.SetLive();
            word[1][0] = left;

            Cell topsx = new Cell(0, 0);
            topsx.SetLive();
            word[0][0] = topsx;
            return word;
        }

        public static Cell[][] Given4LiveNeighbors(Cell cell)
        {
            Cell[][] word = new Cell[3][];
            word[0] = new Cell[3];
            word[1] = new Cell[3];
            word[2] = new Cell[3];
            word[1][1] = cell;

            Cell bottomsx = new Cell(2, 0);
            bottomsx.SetLive();
            word[2][0] = bottomsx;

            Cell left = new Cell(1, 0);
            left.SetLive();
            word[1][0] = left;

            Cell topsx = new Cell(0, 0);
            topsx.SetLive();
            word[0][0] = topsx;

            Cell top = new Cell(0,1);
            top.SetLive();
            word[0][1] = top;
            return word;
        }
    }
}

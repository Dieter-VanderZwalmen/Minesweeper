using Model.MineSweeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cells;

namespace ViewModel
{
    public class RowViewModel
    {
        //public IEnumerable<Square> row;


        //haal uit game de row

        public IEnumerable<SquareViewModel> Squares 
        {
            get;
        }
        /*
        public RowViewModel(IEnumerable<SquareViewModel> squares)
        {
            this.Squares = squares;
        }*/

        public RowViewModel(ICell<IGame> game, IEnumerable<SquareViewModel> row)
       {
            //var degame = game.Derive(g => g.Board) ;//hier krijg ik geen rij uit
            this.Squares = row;
       }


    }
}

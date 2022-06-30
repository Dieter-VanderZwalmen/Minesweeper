using Model.Data;
using Model.MineSweeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cells;

namespace ViewModel
{
    public class GameBoardViewModel
    {
        public readonly ICell<IGameBoard> gameboard;


        public IEnumerable<RowViewModel> Rows { get; }

        

        public GameBoardViewModel(ICell<IGame> game)
        {

            this.gameboard = game.Derive(g => g.Board);
            this.Rows = RowsMaker(game);
        }


        //returns a list of all square on given row 
        public RowViewModel Row(ICell<IGame> game, int row)
        {
            var rij = new List<SquareViewModel>();               //resultaat

            for (int i = 0; i < game.Value.Board.Width; i++)
            {

            
                var position = new Vector2D(i, row);
                var square2 = new SquareViewModel(game, position) ;
                
                rij.Add(square2);                        //bij resultaat een square toevoegen

            }

            var RowViewModel = new RowViewModel(game,rij);   

            return RowViewModel;                                 //return resultaat


        }

        //returns a list of all rows

        public IEnumerable<RowViewModel> RowsMaker(ICell<IGame> game)
        {
            /*
            get
            {
                for (int i = 0; i < gameboard.Height; i++)
                {    //voor elke rij van het board
                    var lijst = Row(gameboard, i);

                    yield return new RowViewModel(lijst);             
                }
            }*/

            
                var boardlist = new List<RowViewModel>();
                   
                for (int i = 0; i < game.Value.Board.Height; i++)
                {    //voor elke rij van het board
                    var lijst = Row(game, i);
                    boardlist.Add(lijst);


                }
                return boardlist;
            
        }



    }
}

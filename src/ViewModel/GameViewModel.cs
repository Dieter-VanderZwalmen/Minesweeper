using Model.Data;
using Model.MineSweeper;
using Cells;
using System.Diagnostics;

namespace ViewModel
{
    public class GameViewModel
    {
        private readonly ICell<IGame> Game;

        public GameBoardViewModel Board
        {
            get;
        }
        public ICell<GameStatus> GameStatus { get; }



        public GameViewModel(IGame game)
        { 
            this.Game = Cell.Create(game);  
            this.Board = new GameBoardViewModel(this.Game);
            this.GameStatus = this.Game.Derive(g => g.Status);

            this.Game.ValueChanged += () => Debug.WriteLine("Game changed");

        }

      

       
    }
}
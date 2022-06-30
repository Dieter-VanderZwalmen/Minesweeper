using Model.MineSweeper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Model.Data;
using Cells;

namespace ViewModel
{
    public class SquareViewModel
    {
        //public IEnumerable<Square> row;

        public ICell<Square> Square
        {
            get;
        }
        public ICell<SquareStatus> SquareStatus { get; }
        public ICommand Uncover 
        { 
            get;
        }
        public ICommand ToggleFlag
        {
            get;
        }
        public Vector2D Position
        {
            get;
        }

        public SquareViewModel(ICell<IGame> game,Vector2D position)
        {
            this.Position = position;

            this.Square = game.Derive(g => g.Board[position]);

            this.SquareStatus = game.Derive(g => 
            {
                if (g.Status == GameStatus.Lost && g.Mines.Contains(position))
                {
                    return Model.MineSweeper.SquareStatus.Mine;
                }

                else 
                {
                    return g.Board[position].Status;
                }
            });

            this.Uncover = new UncoverSquareCommand(game,position);
            this.ToggleFlag = new ToggleFlagSquareCommand(game,position);
                
            
        }


        public class UncoverSquareCommand : ICommand
        {
            public event EventHandler? CanExecuteChanged;

            private ICell<IGame> Game;

            public Vector2D Position {
                get;
            }
            //constructor
            

            public UncoverSquareCommand(ICell<IGame> game, Vector2D position)
            {
                this.Game = game;
                this.Position = position;
            }

            public bool CanExecute(object? parameter)
            {
                return this.Game.Value.IsSquareCovered(this.Position) & this.Game.Value.Status != GameStatus.Lost & this.Game.Value.Status != GameStatus.Won;
                //return true;
            }

            public void Execute(object? parameter)
            {
                Debug.WriteLine("clicked");
                Debug.WriteLine("x,y");
                Debug.WriteLine(this.Position);
                Debug.WriteLine("status");
                Debug.WriteLine(this.Game.Value.Board[Position].Status);
                var game = this.Game.Value.UncoverSquare(this.Position);

                //kmoet nog iets doen als je wint verliest of gwn moet verder spelen
                if (game.Status == GameStatus.Won) {
                    
                    this.Game.Value = this.Game.Value.UncoverSquare(this.Position);

                    
                }
                else if (game.Status == GameStatus.Lost) {
                    //indien je uncoverSquare niet meer kan oproepen toon ik alle andere bommen met een flag
                    //eerst alle vlaggen weg doen
                    
                    foreach (var square in game.Flags)
                    {
                        
                        this.Game.Value = this.Game.Value.ToggleFlag(square);

                    }

                    //nu alle bommen flaggen
                   
                    foreach (var square in game.Mines)
                    {
                       
                        this.Game.Value = this.Game.Value.ToggleFlag(square);

                    }

                    this.Game.Value = this.Game.Value.UncoverSquare(this.Position);
                }
                else if (game.Status == GameStatus.InProgress) {
                    this.Game.Value = this.Game.Value.UncoverSquare(this.Position);
                }

               // this.Game.Value = this.Game.Value.UncoverSquare(this.Position);
                




            }

        }
        public class ToggleFlagSquareCommand : ICommand
        {
            

            private readonly ICell<IGame> Game;

            public Vector2D Position
            {
                get;
            }

            public ToggleFlagSquareCommand(ICell<IGame> game, Vector2D position)
            {
                this.Game = game;
                this.Position = position;
            }

            public event EventHandler? CanExecuteChanged;

            public bool CanExecute(object? parameter)

            {
                //return true;
                return this.Game.Value.IsSquareCovered(this.Position) & this.Game.Value.Status != GameStatus.Lost & this.Game.Value.Status != GameStatus.Won;
            }

            public void Execute(object? parameter)
            {
                Debug.WriteLine("flagged");
                Debug.WriteLine("x,y");
                Debug.WriteLine(this.Position);

                //var game = this.Game.Value.ToggleFlag(this.Position);                

                this.Game.Value = this.Game.Value.ToggleFlag(this.Position);
                

            }

        }

    }

}


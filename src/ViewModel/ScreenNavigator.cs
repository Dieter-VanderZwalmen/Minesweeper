using Cells;
using Model.MineSweeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
   //ik weet eerlijk niet wanneer ik ViewModel achter mijn namen moet zetten
   //ik heb het enkel geschreven wanneer de classe een model class wrapped ongeveer
    public class ScreenNavigator
    {

        public ScreenNavigator() 
        {
            Current = Cell.Create<Screen>(null);
            var fistscreen = new MenuViewModel(Current);
            Current.Value = fistscreen; 

        }
        public ICell<Screen> Current { get; set; }      


        
    }

    
    public  class Screen
    {
        protected Screen(ICell<Screen> current)
        {
            this.Current = current;
        }
       

        protected ICell<Screen>? Current { get; }

    }
    public class GameScreenViewModel : Screen
    {
        public GameScreenViewModel(ICell<Screen> screen, int boardSize, bool enableFlooding, double mineProbability) : base(screen)
        {
            // parameters zijn : int boardSize, double mineProbability, bool flooding = true, int? seed = null 
            var game = IGame.CreateRandom(boardSize, mineProbability, enableFlooding);

            var gameViewModel = new GameViewModel(game);
            this.GameViewModel = gameViewModel;

            GoToSettings = new ActionCommand(() => screen.Value = new MenuViewModel(screen));
        }

        public GameViewModel GameViewModel { get; private set; }
       
        public ICommand GoToSettings { get; }
    }


    public class MenuViewModel : Screen
    {
        public MenuViewModel(ICell<Screen> current) : base(current)
        {
            Start = new ActionCommand(() => current.Value = new GameScreenViewModel(this.Current, BoardSize, Flooding, MineProbability));
            //hier gewoon default waarden in proppen
            Easy = new ActionCommand(() => current.Value = new GameScreenViewModel(this.Current, 5, Flooding, MineProbability));
            Medium = new ActionCommand(() => current.Value = new GameScreenViewModel(this.Current, 10 , Flooding, MineProbability));
            Hard = new ActionCommand(() => current.Value = new GameScreenViewModel(this.Current, 15, Flooding, MineProbability));

          

        }
        
        public int MaxSize { get; } = IGame.MaximumBoardSize;
        public int MinSize { get; } = IGame.MinimumBoardSize;
        
        public int BoardSize { get; set; } = 6;
        public bool Flooding { get; set; } = true;
        public double MineProbability { get; set; } = 0.2;

        public ICommand Start { get; }
        public ICommand Easy { get; }
        public ICommand Medium { get; }
        public ICommand Hard { get; }

      
    }
    public class ActionCommand : ICommand
    {
        private readonly Action action;

        public ActionCommand(Action action)
        {
            this.action = action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.action();
        }
    }
}

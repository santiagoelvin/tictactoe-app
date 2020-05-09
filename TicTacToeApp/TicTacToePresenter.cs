using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe;

namespace TicTacToeApp
{
    public class TicTacToePresenter
    {
        private readonly IConsoleInterface _view;
        private readonly IGame _game;

        public TicTacToePresenter(IConsoleInterface view, IGame game)
        {
            _view = view;
            _game = game;
        }

        public void Start()
        {
            string nextPlayer = "X";
            _view.WriteLine("Welcome to Tic-Tac-Toe Game!");
            _view.WriteLine("Player X will go first...");

            while (string.IsNullOrEmpty(_game.Winner))
            {
                _view.Write($"Player {nextPlayer} move: ");

                var playerEntry = _view.ReadLine();

                bool isValid = int.TryParse(playerEntry.ToString(), out int nextMove);

                if (isValid && _game.Play(nextMove))
                {
                    nextPlayer = nextPlayer == "X" ? "O" : "X";
                }
                else
                {
                    _view.WriteLine("Your move is not valid.");
                }

                DisplayBoard();
            }

            _view.WriteLine(Environment.NewLine);
            _view.WriteLine(_game.Winner == "-" ? "Cat's Game!" : $"Player {_game.Winner} Won!");
            _view.WriteLine(Environment.NewLine);
            _view.WriteLine("Press any key to exit..");
            _view.ReadKey();
        }

        private void DisplayBoard()
        {
            _view.WriteLine($"{Environment.NewLine}");
            _view.WriteLine($" {DisplayPosition(_game, 7)} | {DisplayPosition(_game, 8)} | {DisplayPosition(_game, 9)} ");
            _view.WriteLine("___________");
            _view.WriteLine($" {DisplayPosition(_game, 4)} | {DisplayPosition(_game, 5)} | {DisplayPosition(_game, 6)} ");
            _view.WriteLine("___________");
            _view.WriteLine($" {DisplayPosition(_game, 1)} | {DisplayPosition(_game, 2)} | {DisplayPosition(_game, 3)} ");
            _view.WriteLine(Environment.NewLine);
        }

        private string DisplayPosition(IGame game, int position)
        {
            return game[position] ?? " ";
        }
    }
}

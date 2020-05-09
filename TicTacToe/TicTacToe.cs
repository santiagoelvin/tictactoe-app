using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class TicTacToe : IGame
    {
        private const string X = "X";
        private const string O = "O";

        private string _nextMove = X;
        private string[] positions = new string[9];

        public string this[int position]
        {
            get => positions[GetPositionIndex(position)];
            set => positions[GetPositionIndex(position)] = value;
        }

        public string Winner { get; private set; } = string.Empty;

        public bool Play(int position)
        {
            if (!ValidatePlay(position))
                return false;

            positions[GetPositionIndex(position)] = _nextMove;

            if (FindMatch(_nextMove))
                Winner = _nextMove.ToString();
            else if (CheckTie())
                Winner = "-";

            _nextMove = _nextMove == X ? O : X;
            return true;
        }

        private bool FindMatch(string move)
        {
            var match = FindHorizontalMatch(move);

            if (!match.Any())
                match = FindVerticalMatch(move);

            if (!match.Any())
                match = FindDiagonalMatch(move);
 
            if (match.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckTie()
        {
            return positions.All(x => !string.IsNullOrEmpty(x));
        }

        private List<int> FindHorizontalMatch(string move)
        {
            List<int> match = new List<int>();

            if (CheckMatch(7, 8, 9, move))
                match.AddRange(new [] { 7, 8, 9 });
            else if (CheckMatch(4, 5, 6, move))
                match.AddRange(new[] { 4, 5, 6 });
            else if (CheckMatch(1, 2, 3, move))
                match.AddRange(new[] { 1, 2, 3 });

            return match;
        }

        private List<int> FindVerticalMatch(string move)
        {
            List<int> match = new List<int>();

            if (CheckMatch(7, 4, 1, move))
                match.AddRange(new[] { 7, 4, 1 });
            else if (CheckMatch(8, 5, 2, move))
                match.AddRange(new[] { 8, 5, 2 });
            else if (CheckMatch(9, 6, 3, move))
                match.AddRange(new[] { 9, 6, 3 });

            return match;
        }

        private List<int> FindDiagonalMatch(string move)
        {
            List<int> match = new List<int>();

            if (CheckMatch(7, 5, 3, move))
                match.AddRange(new[] { 7, 5, 3 });
            else if (CheckMatch(9, 5, 1, move))
                match.AddRange(new[] { 9, 5, 1 });

            return match;
        }

        private bool CheckMatch(int pos1, int pos2, int pos3, string move)
        {
            return positions[GetPositionIndex(pos1)] == move && positions[GetPositionIndex(pos2)] == move && positions[GetPositionIndex(pos3)] == move;
        }

        private int GetPositionIndex(int position)
        {
            return position - 1;
        }

        private bool ValidatePlay(int position)
        {
            var isValid = true;
            if (!string.IsNullOrEmpty(Winner))
                isValid = false;
            else if (position < 1 || position > 9)
                isValid = false;
            else if (!string.IsNullOrEmpty(positions[GetPositionIndex(position)]))
                isValid = false;


            return isValid;
        }
    }
}

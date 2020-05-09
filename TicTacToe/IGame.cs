using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public interface IGame
    {
        string Winner { get; }
        bool Play(int position);
        string this[int position] { get;  }
    }
}

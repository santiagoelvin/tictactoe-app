using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeApp
{
    public interface IConsoleInterface
    {
        void ReadKey();
        string ReadLine();
        void Write(string v);
        void WriteLine(string command);
    }
}

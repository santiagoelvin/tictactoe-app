using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeApp
{
    public class ConsoleInterface : IConsoleInterface
    {
        public void ReadKey() => Console.ReadKey();

        public string ReadLine() => Console.ReadLine();

        public void Write(string command) => Console.Write(command);

        public void WriteLine(string command) => Console.WriteLine(command);
    }
}

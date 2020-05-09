using Game = TicTacToe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Net.NetworkInformation;
using TicTacToe;
using Autofac;

namespace TicTacToeApp
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleInterface>().As<IConsoleInterface>();
            builder.RegisterType<Game.TicTacToe>().As<IGame>();
            builder.RegisterType<TicTacToePresenter>();
            Container = builder.Build();

            StartGame();

        }

        static void StartGame()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var presenter = scope.Resolve<TicTacToePresenter>();
                presenter.Start();
            }
        }
    }
}

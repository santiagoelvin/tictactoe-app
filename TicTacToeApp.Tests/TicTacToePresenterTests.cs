using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TicTacToe;

namespace TicTacToeApp.Tests
{
    [TestClass]
    public class TicTacToePresenterTests
    {
        [TestMethod]
        public void Start_Should_DisplayWelcomeMessage()
        {
            var fakeGame = Mock.Of<IGame>();

            var fakeView = Mock.Of<IConsoleInterface>();
            Mock.Get(fakeView).Setup(x => x.ReadLine())
            .Returns("1")
            .Callback(() =>
            {
                Mock.Get(fakeGame).SetupGet(x => x.Winner).Returns("Cat's Game."); // Force game to end.
            });


            var presenter = new TicTacToePresenter(fakeView, fakeGame);
            presenter.Start();

            Mock.Get(fakeView).Verify(x => x.WriteLine("Welcome to Tic-Tac-Toe Game!"), Times.Once);
        }

        [TestMethod]
        public void Start_Should_AskPlayerXForFirstMove()
        {
            var fakeGame = Mock.Of<IGame>();
            var fakeView = Mock.Of<IConsoleInterface>();
            Mock.Get(fakeView).Setup(x => x.ReadLine())
            .Returns("1")
            .Callback(() =>
            {
                Mock.Get(fakeGame).SetupGet(x => x.Winner).Returns("Cat's Game."); // Force game to end.
            });

            var presenter = new TicTacToePresenter(fakeView, fakeGame);
            presenter.Start();

            Mock.Get(fakeView).Verify(x => x.WriteLine("Player X will go first..."), Times.Once);
            Mock.Get(fakeView).Verify(x => x.Write("Player X move: "), Times.Once);
            Mock.Get(fakeView).Verify(x => x.ReadLine(), Times.Once);
        }

        
        [TestMethod]
        public void Start_Should_AskPlayerOForNextMove_When_GameHasNotYetEnded()
        {
            int inputCount = 0;
            var fakeGame = Mock.Of<IGame>();
            Mock.Get(fakeGame).Setup(x => x.Play(It.IsAny<int>())).Returns(true);

            var fakeView = Mock.Of<IConsoleInterface>();
            Mock.Get(fakeView).Setup(x => x.ReadLine())
            .Returns($"{inputCount + 1}")
            .Callback(() =>
            {
                inputCount++;
                if (inputCount == 2)
                    Mock.Get(fakeGame).SetupGet(x => x.Winner).Returns("Cat's Game."); // Force game to end.
            });

            var presenter = new TicTacToePresenter(fakeView, fakeGame);
            presenter.Start();

            Mock.Get(fakeView).Verify(x => x.Write("Player O move: "), Times.Once);
            Mock.Get(fakeView).Verify(x => x.ReadLine(), Times.AtLeast(2));
        }

        [TestMethod]
        public void Start_Should_DisplayUpdatedBoard_When_PlayerEnteredMove()
        {
            int inputCount = 0;
            var fakeGame = Mock.Of<IGame>();
            Mock.Get(fakeGame).Setup(x => x.Play(It.IsAny<int>())).Returns(true);
            Mock.Get(fakeGame).SetupGet(x => x[1]).Returns("X");
            Mock.Get(fakeGame).SetupGet(x => x[2]).Returns("O");

            var fakeView = Mock.Of<IConsoleInterface>();
            Mock.Get(fakeView).Setup(x => x.ReadLine())
            .Returns(() => $"{inputCount + 1}")
            .Callback(() =>
            {
                inputCount++;
                if (inputCount == 1)
                    Mock.Get(fakeGame).SetupGet(x => x.Winner).Returns("Cat's Game."); // Force game to end.
            });

            var presenter = new TicTacToePresenter(fakeView, fakeGame);
            presenter.Start();

            Mock.Get(fakeView).Verify(x => x.WriteLine("   |   |   "), Times.Exactly(2));
            Mock.Get(fakeView).Verify(x => x.WriteLine("___________"), Times.Exactly(2));
            Mock.Get(fakeView).Verify(x => x.WriteLine(" X | O |   "), Times.Once);
        }

        [TestMethod]
        public void Start_Should_NotAcceptInvalidInput_When_PlayerEnteredNonNumericValue()
        {
            var fakeGame = Mock.Of<IGame>();
            var fakeView = Mock.Of<IConsoleInterface>();
            Mock.Get(fakeView).Setup(x => x.ReadLine())
            .Returns("A")
            .Callback(() =>
            {
                Mock.Get(fakeGame).SetupGet(x => x.Winner).Returns("Cat's Game."); // Force game to end.
            });

            var presenter = new TicTacToePresenter(fakeView, fakeGame);
            presenter.Start();

            Mock.Get(fakeView).Verify(x => x.WriteLine("Your move is not valid."), Times.Once);
        }

        [TestMethod]
        public void Start_Should_DisplayMessage_When_GameEndedWithAWinner()
        {
            var endGameWinner = "X";
            var fakeGame = Mock.Of<IGame>();
            var fakeView = Mock.Of<IConsoleInterface>();
            Mock.Get(fakeView).Setup(x => x.ReadLine())
            .Returns("1")
            .Callback(() =>
            {
                Mock.Get(fakeGame).SetupGet(x => x.Winner).Returns(endGameWinner); // Force game to end.
            });

            var presenter = new TicTacToePresenter(fakeView, fakeGame);
            presenter.Start();

            Mock.Get(fakeView).Verify(x => x.WriteLine($"Player {endGameWinner} Won!"), Times.Once);
            Mock.Get(fakeView).Verify(x => x.WriteLine("Press any key to exit.."), Times.Once);
            Mock.Get(fakeView).Verify(x => x.ReadKey(), Times.Once);
        }

        [TestMethod]
        public void Start_Should_DisplayMessage_When_GameEndedWithATie()
        {
            var endGameWinner = "-";
            var fakeGame = Mock.Of<IGame>();
            var fakeView = Mock.Of<IConsoleInterface>();
            Mock.Get(fakeView).Setup(x => x.ReadLine())
            .Returns("1")
            .Callback(() =>
            {
                Mock.Get(fakeGame).SetupGet(x => x.Winner).Returns(endGameWinner); // Force game to end.
            });

            var presenter = new TicTacToePresenter(fakeView, fakeGame);
            presenter.Start();

            Mock.Get(fakeView).Verify(x => x.WriteLine("Cat's Game!"), Times.Once);
            Mock.Get(fakeView).Verify(x => x.WriteLine("Press any key to exit.."), Times.Once);
            Mock.Get(fakeView).Verify(x => x.ReadKey(), Times.Once);
        }
    }
}

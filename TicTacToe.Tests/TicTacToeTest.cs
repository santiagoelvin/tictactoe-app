using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TicTacToe.Tests
{
    [TestClass]
    public class TicTacToeTest
    {
        [TestMethod]
        public void Play_Should_NotAllowPosition_When_PositionIsAlreadyTaken()
        {
            var game = new TicTacToe();

            var xMoveIsValid = game.Play(1);
            var oMoveIsValid = game.Play(1);

            Assert.IsTrue(xMoveIsValid);
            Assert.IsFalse(oMoveIsValid);
        }

        [TestMethod]
        public void Play_Should_NotAllowPosition_When_PositionIsZero()
        {
            var game = new TicTacToe();

            var xMoveIsValid = game.Play(0);

            Assert.IsFalse(xMoveIsValid);
        }

        [TestMethod]
        public void Play_Should_NotAllowPosition_When_PositionIsGreaterThanNine()
        {
            var game = new TicTacToe();

            var xMoveIsValid = game.Play(10);

            Assert.IsFalse(xMoveIsValid);
        }

        [TestMethod]
        public void Play_Should_DeclareWinner_When_PositionSatisfyHorizontalPattern()
        {
            int[] xMoves = { 1, 2, 3 };
            int[] oMoves = { 4, 5, 6 };

            var game = new TicTacToe();

            for (int i = 0; i < 3; i++)
            {
                game.Play(xMoves[i]);
                game.Play(oMoves[i]);
            }

            Assert.AreEqual("X", game.Winner);
        }

        [TestMethod]
        public void Play_Should_DeclareWinner_When_PositionSatisfyVerticalPattern()
        {
            int[] xMoves = { 1, 3, 4 };
            int[] oMoves = { 2, 5, 8 };

            var game = new TicTacToe();

            for (int i = 0; i < 3; i++)
            { 
                game.Play(xMoves[i]);
                game.Play(oMoves[i]);
            }

            Assert.AreEqual("O", game.Winner);
        }

        [TestMethod]
        public void Play_Should_DeclareWinner_When_PositionSatisfyDiagonalPattern()
        {
            int[] xMoves = { 1, 5, 9 };
            int[] oMoves = { 4, 6, 7 };

            var game = new TicTacToe();

            for (int i = 0; i < 3; i++)
            {
                game.Play(xMoves[i]);
                game.Play(oMoves[i]);
            }

            Assert.AreEqual("X", game.Winner);
        }

        [TestMethod]
        public void Play_Should_DeclareTie_When_NoAvailablePositionLeft()
        {
            int[] xMoves = { 1, 3, 4, 8, 9 };
            int[] oMoves = { 2, 5, 7, 6, 9 };

            var game = new TicTacToe();

            for (int i = 0; i < 5; i++)
            {
                game.Play(xMoves[i]);
                game.Play(oMoves[i]);
            }

            Assert.AreEqual("-", game.Winner);
        }
    }
}

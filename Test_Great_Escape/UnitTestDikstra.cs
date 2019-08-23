using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test_Great_Escape
{
    [TestClass]
    public class UnitTestDikstra
    {
        [TestMethod]
        public void TestDikstraSimple()
        {
            Plateau plateau = new Plateau(9, 9);
            var result = plateau.Dikstra(new Position(0, 0), Direction.DROITE);
            List<Case> expectedResult = new List<Case>();
            expectedResult.Add(plateau.GetCase(1, 0));
            expectedResult.Add(plateau.GetCase(2, 0));
            expectedResult.Add(plateau.GetCase(3, 0));
            expectedResult.Add(plateau.GetCase(4, 0));
            expectedResult.Add(plateau.GetCase(5, 0));
            expectedResult.Add(plateau.GetCase(6, 0));
            expectedResult.Add(plateau.GetCase(7, 0));
            expectedResult.Add(plateau.GetCase(8, 0));
            Assert.AreEqual(result.Count, expectedResult.Count);
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], result[i]);
            }
        }

        [TestMethod]
        public void TestDikstraComplexe()
        {
            Plateau plateau = new Plateau(9, 9);
            plateau.AddMur(new Mur(new Position(1, 0), true));
            plateau.AddMur(new Mur(new Position(3, 0), true));
            plateau.AddMur(new Mur(new Position(4, 1), true));
            plateau.AddMur(new Mur(new Position(4, 3), true));
            var result = plateau.Dikstra(new Position(0, 0), Direction.DROITE);
            List<Case> expectedResult = new List<Case>();
            expectedResult.Add(plateau.GetCase(0, 1));
            expectedResult.Add(plateau.GetCase(0, 2));
            expectedResult.Add(plateau.GetCase(1, 2));
            expectedResult.Add(plateau.GetCase(2, 2));
            expectedResult.Add(plateau.GetCase(3, 2));
            expectedResult.Add(plateau.GetCase(3, 1));
            expectedResult.Add(plateau.GetCase(3, 0));
            expectedResult.Add(plateau.GetCase(4, 0));
            expectedResult.Add(plateau.GetCase(5, 0));
            expectedResult.Add(plateau.GetCase(6, 0));
            expectedResult.Add(plateau.GetCase(7, 0));
            expectedResult.Add(plateau.GetCase(8, 0));
            Assert.AreEqual(result.Count, expectedResult.Count);
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], result[i]);
            }
        }

        [TestMethod]
        public void TestDikstraSimpleFromBottom()
        {
            Plateau plateau = new Plateau(9, 9);
            var result = plateau.Dikstra(new Position(0, 8), Direction.DROITE);
            List<Case> expectedResult = new List<Case>();
            expectedResult.Add(plateau.GetCase(1, 8));
            expectedResult.Add(plateau.GetCase(2, 8));
            expectedResult.Add(plateau.GetCase(3, 8));
            expectedResult.Add(plateau.GetCase(4, 8));
            expectedResult.Add(plateau.GetCase(5, 8));
            expectedResult.Add(plateau.GetCase(6, 8));
            expectedResult.Add(plateau.GetCase(7, 8));
            expectedResult.Add(plateau.GetCase(8, 8));
            Assert.AreEqual(result.Count, expectedResult.Count);
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], result[i]);
            }
        }

        [TestMethod]
        public void TestDikstraException()
        {
            Plateau plateau = new Plateau(9, 9);
            plateau.AddMur(new Mur(new Position(1, 0), true));
            plateau.AddMur(new Mur(new Position(1, 2), true));
            plateau.AddMur(new Mur(new Position(1, 4), true));
            plateau.AddMur(new Mur(new Position(1, 6), true));
            plateau.AddMur(new Mur(new Position(0, 8), false));
            plateau.AddMur(new Mur(new Position(2, 7), true));
            try
            {
                var result = plateau.Dikstra(new Position(0, 8), Direction.DROITE);
                Assert.Fail(); // raises AssertionException
            }
            catch (NoPathException)
            {
            }
        }

        [TestMethod]
        public void TestDikstraFromTopToBottom()
        {
            Plateau plateau = new Plateau(9, 9);
            plateau.AddMur(new Mur(new Position(0, 1), false));

            var result = plateau.Dikstra(new Position(0, 0), Direction.BAS);
            List<Case> expectedResult = new List<Case>();
            expectedResult.Add(plateau.GetCase(1, 0));
            expectedResult.Add(plateau.GetCase(2, 0));
            expectedResult.Add(plateau.GetCase(2, 1));
            expectedResult.Add(plateau.GetCase(2, 2));
            expectedResult.Add(plateau.GetCase(2, 3));
            expectedResult.Add(plateau.GetCase(2, 4));
            expectedResult.Add(plateau.GetCase(2, 5));
            expectedResult.Add(plateau.GetCase(2, 6));
            expectedResult.Add(plateau.GetCase(2, 7));
            expectedResult.Add(plateau.GetCase(2, 8));
            Assert.AreEqual(result.Count, expectedResult.Count);
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], result[i]);
            }
        }
    }
}

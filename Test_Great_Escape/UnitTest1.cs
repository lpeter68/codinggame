using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test_Great_Escape
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAddVoisine()
        {
            Case case1 = new Case(new Position(0, 0));
            Case case2 = new Case(new Position(0, 1));
            case1.AddVoisine(case2);
            Assert.IsTrue(case1.Voisines.Contains(case2));
            Assert.IsTrue(case2.Voisines.Contains(case1));
        }

        [TestMethod]
        public void TestRemoveVoisine()
        {
            Case case1 = new Case(new Position(0, 0));
            Case case2 = new Case(new Position(0, 1));
            case1.AddVoisine(case2);
            case1.RemoveVoisine(case2);
            Assert.IsFalse(case2.Voisines.Contains(case2));
            Assert.IsFalse(case2.Voisines.Contains(case1));
        }

        [TestMethod]
        public void TestInitPlateau()
        {
            Plateau plateau = new Plateau(2, 2);
            var case1 = plateau.GetCase(0, 0);
            var case2 = plateau.GetCase(1, 0);
            var case3 = plateau.GetCase(0, 1);
            var case4 = plateau.GetCase(1, 1);

            Assert.IsTrue(case1.Voisines.Contains(case2));
            Assert.IsTrue(case2.Voisines.Contains(case1));

            Assert.IsTrue(case1.Voisines.Contains(case3));
            Assert.IsTrue(case3.Voisines.Contains(case1));

            Assert.IsTrue(case2.Voisines.Contains(case4));
            Assert.IsTrue(case4.Voisines.Contains(case2));

            Assert.IsTrue(case3.Voisines.Contains(case4));
            Assert.IsTrue(case4.Voisines.Contains(case3));

            Assert.IsFalse(case2.Voisines.Contains(case3));
            Assert.IsFalse(case3.Voisines.Contains(case2));

            Assert.IsFalse(case1.Voisines.Contains(case4));
            Assert.IsFalse(case4.Voisines.Contains(case1));
        }

        [TestMethod]
        public void TestInitBigPlateau()
        {
            Plateau plateau = new Plateau(9, 9);
            var case1 = plateau.GetCase(8, 0);
            var case2 = plateau.GetCase(8, 1);
            var case3 = plateau.GetCase(1, 7);
            var case4 = plateau.GetCase(2, 7);

            Assert.IsTrue(case1.Voisines.Contains(case2));
            Assert.IsTrue(case2.Voisines.Contains(case1));

            Assert.IsTrue(case3.Voisines.Contains(case4));
            Assert.IsTrue(case4.Voisines.Contains(case3));
        }


        [TestMethod]
        public void TestAddMurVert()
        {
            Plateau plateau = new Plateau(2, 2);
            plateau.AddMur(new Mur(new Position(1, 0), true));
            var case1 = plateau.GetCase(0, 0);
            var case2 = plateau.GetCase(1, 0);
            var case3 = plateau.GetCase(0, 1);
            var case4 = plateau.GetCase(1, 1);

            Assert.IsFalse(case1.Voisines.Contains(case2));
            Assert.IsFalse(case2.Voisines.Contains(case1));

            Assert.IsTrue(case1.Voisines.Contains(case3));
            Assert.IsTrue(case3.Voisines.Contains(case1));

            Assert.IsTrue(case2.Voisines.Contains(case4));
            Assert.IsTrue(case4.Voisines.Contains(case2));

            Assert.IsFalse(case3.Voisines.Contains(case4));
            Assert.IsFalse(case4.Voisines.Contains(case3));

            Assert.IsFalse(case2.Voisines.Contains(case3));
            Assert.IsFalse(case3.Voisines.Contains(case2));

            Assert.IsFalse(case1.Voisines.Contains(case4));
            Assert.IsFalse(case4.Voisines.Contains(case1));
        }

        [TestMethod]
        public void TestAddMurInteresct()
        {
            Plateau plateau = new Plateau(9, 9);
            Assert.IsTrue(plateau.AddMur(new Mur(new Position(1, 1), true)));

            //Intersection
            Assert.IsFalse(plateau.AddMur(new Mur(new Position(1, 0), true)));
            Assert.IsFalse(plateau.AddMur(new Mur(new Position(1, 1), true)));
            Assert.IsFalse(plateau.AddMur(new Mur(new Position(0, 2), false)));
            Assert.IsFalse(plateau.AddMur(new Mur(new Position(1, 2), true)));

            //Out of board
            Assert.IsFalse(plateau.AddMur(new Mur(new Position(0, 5), true)));
            Assert.IsFalse(plateau.AddMur(new Mur(new Position(9, 5), true)));
            Assert.IsFalse(plateau.AddMur(new Mur(new Position(5, 8), true)));
            Assert.IsFalse(plateau.AddMur(new Mur(new Position(5, 0), false)));
            Assert.IsFalse(plateau.AddMur(new Mur(new Position(5, 9), false)));
            Assert.IsFalse(plateau.AddMur(new Mur(new Position(8, 5), false)));

            //OK
            Assert.IsTrue(plateau.AddMur(new Mur(new Position(1, 1), false)));
            Assert.IsTrue(plateau.AddMur(new Mur(new Position(3, 0), true)));
            Assert.IsTrue(plateau.AddMur(new Mur(new Position(8, 7), true)));
            Assert.IsTrue(plateau.AddMur(new Mur(new Position(5, 5), true)));
            Assert.IsTrue(plateau.AddMur(new Mur(new Position(7, 5), false)));
        }

        [TestMethod]
        public void TestAddMurHor()
        {
            Plateau plateau = new Plateau(2, 2);
            plateau.AddMur(new Mur(new Position(0, 1), false));
            var case1 = plateau.GetCase(0, 0);
            var case2 = plateau.GetCase(1, 0);
            var case3 = plateau.GetCase(0, 1);
            var case4 = plateau.GetCase(1, 1);

            Assert.IsTrue(case1.Voisines.Contains(case2));
            Assert.IsTrue(case2.Voisines.Contains(case1));

            Assert.IsFalse(case1.Voisines.Contains(case3));
            Assert.IsFalse(case3.Voisines.Contains(case1));

            Assert.IsFalse(case2.Voisines.Contains(case4));
            Assert.IsFalse(case4.Voisines.Contains(case2));

            Assert.IsTrue(case3.Voisines.Contains(case4));
            Assert.IsTrue(case4.Voisines.Contains(case3));

            Assert.IsFalse(case2.Voisines.Contains(case3));
            Assert.IsFalse(case3.Voisines.Contains(case2));

            Assert.IsFalse(case1.Voisines.Contains(case4));
            Assert.IsFalse(case4.Voisines.Contains(case1));
        }

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

        [TestMethod]
        public void TestReOrderJoueurOrder()
        {
            Joueur moi = new Joueur(new Position(0, 0), 0, 6);
            Joueur opposant1 = new Joueur(new Position(8, 8), 1, 6);
            Joueur opposant2 = new Joueur(new Position(7, 7), 2, 6);
            List<Joueur> opposants = new List<Joueur>();
            opposants.Add(opposant1);
            opposants.Add(opposant2);
            var result = Player.GetAllJoueurOrder(moi, opposants);
            Assert.IsTrue(result[0] == moi);
            Assert.IsTrue(result[1] == opposant1);
            Assert.IsTrue(result[2] == opposant2);
        }

        [TestMethod]
        public void TestReOrderJoueurDisorder()
        {
            Joueur moi = new Joueur(new Position(0, 0), 1, 6);
            Joueur opposant1 = new Joueur(new Position(8, 8), 0, 6);
            Joueur opposant2 = new Joueur(new Position(7, 7), 2, 6);
            List<Joueur> opposants = new List<Joueur>();
            opposants.Add(opposant1);
            opposants.Add(opposant2);
            var result = Player.GetAllJoueurOrder(moi, opposants);
            Assert.IsTrue(result[0] == moi);
            Assert.IsTrue(result[1] == opposant2);
            Assert.IsTrue(result[2] == opposant1);
        }

        [TestMethod]
        public void TestFonctionEvaluationSimple()
        {
            Plateau plateau = new Plateau(9, 9);
            Joueur moi = new Joueur(new Position(0, 0), 0, 6);
            Joueur opposant = new Joueur(new Position(8, 8), 1, 6);
            List<Joueur> opposants = new List<Joueur>();
            opposants.Add(opposant);
            plateau.Joueurs = Player.GetAllJoueurOrder(moi, opposants);
            var result = plateau.Evaluation();
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void TestFonctionEvaluationComplexe()
        {
            Plateau plateau = new Plateau(9, 9);
            Joueur moi = new Joueur(new Position(1, 0), 0, 6);
            Joueur opposant = new Joueur(new Position(8, 8), 1, 6);
            List<Joueur> opposants = new List<Joueur>();
            opposants.Add(opposant);
            plateau.Joueurs = Player.GetAllJoueurOrder(moi, opposants);
            var result = plateau.Evaluation();
            Assert.AreEqual(result, 3);
        }

        [TestMethod]
        public void TestMinMax()
        {
            Plateau plateau = new Plateau(9, 9);
            Player.InitAvailableMur(plateau);
            Joueur moi = new Joueur(new Position(0, 2), 0, 6);
            Joueur opposant = new Joueur(new Position(8, 4), 1, 6);
            List<Joueur> opposants = new List<Joueur>();
            opposants.Add(opposant);
            Coup coup;
            plateau.Joueurs = Player.GetAllJoueurOrder(moi, opposants);
            var trace = "";
            var result = Player.MinMax(plateau, 0, 2, int.MinValue, int.MaxValue, out coup, ref trace);
            Assert.IsNotNull(coup);
        }

        [TestMethod]
        public void TestMinMaxException()
        {
            Plateau plateau = new Plateau(9, 9);
            Player.InitAvailableMur(plateau);
            plateau.AddMur(new Mur(new Position(7, 0), true));
            plateau.AddMur(new Mur(new Position(7, 2), true));
            Joueur moi = new Joueur(new Position(2, 1), 0, 6);
            Joueur opposant = new Joueur(new Position(8, 3), 1, 6);
            List<Joueur> opposants = new List<Joueur>();
            opposants.Add(opposant);
            Coup coup;
            plateau.Joueurs = Player.GetAllJoueurOrder(moi, opposants);
            var trace = "";
            var result = Player.MinMax(plateau, 0, 2, int.MinValue, int.MaxValue, out coup, ref trace);
            Assert.AreNotEqual(coup.ToString(), "7 4 H");
        }

        [TestMethod]

        public void HashCodePos()
        {
            List<int> hashs = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var a = new Position(i, j);
                    Assert.IsFalse(hashs.Contains(a.GetHashCode()));
                    hashs.Add(a.GetHashCode());
                }
            }
        }

        [TestMethod]

        public void HashCodeMur()
        {
            List<int> hashs = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        var a = new Mur(new Position(i, j), k == 0);
                        Assert.IsFalse(hashs.Contains(a.GetHashCode()));
                        hashs.Add(a.GetHashCode());
                    }
                }
            }
        }

        [TestMethod]
        public void TestMinMaxBug()
        {
            Plateau plateau = new Plateau(9, 9);
            Player.InitAvailableMur(plateau);
            plateau.AddMur(new Mur(new Position(8, 0), true));
            Joueur moi = new Joueur(new Position(1, 0), 0, 10);
            Joueur opposant = new Joueur(new Position(8, 7), 1, 9);
            List<Joueur> opposants = new List<Joueur>();
            opposants.Add(opposant);
            Coup coup;
            plateau.Joueurs = Player.GetAllJoueurOrder(moi, opposants);
            var trace = "";
            var result = Player.MinMax(plateau, 0, 2, int.MinValue, int.MaxValue, out coup, ref trace);
            Assert.AreNotEqual(coup.ToString(), "7 4 H");
        }

    }
}

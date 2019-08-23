using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test_Great_Escape
{
    [TestClass]
    public class UnitTestFonction
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
        public void TestPlateauEquality()
        {
            Plateau plateau1 = new Plateau(9, 9);
            Player.InitAvailableMur(plateau1);
            plateau1.AddMur(new Mur(new Position(8, 0), true));
            Joueur moi = new Joueur(new Position(1, 0), 0, 10);
            Joueur opposant = new Joueur(new Position(8, 7), 1, 9);
            List<Joueur> opposants = new List<Joueur>();
            opposants.Add(opposant);
            plateau1.Joueurs = Player.GetAllJoueurOrder(moi, opposants);

            Plateau plateau2 = new Plateau(9, 9);
            Player.InitAvailableMur(plateau2);
            plateau2.AddMur(new Mur(new Position(8, 0), true));
            Joueur moi2 = new Joueur(new Position(1, 0), 0, 10);
            Joueur opposant2 = new Joueur(new Position(8, 7), 1, 9);
            List<Joueur> opposants2 = new List<Joueur>();
            opposants2.Add(opposant2);
            plateau2.Joueurs = Player.GetAllJoueurOrder(moi2, opposants2);
            Assert.AreEqual(plateau1, plateau2);
        }

        [TestMethod]
        public void TestMurVerticalBloqueCase()
        {
            Plateau plateau = new Plateau(2, 2);
            var case1 = plateau.GetCase(0, 0);
            var case2 = plateau.GetCase(1, 0);
            var case3 = plateau.GetCase(0, 1);
            var case4 = plateau.GetCase(1, 1);
            var mur = new Mur(new Position(1, 0), true);
            Assert.IsTrue(mur.BloqueCases(case1, case2, plateau));
            Assert.IsTrue(mur.BloqueCases(case3, case4, plateau));
            Assert.IsTrue(mur.BloqueCases(case2, case1, plateau));
            Assert.IsTrue(mur.BloqueCases(case4, case3, plateau));

            Assert.IsFalse(mur.BloqueCases(case1, case3, plateau));
            Assert.IsFalse(mur.BloqueCases(case1, case4, plateau));
            Assert.IsFalse(mur.BloqueCases(case2, case3, plateau));
            Assert.IsFalse(mur.BloqueCases(case2, case4, plateau));
            Assert.IsFalse(mur.BloqueCases(case3, case1, plateau));
            Assert.IsFalse(mur.BloqueCases(case3, case2, plateau));
            Assert.IsFalse(mur.BloqueCases(case4, case1, plateau));
            Assert.IsFalse(mur.BloqueCases(case4, case2, plateau));
        }

        [TestMethod]
        public void TestMurHorizontalBloqueCase()
        {
            Plateau plateau = new Plateau(2, 2);
            var case1 = plateau.GetCase(0, 0);
            var case2 = plateau.GetCase(1, 0);
            var case3 = plateau.GetCase(0, 1);
            var case4 = plateau.GetCase(1, 1);
            var mur = new Mur(new Position(0, 1), false);
            Assert.IsTrue(mur.BloqueCases(case1, case3, plateau));
            Assert.IsTrue(mur.BloqueCases(case2, case4, plateau));
            Assert.IsTrue(mur.BloqueCases(case3, case1, plateau));
            Assert.IsTrue(mur.BloqueCases(case4, case2, plateau));

            Assert.IsFalse(mur.BloqueCases(case1, case2, plateau));
            Assert.IsFalse(mur.BloqueCases(case1, case4, plateau));
            Assert.IsFalse(mur.BloqueCases(case2, case3, plateau));
            Assert.IsFalse(mur.BloqueCases(case2, case1, plateau));
            Assert.IsFalse(mur.BloqueCases(case3, case4, plateau));
            Assert.IsFalse(mur.BloqueCases(case3, case2, plateau));
            Assert.IsFalse(mur.BloqueCases(case4, case1, plateau));
            Assert.IsFalse(mur.BloqueCases(case4, case3, plateau));
        }

    }
}

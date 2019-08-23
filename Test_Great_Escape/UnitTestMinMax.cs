using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Test_Great_Escape
{
    [TestClass]
    public class UnitTestMinMax
    {
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

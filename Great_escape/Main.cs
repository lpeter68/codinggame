using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
public class Player
{
    static List<Mur> AvailableMur;

    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int w = int.Parse(inputs[0]); // width of the board
        int h = int.Parse(inputs[1]); // height of the board
        int playerCount = int.Parse(inputs[2]); // number of players (2 or 3)
        int myId = int.Parse(inputs[3]); // id of my player (0 = 1st player, 1 = 2nd player, ...)

        Plateau plateau = new Plateau(w, h);

        InitAvailableMur(plateau);

        // game loop
        while (true)
        {
            Joueur moi = null;
            List<Joueur> opposants = new List<Joueur>();
            for (int i = 0; i < playerCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int x = int.Parse(inputs[0]); // x-coordinate of the player
                int y = int.Parse(inputs[1]); // y-coordinate of the player
                int wallsLeft = int.Parse(inputs[2]); // number of walls available for the player
                if (i == myId)
                {
                    moi = new Joueur(new Position(x, y), myId, wallsLeft);
                }
                else if (x != -1 && y != -1)
                {
                    opposants.Add(new Joueur(new Position(x, y), i, wallsLeft));
                }
            }
            plateau.Joueurs = GetAllJoueurOrder(moi, opposants);

            int wallCount = int.Parse(Console.ReadLine()); // number of walls on the board
            for (int i = 0; i < wallCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int wallX = int.Parse(inputs[0]); // x-coordinate of the wall
                int wallY = int.Parse(inputs[1]); // y-coordinate of the wall
                string wallOrientation = inputs[2]; // wall orientation ('H' or 'V')
                Mur mur = new Mur(new Position(wallX, wallY), wallOrientation == "V");
                plateau.AddMur(mur);
            }

            /*foreach (var item in plateau.Murs)
            {
                Console.Error.WriteLine(item.ToString());
            }

            Console.Error.WriteLine("Dikjstra");
            var dik = plateau.Dikstra(moi.Pos, moi.Objectif);
            foreach (var item in dik)
            {
                Console.Error.WriteLine(item.Pos.ToString());
            }*/

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            Coup coup;
            Console.Error.WriteLine("Start MinMax");
            var trace = "";
            var r = MinMax(plateau, 0, 2, int.MinValue, int.MaxValue, out coup, ref trace);
            Console.Error.WriteLine("End MinMax");
            //Console.Error.WriteLine(trace);
            Console.Error.WriteLine(r);
            Console.Error.WriteLine(coup.ToString());
            coup.JouerLeCoup();
        }
    }

    public static void InitAvailableMur(Plateau plateau)
    {
        AvailableMur = new List<Mur>();

        for (int i = 0; i < plateau.Width; i++)
        {
            for (int j = 0; j < plateau.Height; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    var mur = new Mur(new Position(i, j), k == 0);
                    AvailableMur.Add(mur);
                }
            }
        }
    }

    public static List<Joueur> GetAllJoueurOrder(Joueur moi, List<Joueur> lesAutres)
    {
        Console.Error.WriteLine("start");
        var nbJoueurs = lesAutres.Count() + 1;
        Console.Error.WriteLine(nbJoueurs);

        List<Joueur> orderList = new List<Joueur>();
        orderList.Add(moi);
        int myId = moi.PlayerId;

        var nextPlayers = lesAutres.Where(a => a.PlayerId > myId);
        foreach (var nextPlayer in nextPlayers)
        {
            orderList.Add(nextPlayer);
        }
        nextPlayers = lesAutres.Where(a => a.PlayerId < myId); // ceux qui jouent avant moi
        foreach (var nextPlayer in nextPlayers)
        {
            orderList.Add(nextPlayer);
        }
        return orderList;
    }

    public static void Play(Plateau plateau, Joueur myPlayer, List<Joueur> opposants)
    {
        var move = plateau.Dikstra(myPlayer.Pos, myPlayer.Objectif);
        var vainqueur = myPlayer;
        var coupVainqueur = move.Count();
        foreach (var joueur in opposants)
        {
            var result = plateau.Dikstra(joueur.Pos, joueur.Objectif).Count;
            if (result < coupVainqueur)
            {
                vainqueur = joueur;
                coupVainqueur = result;
            }
        }
        Mur bestMur = null;
        int bestCoup = coupVainqueur;
        if (vainqueur != myPlayer && myPlayer.MurRestant > 0)
        {
            var monCoup = move.Count();
            Console.Error.WriteLine(" on bloque " + vainqueur.PlayerId);
            for (int i = 0; i < plateau.Width; i++)
            {
                for (int j = 0; j < plateau.Height; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        var mur = new Mur(new Position(i, j), k == 0);
                        if (plateau.AddMur(mur))
                        {
                            try
                            {
                                var resultWithMur = plateau.Dikstra(vainqueur.Pos, vainqueur.Objectif).Count;
                                if (resultWithMur > bestCoup)
                                {
                                    var monCoupWithMur = plateau.Dikstra(myPlayer.Pos, myPlayer.Objectif).Count;
                                    if (monCoup >= monCoupWithMur)
                                    {
                                        resultWithMur = bestCoup;
                                        bestMur = mur;
                                    }
                                }
                            }
                            catch (NoPathException)
                            {
                                Console.Error.WriteLine("no issue");
                            }
                            finally
                            {
                                plateau.RemoveMur(mur);
                            }
                        }
                    }
                }
            }
            if (bestMur != null)
            {
                Console.WriteLine(bestMur.ToString());
            }
            else
            {
                Console.Error.WriteLine("no wall : try to go " + move[move.Count - 1].Pos.ToString());
                myPlayer.MoveToNextCase(move[0]);
            }
        }
        else
        {
            Console.Error.WriteLine("try to go " + move[move.Count - 1].Pos.ToString());
            myPlayer.MoveToNextCase(move[0]);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="plateau"></param>
    /// <param name="joueurs"> la liste des joueurs dans l'ordre du tour en commencant par moi</param>
    /// <param name="profondeur"></param>
    /// <param name="profondeurMax"></param>
    /// <returns></returns>
    public static int MinMax(Plateau plateau, int profondeur, int profondeurMax, int alpha, int beta, out Coup meilleurCoup, ref string trace)
    {
        trace += "\r\n";
        for (int i = 0; i < profondeur; i++)
        {
            trace += "   ";
        }
        if (profondeur < profondeurMax)
        {
            int currentJoueurIndex = profondeur % plateau.Joueurs.Count(); //id du joueurs de la liste dont c'est le tour (0 c'est moi)

            List<Coup> coups = new List<Coup>();
            coups.Add(new Coup(Direction.DROITE));
            coups.Add(new Coup(Direction.GAUCHE));
            coups.Add(new Coup(Direction.HAUT));
            coups.Add(new Coup(Direction.BAS));

            // pour tout les murs
            if (plateau.Joueurs[currentJoueurIndex].MurRestant > 0)
            {
                foreach (var mur in Player.AvailableMur)
                {
                    coups.Add(new Coup(mur));
                }
            }

            int bestEval = currentJoueurIndex == 0 ? int.MinValue : int.MaxValue;
            Coup bestCoup = null;
            foreach (var coup in coups)
            {
                //Console.Error.WriteLine(profondeur + " " + coup.ToString());
                if (coup.Mur != null)
                {
                    var mur = coup.Mur;
                    if (plateau.AddMur(mur))
                    {
                        try
                        {
                            var eval = MinMax(plateau, profondeur + 1, profondeurMax, alpha, beta, out meilleurCoup, ref trace);
                            trace += " " + eval;
                            //Console.Error.WriteLine("eval " + eval);
                            if (currentJoueurIndex == 0 && eval > bestEval) //MAX
                            {
                                bestEval = eval;
                                bestCoup = coup;
                                alpha = Math.Max(alpha, bestEval);
                                if (beta <= bestEval)
                                {
                                    meilleurCoup = bestCoup;
                                    return bestEval;
                                }
                            }
                            else if (currentJoueurIndex != 0 && eval < bestEval) //MIN
                            {
                                bestEval = eval;
                                bestCoup = coup;
                                beta = Math.Min(alpha, bestEval);
                                if (bestEval <= alpha)
                                {
                                    meilleurCoup = bestCoup;
                                    return bestEval;
                                }
                            }
                        }
                        catch (NoPathException)
                        {
                            Console.Error.WriteLine("invalid no issue " + coup.Mur + " " + coup.Dir);
                        }
                        finally
                        {
                            plateau.RemoveMur(mur);
                        }
                    }
                    else if (profondeur == 0 && AvailableMur.Contains(mur))
                    {
                        AvailableMur.Remove(mur);
                    }
                }
                else
                {
                    Direction direction = (Direction)coup.Dir;
                    var oldPos = plateau.Joueurs[currentJoueurIndex].Pos;
                    var newPos = oldPos.GetPositionInDirection(direction);
                    var test = plateau.GetCase(oldPos).Voisines.Contains(plateau.GetCase(newPos));
                    if (newPos != null && test)
                    {
                        try
                        {
                            plateau.Joueurs[currentJoueurIndex].Pos = newPos;
                            var eval = MinMax(plateau, profondeur + 1, profondeurMax, beta, alpha, out meilleurCoup, ref trace);
                            trace += " " + eval;
                            //Console.Error.WriteLine("eval " + eval);
                            if (currentJoueurIndex == 0 && eval > bestEval) //MAX
                            {
                                bestEval = eval;
                                bestCoup = coup;
                                alpha = Math.Max(alpha, bestEval);
                                if (beta <= bestEval) //coupe beta
                                {
                                    meilleurCoup = bestCoup;
                                    return bestEval;
                                }
                            }
                            else if (currentJoueurIndex != 0 && eval < bestEval) //MIN
                            {
                                bestEval = eval;
                                bestCoup = coup;
                                beta = Math.Min(beta, bestEval);
                                if (bestEval <= alpha)  //coupe alpha
                                {
                                    meilleurCoup = bestCoup;
                                    return bestEval;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.Error.WriteLine("invalid no issue " + coup.Mur + " " + coup.Dir);
                        }
                        finally
                        {
                            plateau.Joueurs[currentJoueurIndex].Pos = oldPos;
                        }
                    }
                }
            }
            meilleurCoup = bestCoup;
            trace += "=>" + bestEval;
            return bestEval;
        }
        else
        {
            meilleurCoup = null;
            var eval = plateau.Evaluation();
            trace += "=>" + eval;
            return eval;
        }
    }
}



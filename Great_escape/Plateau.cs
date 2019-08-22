using System;
using System.Collections.Generic;
using System.Linq;

public class Plateau
{
    public List<Case> Cases { get; set; }
    public List<Mur> Murs { get; set; }
    public List<Joueur> Joueurs { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public static Dictionary<int, InfoDikstra> transposition = new Dictionary<int, InfoDikstra>();

    public Plateau(int width, int height)
    {
        Width = width;
        Height = height;
        Cases = new List<Case>();
        Murs = new List<Mur>();
        Joueurs = new List<Joueur>();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var pos = new Position(i, j);
                Cases.Add(new Case(pos));
            }
        }
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var current = GetCase(i, j);
                current.AddVoisine(GetCase(i, j - 1));
                current.AddVoisine(GetCase(i, j + 1));
                current.AddVoisine(GetCase(i - 1, j));
                current.AddVoisine(GetCase(i + 1, j));
            }
        }
    }

    public bool AddMur(Mur mur, bool noValidation = false)
    {
        if (!Murs.Contains(mur) && (noValidation || MurIsValid(mur)))
        {
            Murs.Add(mur);
            var case1 = GetCase(mur.Pos);
            Case case2;
            if (mur.IsVertical)
            {
                case2 = GetCase(mur.Pos.GetPositionBas());
                case1.RemoveVoisine(GetCase(case1.Pos.GetPositionGauche()));
                case2.RemoveVoisine(GetCase(case2.Pos.GetPositionGauche()));
            }
            else
            {
                case2 = GetCase(mur.Pos.GetPositionDroite());
                case1.RemoveVoisine(GetCase(case1.Pos.GetPositionHaut()));
                case2.RemoveVoisine(GetCase(case2.Pos.GetPositionHaut()));
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RemoveMur(Mur mur)
    {
        if (Murs.Contains(mur))
        {
            Murs.Remove(mur);
            var case1 = GetCase(mur.Pos);
            Case case2;
            if (mur.IsVertical)
            {
                case2 = GetCase(mur.Pos.GetPositionBas());
                case1.AddVoisine(GetCase(case1.Pos.GetPositionGauche()));
                case2.AddVoisine(GetCase(case2.Pos.GetPositionGauche()));
            }
            else
            {
                case2 = GetCase(mur.Pos.GetPositionDroite());
                case1.AddVoisine(GetCase(case1.Pos.GetPositionHaut()));
                case2.AddVoisine(GetCase(case2.Pos.GetPositionHaut()));
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool MurIsValid(Mur murAdd)
    {
        if (murAdd.ToString() == "4 1 H") Console.Error.WriteLine("validation");
        // vérification qu'il est bien sur le plateu
        var x = murAdd.Pos.X;
        var y = murAdd.Pos.Y;
        if (murAdd.IsVertical)
        {
            if (!(y < Height - 1 && x > 0 && x < Width)) return false;
        }
        else
        {
            if (!(x < Width - 1 && y > 0 && y < Width)) return false;
        }

        // vérification des intersections (inclus le cas ou le mur existe déjà)
        var pointCentral = murAdd.PointCentral;
        foreach (var mur in Murs)
        {
            if (pointCentral == mur.PointCentral || (mur.IsVertical == murAdd.IsVertical && (pointCentral == mur.Pos || murAdd.Pos == mur.PointCentral)))
            {
                return false;
            }
        }

        if (murAdd.ToString() == "4 1 H") Console.Error.WriteLine("placement ok");

        foreach (var joueur in Joueurs)
        {
            if (murAdd.ToString() == "4 1 H") Console.Error.WriteLine("test joueur " + joueur.PlayerId);
            if (AddMur(murAdd, true))
            {
                try
                {
                    var result = Dikstra(joueur.Pos, joueur.Objectif);
                    if (murAdd.ToString() == "4 1 H")
                    {
                        foreach (var item in result)
                        {
                            Console.Error.WriteLine(item.ToString());
                        }
                    }
                }
                catch (NoPathException)
                {
                    return false;
                }
                finally
                {
                    RemoveMur(murAdd);
                }
            }
        }
        return true;
    }

    public Case GetCase(int x, int y)
    {
        Position pos = new Position(x, y);
        return GetCase(pos);
    }

    public Case GetCase(Position pos)
    {
        if (pos != null)
        {
            return Cases.FirstOrDefault(c => c.Pos == pos);
        }
        else
        {
            return null;
        }
    }

    public List<Case> Dikstra(Position from, Direction objectif)
    {
        var hashCode = GetDikstraHashCode(from, objectif);
        if (transposition.ContainsKey(hashCode))
        {
            var a = transposition[hashCode];
            if (a.From != from || a.Objectif != objectif)
            {
                Console.Error.WriteLine("hashCode colission Exception");
                throw new Exception("hashCode colission Exception");
            }
            return a.Result;
        }
        else
        {
            var lastMur = Murs.LastOrDefault();
            if (lastMur != null)
            {
                var previousHashCode = hashCode ^ Murs.LastOrDefault().GetHashCode();
                if (transposition.ContainsKey(previousHashCode))
                {
                    //TODO vérifier le chemin différentiel
                    //Console.Error.WriteLine("diffenrtiel is enough");
                }
            }
            var result = DikstraCalculation(from, objectif);
            transposition.Add(hashCode, new InfoDikstra(from, objectif, result, this));
            return result;
        }
    }

    public List<Case> DikstraCalculation(Position from, Direction objectif)
    {
        var caseFrom = GetCase(from);

        Dictionary<Case, int> caseCout = new Dictionary<Case, int>();
        Dictionary<Case, List<Case>> caseChemin = new Dictionary<Case, List<Case>>();
        List<Case> casesAtraiter = new List<Case>();
        casesAtraiter.Add(caseFrom);
        caseCout.Add(caseFrom, 0);
        caseChemin.Add(caseFrom, new List<Case>());
        var i = 0;
        while (casesAtraiter.Count > i)
        {
            var enCours = casesAtraiter[i];
            int currentCout = caseCout[enCours];
            var currentChemin = caseChemin[enCours];
            int voisineCout = currentCout + 1;
            foreach (var voisine in enCours.Voisines)
            {
                if (!casesAtraiter.Contains(voisine))
                {
                    casesAtraiter.Add(voisine);
                }
                var voisineChemin = new List<Case>(currentChemin);
                voisineChemin.Add(voisine);
                if (!caseCout.Keys.Contains(voisine) || caseCout[voisine] > voisineCout)
                {
                    caseCout[voisine] = voisineCout;
                    caseChemin[voisine] = voisineChemin;
                }
            }
            i++;
        }
        var targets = GetCaseObjectif(objectif);
        Case bestCase = null;
        int bestCout = int.MaxValue;
        foreach (var target in targets)
        {
            if (caseCout.Keys.Contains(target))
            {
                var targetCout = caseCout[target];
                if (targetCout < bestCout)
                {
                    bestCase = target;
                    bestCout = targetCout;
                }
            }
        }
        if (bestCase == null)
        {
            throw new NoPathException();
        }
        return caseChemin[bestCase];
    }

    public List<Case> GetCaseObjectif(Direction objectif)
    {
        List<Case> result = new List<Case>();
        switch (objectif)
        {
            case Direction.DROITE:
                for (int j = 0; j < Height; j++)
                {
                    result.Add(GetCase(Width - 1, j));
                }
                break;
            case Direction.GAUCHE:
                for (int j = 0; j < Height; j++)
                {
                    result.Add(GetCase(0, j));
                }
                break;
            case Direction.HAUT:
                for (int j = 0; j < Width; j++)
                {
                    result.Add(GetCase(j, 0));
                }
                break;
            case Direction.BAS:
                for (int j = 0; j < Width; j++)
                {
                    result.Add(GetCase(j, Height - 1));
                }
                break;
        }
        return result;
    }

    public int Evaluation()
    {
        var moi = Joueurs[0];
        var result = 0;
        var maDistance = Dikstra(moi.Pos, moi.Objectif).Count();
        var mesMurs = moi.MurRestant;
        for (int i = 1; i < Joueurs.Count(); i++)
        {
            var adversaire = Joueurs[i];
            var distanceAdversaire = Dikstra(adversaire.Pos, adversaire.Objectif).Count();
            result += (distanceAdversaire - maDistance) * 3;
            result += mesMurs - adversaire.MurRestant;
        }
        return result;
    }

    public override int GetHashCode()
    {
        int result = 0;
        foreach (var mur in Murs)
        {
            result = result ^ mur.GetHashCode();
        }
        return result;
    }

    public int GetDikstraHashCode(Position from, Direction objectif)
    {
        return this.GetHashCode() ^ from.GetHashCode() * 1000 ^ objectif.GetHashCode() * 1000000000;
    }
}

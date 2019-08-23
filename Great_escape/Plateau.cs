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

    public Plateau(Plateau plateau)
    {
        Width = plateau.Width;
        Height = plateau.Height;
        Cases = new List<Case>(plateau.Cases);
        Murs = new List<Mur>(plateau.Murs);
        Joueurs = new List<Joueur>(plateau.Joueurs);
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
        if (murAdd.ToString() == "") Console.Error.WriteLine("validation");
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

        if (murAdd.ToString() == "") Console.Error.WriteLine("placement ok");

        foreach (var joueur in Joueurs)
        {
            if (murAdd.ToString() == "") Console.Error.WriteLine("test joueur " + joueur.PlayerId);
            if (AddMur(murAdd, true))
            {
                try
                {
                    var result = Dikstra(joueur.Pos, joueur.Objectif);
                    if (murAdd.ToString() == "")
                    {
                        foreach (var item in result)
                        {
                            Console.Error.WriteLine(item.Pos.ToString());
                        }
                    }
                }
                catch (NoPathException)
                {
                    if (murAdd.ToString() == "") Console.Error.WriteLine("No Path");
                    return false;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.Message);
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
            if (a.From != from || a.Objectif != objectif || a.Plateau != this)
            {
                //Console.Error.WriteLine("hashCode colission");
                var result = DikstraCalculation(from, objectif);
                a = new InfoDikstra(from, objectif, result, this);
                transposition[hashCode] = a;
            }
            /*if (a.Plateau != this)
            {
                Console.Error.WriteLine("hashCode colission plateau");
                if (a.Plateau.Murs.Count == this.Murs.Count)
                {
                    for (int i = 0; i < this.Murs.Count; i++)
                    {
                        Console.Error.WriteLine("Mur " + i + " " + a.Plateau.Murs[i] + " " + this.Murs[i]);
                    }
                }
                else
                {
                    Console.Error.WriteLine("Mur count " + a.Plateau.Murs.Count + " " + this.Murs.Count);
                }
                Console.Error.WriteLine("hashCode colission plateau");
                if (a.Plateau.Joueurs.Count == this.Joueurs.Count)
                {
                    for (int i = 0; i < this.Joueurs.Count; i++)
                    {
                        Console.Error.WriteLine("Joueurs " + i + " " + a.Plateau.Joueurs[i] + " " + this.Joueurs[i]);
                    }
                }
                else
                {
                    Console.Error.WriteLine("Joueurs count " + a.Plateau.Joueurs.Count + " " + this.Joueurs.Count);
                }
            }*/
            return a.Result;
        }
        else
        {
            var lastMur = Murs.LastOrDefault();
            if (lastMur != null)
            {
                var previousHashCode = hashCode ^ Murs.LastOrDefault().GetHashCode() * 100000000;
                if (transposition.ContainsKey(previousHashCode))
                {
                    //Console.Error.WriteLine("diffenrtiel is enough");
                    bool pathOk = true;
                    var previousResult = transposition[previousHashCode].Result;
                    if (lastMur.BloqueCases(GetCase(from), previousResult[0], this)) pathOk = false;
                    for (int i = 1; i < previousResult.Count && pathOk; i++)
                    {
                        if (lastMur.BloqueCases(previousResult[i - 1], previousResult[i], this))
                        {
                            pathOk = false;
                        }
                    }
                    if (pathOk)
                    {
                        //Console.Error.WriteLine("differentiel found");
                        return previousResult;
                    }
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
            //result += mesMurs - adversaire.MurRestant;
        }
        return result;
    }

    public int GetDikstraHashCode(Position from, Direction objectif)
    {
        return this.GetHashCode() ^ from.GetHashCode() * 10000000 ^ objectif.GetHashCode() * 100000000;
    }

    #region comparator

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(obj, null) || GetType() != obj.GetType())
        {
            return false;
        }

        var a = (Plateau)obj;
        if (a.Murs.Count == Murs.Count)
        {
            foreach (var item in Murs)
            {
                if (!a.Murs.Contains(item))
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }
        if (a.Joueurs.Count == Joueurs.Count)
        {
            foreach (var item in Joueurs)
            {
                if (!a.Joueurs.Contains(item))
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    public static bool operator ==(Plateau obj1, Plateau obj2)
    {
        if (ReferenceEquals(obj1, null)) return ReferenceEquals(obj2, null);
        return obj1.Equals(obj2);
    }

    public static bool operator !=(Plateau obj1, Plateau obj2)
    {
        return !(obj1 == obj2);
    }

    public override int GetHashCode()
    {
        int result = 0;
        foreach (var mur in Murs)
        {
            result = result ^ mur.GetHashCode() * 100000000;
        }
        foreach (var joueur in Joueurs)
        {
            result = result ^ joueur.GetHashCode() * 1000000;
        }
        return result;
    }

    #endregion
}

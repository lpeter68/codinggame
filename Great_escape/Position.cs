
public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return X + " " + Y;
    }

    #region positions voisines

    public Position GetPositionInDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.DROITE: return GetPositionDroite();
            case Direction.GAUCHE: return GetPositionGauche();
            case Direction.HAUT: return GetPositionHaut();
            case Direction.BAS: return GetPositionBas();
            default: return this;
        }
    }

    public Position GetPositionHaut()
    {
        if (Y > 0)
        {
            return new Position(X, Y - 1);
        }
        else
        {
            return null;
        }
    }

    public Position GetPositionBas()
    {
        if (Y < 8) //TODO must be high
        {
            return new Position(X, Y + 1);
        }
        else
        {
            return null;
        }
    }

    public Position GetPositionDroite()
    {
        if (X < 8) //TODO must be high
        {
            return new Position(X + 1, Y);
        }
        else
        {
            return null;
        }
    }

    public Position GetPositionGauche()
    {
        if (X > 0)
        {
            return new Position(X - 1, Y);
        }
        else
        {
            return null;
        }
    }

    #endregion

    #region comparator

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(obj, null) || GetType() != obj.GetType())
        {
            return false;
        }

        var Pos = (Position)obj;
        return X == Pos.X && Y == Pos.Y;
    }

    public static bool operator ==(Position obj1, Position obj2)
    {
        if (ReferenceEquals(obj1, null)) return ReferenceEquals(obj2, null);
        return obj1.Equals(obj2);
    }

    public static bool operator !=(Position obj1, Position obj2)
    {
        return !(obj1 == obj2);
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() * 100000 ^ Y.GetHashCode();
    }
    #endregion
}

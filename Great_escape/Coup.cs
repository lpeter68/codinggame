using System;

public class Coup
{
    public Mur Mur { get; }
    public Direction? Dir { get; }

    public Coup(Mur mur)
    {
        Mur = mur;
        Dir = null;
    }

    public Coup(Direction direction)
    {
        Mur = null;
        Dir = direction;
    }

    public override string ToString()
    {
        if (Dir != null)
        {
            return Dir.ToString();
        }
        else
        {
            return Mur.ToString();
        }
    }

    public void JouerLeCoup()
    {
        if (Dir == null)
        {
            Console.WriteLine(Mur.ToString());
        }
        else
        {
            switch (Dir)
            {
                case Direction.DROITE:
                    Console.WriteLine("RIGHT");
                    break;
                case Direction.GAUCHE:
                    Console.WriteLine("LEFT");
                    break;
                case Direction.HAUT:
                    Console.WriteLine("UP");
                    break;
                case Direction.BAS:
                    Console.WriteLine("DOWN");
                    break;
            }
        }
    }
}

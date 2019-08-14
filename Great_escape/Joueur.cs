using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Joueur : IPosition
{
    private Position _pos;

    public Position Pos
    {
        get => _pos;
        set => _pos = value;
    }
    public int PlayerId { get; set; }
    public int MurRestant { get; set; }

    public Direction Objectif
    {
        get
        {
            switch (PlayerId)
            {
                case 0: return Direction.DROITE;
                case 1: return Direction.GAUCHE;
                case 2: return Direction.BAS;
                case 3: return Direction.HAUT;
                default: return Direction.DROITE;
            }
        }
    }


    public Joueur(Position pos, int playerId, int murRestant)
    {
        Pos = pos;
        PlayerId = playerId;
        MurRestant = murRestant;
    }

    public void MoveToNextCase(Case target)
    {
        if (target.Pos == Pos.GetPositionBas())
        {
            Console.WriteLine("DOWN");
        }
        if (target.Pos == Pos.GetPositionDroite())
        {
            Console.WriteLine("RIGHT");
        }
        if (target.Pos == Pos.GetPositionGauche())
        {
            Console.WriteLine("LEFT");
        }
        if (target.Pos == Pos.GetPositionHaut())
        {
            Console.WriteLine("UP");
        }
    }

}


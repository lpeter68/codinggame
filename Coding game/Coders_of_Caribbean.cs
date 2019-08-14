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
class Player
{

    public static Entity[,] game = new Entity[23, 21];

    static void Main(string[] args)
    {
        bool firstLoop = true;
        Random rnd = new Random();
        Position lastTarget = null;
        bool randomMove = true;

        List<Ship> myShips = new List<Ship>();
        List<Barrel> barrels = new List<Barrel>();
        List<Ship> otherShips = new List<Ship>();
        List<CannonBall> cannonBalls = new List<CannonBall>();
        List<Mine> mines = new List<Mine>();

        // game loop
        while (true)
        {
            barrels = new List<Barrel>();
            otherShips = new List<Ship>();
            cannonBalls = new List<CannonBall>();
            mines = new List<Mine>();

            for (int i = 0; i < game.GetLength(0); i++)
            {
                for (int j = 0; j < game.GetLength(1); j++)
                {
                    game[i, j] = Entity.EMPTY;
                }
            }

            int myShipCount = int.Parse(Console.ReadLine()); // the number of remaining ships
            int entityCount = int.Parse(Console.ReadLine()); // the number of entities (e.g. ships, mines or cannonballs)

            var shipId = 0;
            for (int i = 0; i < entityCount; i++)
            {
                string[] inputs = Console.ReadLine().Split(' ');
                int entityId = int.Parse(inputs[0]);
                string entityType = inputs[1];
                int x = int.Parse(inputs[2]);
                int y = int.Parse(inputs[3]);
                int arg1 = int.Parse(inputs[4]);
                int arg2 = int.Parse(inputs[5]);
                int arg3 = int.Parse(inputs[6]);
                int arg4 = int.Parse(inputs[7]);

                var pos = new Position(x, y);

                switch (entityType)
                {
                    case "SHIP":
                        var ship = new Ship(shipId++, arg1, arg2, arg3, arg4 == 1, pos);
                        if (ship.IsMine)
                        {
                            if (firstLoop) myShips.Add(ship); //Vérifier le mécanisme d'update tour par tour
                            else
                            {
                                var existingShip = myShips.FirstOrDefault(a => a.Id == ship.Id);
                                if (existingShip == null)
                                {
                                    Console.Error.WriteLine("ship Error");
                                    throw new Exception("ship id error");
                                }
                                ship.NextRound(arg1, arg2, arg3, pos);
                            }
                        }
                        else
                        {
                            otherShips.Add(ship);
                        }
                        break;
                    case "BARREL":
                        barrels.Add(new Barrel(arg1, pos));
                        break;
                    case "CANNONBALL":
                        cannonBalls.Add(new CannonBall(arg1, arg2, pos));
                        break;
                    case "MINE":
                        mines.Add(new Mine(pos));
                        break;
                }
            }
            for (int i = 0; i < myShipCount; i++)
            {
                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");

                var ship = myShips[i];
                if (ship.IsObjectOnRoad(mines) || ship.IsObjectOnRoad(cannonBalls.Where(c => c.ImpactIn == 1).ToList()))
                {
                    ship.DoSlower();
                }
                else if (barrels.Any() && ship.Rhum < 75)
                {
                    var target = ship.GetClosest(barrels).Pos;
                    var continueMove = ship.GetFuturePosition(1);
                    bool closerWithoutMove = continueMove.GetDist(target) < ship.Pos.GetDist(target);
                    if (closerWithoutMove)
                    {
                        lastTarget = null;
                        ship.DoFireOn(ship.GetClosest(otherShips));
                    }
                    else
                    {
                        lastTarget = target;
                        Console.WriteLine("MOVE " + target.ToString());
                    }
                }
                else
                {
                    if (randomMove)
                    {
                        Console.WriteLine("MOVE " + rnd.Next(0, 22) + " " + rnd.Next(0, 20));
                        randomMove = !randomMove;
                    }
                    else
                    {
                        randomMove = !randomMove;
                        ship.DoFireOn(ship.GetClosest(otherShips));
                    }
                }
            }
            firstLoop = false;
        }
    }
}

enum Entity
{
    BARREL, ME, OTHER, CANNONBALL, MINE, EMPTY
}

interface IEntity
{
    Entity Entity { get; }
}


class Ship : IPosition, IEntity
{
    public int Id { get; set; }
    public int Dir { get; set; }
    public int Speed { get; set; }
    public int Rhum { get; set; }
    public bool IsMine { get; set; }
    public Position Pos { get; set; }
    public int LastMine { get; set; }
    public int LastFire { get; set; }

    public Entity Entity => IsMine ? Entity.ME : Entity.OTHER;

    public Ship(int id, int dir, int speed, int rhum, bool isMine, Position pos = null)
    {
        Id = id;
        Dir = dir;
        Speed = speed;
        Rhum = rhum;
        IsMine = isMine;
        Pos = pos;
        LastMine = 5;
        LastFire = 2;
        Pos.SetOccupation(this);
    }

    public Ship NextRound(int dir, int speed, int rhum, Position pos = null)
    {
        if (dir == Dir && Speed == speed) //TODO vérifier la position
        {
            Rhum = rhum;
            Pos = pos;
            LastFire++;
            LastMine++;
        }
        else
        {
            Speed = speed;
            Dir = dir;
            Console.Error.WriteLine("move non maitrisé");
        }
        Pos.SetOccupation(this);
        return this;
    }


    public T GetClosest<T>(List<T> objs) where T : IPosition
    {
        T closetObj = default(T);
        int distToclosest = -1;
        foreach (var obj in objs)
        {
            var dist = Pos.GetDist(obj.Pos);
            if (dist < distToclosest || distToclosest == -1)
            {
                closetObj = obj;
                distToclosest = dist;
            }
        }
        return closetObj;
    }

    public bool IsObjectOnRoad<T>(List<T> objects) where T : IPosition
    {
        var currentPos = Pos.GetNeighbourInDir(Dir); //la position de l'avant
        for (int i = 0; i < Speed; i++)
        {
            currentPos = currentPos.GetNeighbourInDir(Dir);
            foreach (var obj in objects)
            {
                if (obj.Pos == currentPos) return true;
            }
        }
        return false;
    }

    public Position GetFuturePosition(int nbGame)
    {
        var currentPos = Pos;
        for (int j = 0; j < nbGame; j++)
        {
            for (int i = 0; i < Speed; i++)
            {
                currentPos = currentPos.GetNeighbourInDir(Dir);
            }
        }
        return currentPos;
    }

    public int OpossiteDir()
    {
        return (Dir + 3) % 6;
    }

    #region move

    public void DoPort()
    {
        Dir = (Dir + 1) % 6;
        Console.WriteLine("PORT");
    }

    public void DoStarboard()
    {
        Dir = (Dir - 1) % 6;
        Console.WriteLine("STARBOARD");
    }

    public void DoFaster()
    {
        if (Speed < 2)
        {
            Speed++;
            Console.WriteLine("FASTER");
        }
        else
        {
            DoWait();
        }
    }

    public void DoSlower()
    {
        if (Speed > 0)
        {
            Speed++;
            Console.WriteLine("SLOWER");
        }
        else
        {
            DoWait();
        }
    }

    public void DoMine()
    {
        if (LastMine > 4)
        {
            LastMine = 0;
            Console.WriteLine("MINE");
        }
        else
        {
            DoWait();
        }
    }

    public void DoFireOn(Ship targetShip)
    {
        if (LastMine > 1)
        {
            LastFire = 0;
            var distToTarget = Pos.GetDist(targetShip.Pos);
            var roundToTarget = (int)(distToTarget / 3);
            var targetPos = targetShip.GetFuturePosition(roundToTarget);
            Console.WriteLine("FIRE " + targetPos.ToString());
        }
        else
        {
            DoWait();
        }
    }

    public void DoWait()
    {
        Console.Error.WriteLine("WARNING : one round lost");
        Console.WriteLine("WAIT");
    }


    #endregion
}

class Barrel : IPosition, IEntity
{
    public int Rhum { get; set; }
    public Position Pos { get; set; }
    Entity IEntity.Entity => Entity.BARREL;

    public Barrel(int rhum, Position pos = null)
    {
        Rhum = rhum;
        Pos = pos;
        Pos.SetOccupation(this);
    }
}

class CannonBall : IPosition, IEntity
{
    public int ShipId { get; set; }

    public int ImpactIn { get; set; }
    public Position Pos { get; set; }

    public Entity Entity => Entity.CANNONBALL;

    public CannonBall(int shipId, int impactIn, Position pos = null)
    {
        ShipId = shipId;
        ImpactIn = impactIn;
        Pos = pos;
        Pos.SetOccupation(this);
    }
}

class Mine : IPosition, IEntity
{
    public Position Pos { get; set; }
    Entity IEntity.Entity => Entity.MINE;

    public Mine(Position pos = null)
    {
        Pos = pos;
        Pos.SetOccupation(this);
    }
}

class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int GetDist(Position pos)
    {
        var vertical = Math.Abs(Y - pos.Y);
        var horizontal = Math.Abs(X - pos.X);
        var horizontalBis = Math.Max(0, horizontal - vertical / 2);
        //TODO incorect sur cette grille
        return horizontal + vertical;
    }

    public void SetOccupation<T>(T occupant) where T : IEntity
    {
        Player.game[X, Y] = occupant.Entity;
        if (occupant.GetType() == typeof(Ship))
        {
            var ship = occupant as Ship;
            var shipFront = GetNeighbourInDir(ship.Dir);
            var shipBack = GetNeighbourInDir(ship.OpossiteDir());
            Player.game[shipFront.X, shipFront.Y] = occupant.Entity;
            Player.game[shipBack.X, shipBack.Y] = occupant.Entity;
        }
    }

    public Entity GetOccupation()
    {
        return Player.game[X, Y];
    }

    public override string ToString()
    {
        return X + " " + Y;
    }

    public Position GetNeighbourInDir(int dir)
    {
        Position neighbour = null;
        switch (dir)
        {
            case 0:
                neighbour = new Position(X + 1, Y);
                break;
            case 1:
                neighbour = new Position(X + X % 2, Y - 1);
                break;
            case 2:
                neighbour = new Position(X - 1 + X % 2, Y - 1);
                break;
            case 3:
                neighbour = new Position(X - 1, Y);
                break;
            case 4:
                neighbour = new Position(X - 1 + X % 2, Y + 1);
                break;
            case 5:
                neighbour = new Position(X + X % 2, Y + 1);
                break;
        }
        return neighbour;
    }

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
        return X.GetHashCode() ^ Y.GetHashCode();
    }
    #endregion
}

interface IPosition
{
    Position Pos { get; set; }
}
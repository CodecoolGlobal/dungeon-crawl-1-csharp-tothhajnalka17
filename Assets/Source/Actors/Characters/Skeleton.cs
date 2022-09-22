using Assets.Source.Actors.Projectile;
using Assets.Source.Core;
using DungeonCrawl.Core;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        public int MoveTimer = 50;
        public Direction Direction = Direction.Up;
        public bool Alert = false;
        public Skeleton()
        {
            Damage = 10;
            Health = 30;           
        }
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
            var player = (Player)anotherActor;
            player.ApplyDamage(Damage);
            }
            return false;
        }
        public (int,int) ScanForPlayer()
        {
            for (int i = -3; i <= 3; i++)
            {
                for (int j = -3; j <= 3; j++)
                {
                    var position = (Position.x + i, Position.y + j);
                    if (ActorManager.Singleton.GetActorAt(position) != null)
                    {
                       if (ActorManager.Singleton.GetActorAt(position) is Player)
                        {
                            Alert = true;
                            return position;
                        }
                    }
                }
            }
            return (0, 0);
        }

        protected override void OnDeath()
        {
            UserInterface.Singleton.SetText("Skeleton died.", UserInterface.TextPosition.BottomCenter);
        }

        protected override void OnUpdate(float deltaTime)
        {
            MoveTimer--;
            (int,int) destination = ScanForPlayer();
            if (destination == (0, 0))
            {
                // patrol
            }
            else
            {
                if (MoveTimer < 1)
                {
                    if (destination.Item2 != Position.y)
                    {
                        if (destination.Item2 - Position.y != 0 && destination.Item2 > Position.y)
                        {
                            Debug.Log("Player is above me I need to move up");
                            TryMove(Direction.Up);
                            MoveTimer = 250;
                            destination = (0, 0);
                        }
                        else if (destination.Item2 - Position.y != 0 && destination.Item2 < Position.y)
                        {
                            Debug.Log("Player is Below me I need to move Down");
                            TryMove(Direction.Down);
                            MoveTimer = 250;
                            destination = (0, 0);
                        }
                    }
                    else
                    {
                        if (destination.Item1 - Position.x != 0 && destination.Item1 > Position.x)
                        {
                            Debug.Log("Player is Right of me I need to move Right");
                            TryMove(Direction.Right);
                            MoveTimer = 250;
                            destination = (0, 0);
                        }
                        if (destination != (0, 0))
                        {
                            if (destination.Item1 - Position.x != 0 && destination.Item1 < Position.x)
                            {
                                Debug.Log("Player is Left of me I need to move Left");
                                TryMove(Direction.Left);
                                MoveTimer = 250;
                                destination = (0, 0);
                            }
                        }
                    }
                }
            }
        }
        public override int DefaultSpriteId => 316;
        public override string DefaultName => "Skeleton";
    }
}

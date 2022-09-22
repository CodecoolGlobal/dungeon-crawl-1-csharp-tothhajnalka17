using DungeonCrawl.Core;
using System.Collections.Generic;
using UnityEngine;
using DungeonCrawl.Actors.Static;
using Assets.Source.Actors.Static;
using Assets.Source.Core;
using Assets.Source.Actors.Items;
using Assets.Source.Actors.Projectile;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        public int DistanceTimer = 0;
        public int LevelClearCount = 0;
        public int FlipendoCooldown = 0;
        public int ObliviateCooldown = 0;
        public bool WandEquipped = false;
        public bool ObliviateUnlocked = false;
        public int BlinkCooldown = 0;
        public bool BlinkUnlocked = false;
        private Direction _direction;

        public List<Actor> Inventory = new List<Actor>();

        public Player()
        {
            Damage = 5;
            Health = 50;
            _direction = Direction.Up;
        }
        protected override void OnUpdate(float deltaTime)
        {
            CameraController.Singleton.Position = this.Position;
            if (DistanceTimer == 0)
            {
                CameraController.Singleton.Size = 6;
                UserInterface.Singleton.SetText(" ", UserInterface.TextPosition.BottomCenter);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                // Move up
                TryMove(Direction.Up);
                _direction = Direction.Up;
                if (DistanceTimer > 0) DistanceTimer--;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                // Move down
                TryMove(Direction.Down);
                _direction = Direction.Down;
                if (DistanceTimer > 0) DistanceTimer--;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                // Move left
                TryMove(Direction.Left);
                _direction = Direction.Left;
                if (DistanceTimer > 0) DistanceTimer--;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                // Move right
                TryMove(Direction.Right);
                _direction = Direction.Right;
                if (DistanceTimer > 0) DistanceTimer--;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (WandEquipped != true)
                {
                    foreach (var item in Inventory)
                    {
                        if (item is Wand)
                        {
                            WandEquipped = true;
                        }
                    }
                }
                if (WandEquipped == true)
                {
                    if(FlipendoCooldown < 1)
                    {
                        Launch("Flipendo");
                        FlipendoCooldown = 100;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (ObliviateUnlocked)
                {
                    List<Actor> neighbours = new List<Actor>();
                    if (ObliviateCooldown < 1)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                var position = (Position.x + i, Position.y + j);
                                if (ActorManager.Singleton.GetActorAt(position) != null)
                                {
                                    ActorManager.Singleton.Spawn<Obliviate>(position).OnCollision(ActorManager.Singleton.GetActorAt(position));
                                }
                                else
                                {
                                    ActorManager.Singleton.Spawn<Obliviate>(position);
                                }
                            }
                        }
                        ObliviateCooldown = 360;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (BlinkUnlocked)
                {
                    if (BlinkCooldown < 1)
                    {
                        Launch("Blink");
                    }
                }
            }
            UserInterface.Singleton.SetText($"Health: {Health}", UserInterface.TextPosition.TopLeft);
            if (WandEquipped)
            {
                UserInterface.Singleton.SetText($"Q:{BlinkCooldown} E:{FlipendoCooldown} F:{ObliviateCooldown}", UserInterface.TextPosition.BottomRight);
            }
            else
            {
                UserInterface.Singleton.SetText($"Q:{BlinkCooldown} F:{ObliviateCooldown}", UserInterface.TextPosition.BottomRight);
            }
           
            CameraController.Singleton.Position = ActorManager.Singleton.GetActorAt(Position).Position;
            if (FlipendoCooldown > 0) FlipendoCooldown--;
            if (ObliviateCooldown > 0) ObliviateCooldown--;
            if (BlinkCooldown > 0) BlinkCooldown--;
        }

        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        protected override void OnDeath()
        {
            Debug.Log("Oh no, I'm dead!");
        }

        public void AddToInvetory(Actor item)
        {
            Inventory.Add(item);
        }

        public void Launch(string spellName)
        {
            if (spellName == "Flipendo")
            {
                var spell = ActorManager.Singleton.Spawn<Flipendo>(Position);
                spell.Direction = _direction;
            }
            if (spellName == "Obliviate")
            {
                var spell = ActorManager.Singleton.Spawn<Obliviate>(Position);
                spell.Direction = _direction;
            }
            if (spellName == "Blink")
            {
                int distance = 5;
                if (_direction == Direction.Up)
                {
                    var possiblePosition = (Position.x, Position.y + distance);
                    if (ActorManager.Singleton.GetActorAt(possiblePosition) is Wall)
                    {
                        distance--;
                    }
                    if (ActorManager.Singleton.GetActorAt(possiblePosition) is Wall)
                    {
                        distance--;
                    }
                    if (ActorManager.Singleton.GetActorAt(possiblePosition) is Wall)
                    {
                    }
                    else
                    {
                        Position = possiblePosition;
                        BlinkCooldown = 600;
                    }
                }
                else if (_direction == Direction.Down)
                {
                var possiblePosition = (Position.x, Position.y - distance);
                    if (ActorManager.Singleton.GetActorAt(possiblePosition) is Wall)
                    {
                        distance--;
                    }
                    if (ActorManager.Singleton.GetActorAt(possiblePosition) is Wall)
                    {
                        distance--;
                    }
                    if (ActorManager.Singleton.GetActorAt(possiblePosition) is Wall)
                    {
                    }
                    else
                    {
                            Position = possiblePosition;
                            BlinkCooldown = 600;
                    }
                }
                else if (_direction == Direction.Left)
                {
                var possiblePosition = (Position.x - distance, Position.y);
                    if (ActorManager.Singleton.GetActorAt(possiblePosition) is Wall)
                    {
                        distance--;
                    }
                    if (ActorManager.Singleton.GetActorAt(possiblePosition) is Wall)
                    {
                        distance--;
                    }
                    if (ActorManager.Singleton.GetActorAt(possiblePosition) is Wall)
                    {
                    }
                    else
                    {
                        Position = possiblePosition;
                        BlinkCooldown = 600;
                    }
                }
                else if (_direction == Direction.Right)
                {
                    var possiblePosition = (Position.x + distance, Position.y);
                    if (ActorManager.Singleton.GetActorAt(possiblePosition) is Wall)
                    {
                        distance--;
                    }
                    if (ActorManager.Singleton.GetActorAt(possiblePosition) is Wall)
                    {
                        distance--;             
                    }
                    if (ActorManager.Singleton.GetActorAt(possiblePosition) is Wall)
                    {
                    }
                    else
                    {
                        Position = possiblePosition;
                        BlinkCooldown = 600;
                    }
                }
            }
        }
        

        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Player";
    }
}

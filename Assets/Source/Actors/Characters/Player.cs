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
                List<Actor> neighbours = new List<Actor>();
                if (ObliviateCooldown < 1)
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            var position = (Position.x + i, Position.y + j);

                            ActorManager.Singleton.Spawn<Obliviate>(position);
                        }
                    }
                    ObliviateCooldown = 200;
                }
            }
            UserInterface.Singleton.SetText($"Health: {Health}", UserInterface.TextPosition.TopLeft);
            if (WandEquipped)
            {
                UserInterface.Singleton.SetText($"E:{FlipendoCooldown} F:{ObliviateCooldown}", UserInterface.TextPosition.BottomRight);
            }
            else
            {
                UserInterface.Singleton.SetText($"F:{ObliviateCooldown}", UserInterface.TextPosition.BottomRight);
            }
           
            CameraController.Singleton.Position = ActorManager.Singleton.GetActorAt(Position).Position;
            if (FlipendoCooldown > 0) FlipendoCooldown--;
            if (ObliviateCooldown > 0) ObliviateCooldown--;
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
        }

        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Player";
    }
}

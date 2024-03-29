﻿using DungeonCrawl.Core;
using System.Collections.Generic;
using UnityEngine;
using DungeonCrawl.Actors.Static;
using Assets.Source.Core;
using Assets.Source.Actors.Items;
using Assets.Source.Actors.Projectile;
using System.Text;
using DungeonCrawl.Actors.Items;

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
        public int SkeletonsKilled = 0;

        public List<Item> Inventory = new List<Item>();

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
                SetSprite(1003);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                // Move right
                TryMove(Direction.Right);
                _direction = Direction.Right;
                if (DistanceTimer > 0) DistanceTimer--;
                SetSprite(1000);
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

                UserInterface.Singleton.SetText($"F:{ObliviateCooldown}", UserInterface.TextPosition.BottomRight);


            }

            DisplayInventory();

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
            ActorManager.Singleton.DestroyAllActors();
            var skeletonsKilled = SkeletonsKilled;
            Player player = ActorManager.Singleton.Spawn<Player>(3, -3);
            MapLoader.LoadMap(666);
            UserInterface.Singleton.SetText($"Your kill count for this run: {skeletonsKilled}", UserInterface.TextPosition.MiddleCenter);
        }

        public void AddToInvetory(Item item)
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
                var normalVector = Utilities.ToVector(_direction);
                for (int i = 5; i > 2; i--)
                {
                    var possiblePosition = (Position.x + (normalVector.x * i), Position.y + (normalVector.y * i));
                    if (!(ActorManager.Singleton.GetActorAt(possiblePosition) is Wall) && ActorManager.Singleton.GetAnyActorAt(possiblePosition) != null)
                    {
                        Position = possiblePosition;
                        BlinkCooldown = 666;
                        break;
                    }
                }
            }
        }
                
        
        

        public bool DisplayInventory()
        {
            StringBuilder InventoryText = new StringBuilder();
            InventoryText.Append("Inventory:");
            InventoryText.AppendLine();

            for (var i = 0; i < Inventory.Count; i++)
            {
                InventoryText.Append(Inventory[i].DefaultName);
                if (i < Inventory.Count - 1)
                {
                    InventoryText.Append(", ");
                }
            }
            UserInterface.Singleton.SetText(InventoryText.ToString(), UserInterface.TextPosition.BottomLeft);
            return true;
        }

        public override int DefaultSpriteId => 1000;
        public override string DefaultName => "Player";
    }
}

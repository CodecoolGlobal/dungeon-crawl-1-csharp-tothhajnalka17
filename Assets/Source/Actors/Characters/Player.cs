using DungeonCrawl.Core;
using System.Collections.Generic;
using UnityEngine;
using DungeonCrawl.Actors.Static;
using Assets.Source.Actors.Static;
using Assets.Source.Core;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        public int LevelClearCount = 0;

        public List<Actor> Inventory = new List<Actor>();
        protected override void OnUpdate(float deltaTime)
        {
            CameraController.Singleton.Position = this.Position;
            if (Input.GetKeyDown(KeyCode.W))
            {
                // Move up
                TryMove(Direction.Up);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                // Move down
                TryMove(Direction.Down);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                // Move left
                TryMove(Direction.Left);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                // Move right
                TryMove(Direction.Right);
            }
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

        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Player";
    }
}

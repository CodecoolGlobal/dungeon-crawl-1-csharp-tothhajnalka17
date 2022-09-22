using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors;
using DungeonCrawl.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Source.Actors.Projectile;
using UnityEngine;

namespace Assets.Source.Actors.Static
{
    public class Launcher : Character
    {
        public double Cooldown = 2;
        public override int DefaultSpriteId => 292;
        public override string DefaultName => "Launcher";
        public override bool Detectable => true;
        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        protected override void OnDeath()
        {
            Debug.Log("Launcher died?");
        }
        protected override void OnUpdate(float deltaTime)
        {
            Cooldown--;
            if (Cooldown < 1)
            {
                Launch(4);
                Cooldown = 1200;
            }
        }
        public void Launch(int mode)
        {
            if (mode.Equals(4))
            {
                var book1 = ActorManager.Singleton.Spawn<Book>(Position);
                book1.Direction = DungeonCrawl.Direction.Right;
                var book2 = ActorManager.Singleton.Spawn<Book>(Position);
                book2.Direction = DungeonCrawl.Direction.Down;
                var book3 = ActorManager.Singleton.Spawn<Book>(Position);
                book3.Direction = DungeonCrawl.Direction.Left;
                var book4 = ActorManager.Singleton.Spawn<Book>(Position);
                book4.Direction = DungeonCrawl.Direction.Up;
            }
        }
    }
}

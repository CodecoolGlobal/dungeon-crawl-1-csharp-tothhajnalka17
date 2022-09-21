using Assets.Source.Core;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors;
using DungeonCrawl.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawl.Actors.Static;
using DungeonCrawl.Actors.Items;
using DungeonCrawl;
using UnityEngine;

namespace Assets.Source.Actors.Projectile
{
    public class Book : Projectile
    {
        public override int DefaultSpriteId => 729;
        public override string DefaultName => "Book";
        public override bool Detectable => true;
        public Book()
        {
            Direction = DefaultDirection;
            Damage = 10;
        }
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                var player = (Player)anotherActor;
                player.ApplyDamage(Damage);
            }
            if (anotherActor is Skeleton)
            {
                var enemy = (Skeleton)anotherActor;
                enemy.ApplyDamage(Damage);
            }
            
            ActorManager.Singleton._allActors.Remove(this);
            ActorManager.Singleton.DestroyActor(this);
            return true;
        }

        protected override void OnUpdate(float deltatime)
        {
            LifeTime++;
            if (LifeTime > 36)
            {
                TryMove(Direction);
                LifeTime = 0;
            }
        }
    }
}

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
using Assets.Source.Actors.Static;

namespace Assets.Source.Actors.Projectile
{
    public class Flipendo : Projectile
    {
        public override int DefaultSpriteId => 666;
        public override string DefaultName => "Flipendo";
        public override bool Detectable => true;
        public Flipendo()
        {
            Direction = DefaultDirection;
            LifeTime = 0;
            Damage = 10;
        }
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Skeleton)
            {
                var enemy = (Skeleton)anotherActor;
                enemy.ApplyDamage(Damage);
                ActorManager.Singleton._allActors.Remove(this);
                ActorManager.Singleton.DestroyActor(this);
                return false;
            }
            if (anotherActor is Teleport)
            {
                
                return false;
            }
            else
            {
                ActorManager.Singleton._allActors.Remove(this);
                ActorManager.Singleton.DestroyActor(this);
                return false;
            }
            
        }

        protected override void OnUpdate(float deltatime)
        {
            LifeTime++;
            if (LifeTime > 24)
            {
                TryMove(Direction);
                LifeTime = 0;
            }
        }
    }
}
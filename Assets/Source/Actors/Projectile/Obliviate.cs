using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors;
using DungeonCrawl.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawl;
using UnityEngine;

namespace Assets.Source.Actors.Projectile
{
    public class Obliviate : Projectile
    {
        public override int DefaultSpriteId => 442;
        public override string DefaultName => "Obliviate";
        public override bool Detectable => true;
        public Obliviate()
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
            }
            if (anotherActor.OnCollision(this))
            {
                return true;
            }
            else
            {
                ActorManager.Singleton._allActors.Remove(this);
                ActorManager.Singleton.DestroyActor(this);
                return true;
            }

        }

        protected override void OnUpdate(float deltatime)
        {
        }
    }
}

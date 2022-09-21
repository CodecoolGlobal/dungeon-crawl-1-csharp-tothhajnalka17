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
using DungeonCrawl.Actors.Static;

namespace Assets.Source.Actors.Projectile
{
    public class Obliviate : Projectile
    {
        public int Duration;
        public override int DefaultSpriteId => 554;
        public override string DefaultName => "Obliviate";
        public override bool Detectable => true;
        public Obliviate()
        {
            Duration = 30;
            Direction = DefaultDirection;
            LifeTime = 0;
            Damage = 7;
            if (ActorManager.Singleton.GetActorAt(Position) != null)
            {
                OnCollision(ActorManager.Singleton.GetActorAt(Position));
            }
        }
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Skeleton)
            {
                var enemy = (Skeleton)anotherActor;
                enemy.ApplyDamage(Damage);
                ActorManager.Singleton._allActors.Remove(this);
                ActorManager.Singleton.DestroyActor(this);
            }
            ActorManager.Singleton._allActors.Remove(this);
            ActorManager.Singleton.DestroyActor(this);
            return false;

        }

        protected override void OnUpdate(float deltatime)
        {
            Duration--;
            if (Duration < 1)
            {
                ActorManager.Singleton._allActors.Remove(this);
                ActorManager.Singleton.DestroyActor(this);
            }
        }
    }
}

using DungeonCrawl.Actors;
using DungeonCrawl.Core;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Source.Actors.Static
{
    public class DestructableDoor : Door
    {
        public int Health = 1;
        public DestructableDoor()
        {
            Health = 1;
        }
        public override int DefaultSpriteId => 123;
        public override string DefaultName => "DestructableDoor";
        public override bool Detectable => true;
        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }
        public void ApplyDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                // Die
                OnDeath();

                ActorManager.Singleton.DestroyActor(this);
            }
        }
        public void OnDeath()
        {
            Debug.Log("Sajt");
        }
    }
}

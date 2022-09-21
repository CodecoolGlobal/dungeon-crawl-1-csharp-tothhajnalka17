using Assets.Source.Core;
using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        public Skeleton()
        {
            Damage = 10;
            Health = 30;
        }
        public override bool OnCollision(Actor anotherActor)
        {
            foreach (var actor in ActorManager.Singleton._allActors)
            {
                if (actor is Player)
                {
                    var player = (Player)actor;
                    player.ApplyDamage(Damage);
                }
            }
            return false;
        }

        protected override void OnDeath()
        {
            UserInterface.Singleton.SetText("Skeleton died.", UserInterface.TextPosition.BottomCenter);
        }

        public override int DefaultSpriteId => 316;
        public override string DefaultName => "Skeleton";
    }
}

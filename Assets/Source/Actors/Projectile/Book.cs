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

namespace Assets.Source.Actors.Projectile
{
    public class Book : Actor
    {
        public int Damage;

        public Direction Direction;
        public override int DefaultSpriteId => 559;
        public override string DefaultName => "Key";
        public override bool Detectable => true;

        public Book(Direction direction)
        {
            Direction = direction;
            Damage = 10;
        }
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                Player player = (Player)anotherActor;
                player.ApplyDamage(Damage);
                ActorManager.Singleton.DestroyActor(this);
            }
            else if (anotherActor is Skeleton)
            {
                Skeleton skeleton = (Skeleton)anotherActor;
                skeleton.ApplyDamage(Damage);
                ActorManager.Singleton.DestroyActor(this);
            }
            else if (anotherActor is Wall || anotherActor is Item)
            {
                ActorManager.Singleton.DestroyActor(this);
            }

            return false;
        }

        protected override void OnUpdate(float deltatime)
        {

        }
    }
}

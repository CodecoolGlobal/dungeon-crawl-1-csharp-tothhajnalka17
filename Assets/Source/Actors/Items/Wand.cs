using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors;
using DungeonCrawl.Core;
using System.Collections;
using UnityEngine;

namespace Assets.Source.Actors.Static
{
    public class Wand : Actor
    {
        public override int DefaultSpriteId => 178;
        public override string DefaultName => "Wand";
        public override bool Detectable => true;
        public override bool IsItem()
        {
            return true;
        }

        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                Player player = (Player)anotherActor;
                player.AddToInvetory(this);
                ActorManager.Singleton.DestroyActor(this);
            }
            return false;
        }
    }
}
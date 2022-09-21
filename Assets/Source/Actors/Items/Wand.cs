using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors;
using DungeonCrawl.Core;
using DungeonCrawl.Actors.Items;
using UnityEngine;

namespace Assets.Source.Actors.Items
{
    public class Wand : Item
    {
        public override int DefaultSpriteId => 178;
        public override string DefaultName => "Wand";
        public override bool Detectable => true;
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
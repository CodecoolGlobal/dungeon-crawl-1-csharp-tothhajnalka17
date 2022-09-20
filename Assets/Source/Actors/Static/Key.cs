using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source.Actors.Static
{
    public class Key : Actor
    {
        public override int DefaultSpriteId => 559;
        public override string DefaultName => "Key";
        public override bool Detectable => true;
        public override bool IsItem()
        {
            return true;
        }

        public override bool OnCollision(Actor anotherActor)
        {
            Debug.Log("were here");
            if (anotherActor is Player)
            {
                Player player = (Player)anotherActor;
                
                player.AddToInvetory(this);
                ActorManager.Singleton.DestroyActor(this);
            }
            return base.OnCollision(anotherActor);
        }
    }
}

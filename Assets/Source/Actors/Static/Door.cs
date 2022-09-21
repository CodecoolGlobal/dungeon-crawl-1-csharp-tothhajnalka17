using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.Actors.Static
{
    public class Door : Actor
    {
        public override int DefaultSpriteId => 146;
        public override string DefaultName => "Door";
        public override bool Detectable => true;
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                Player player = (Player)anotherActor;
                foreach (var item in player.Inventory)
                {
                    if (item is Key)
                    {
                        player.Inventory.Remove(item);
                        ActorManager.Singleton.DestroyActor(this);
                        ActorManager.Singleton.Spawn<OpenDoor>(this.Position);
                        break;
                    }
                }
            }
            return false;
        }
    }
}

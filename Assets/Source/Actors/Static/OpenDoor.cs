using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors;
using DungeonCrawl.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.Actors.Static
{
    public class OpenDoor : Door
    {
        public override int DefaultSpriteId => 147;
        public override string DefaultName => "OpenDoor";
        public override bool Detectable => true;
        public override bool OnCollision(Actor anotherActor)
        {
            
            return false;
        }
    }
}

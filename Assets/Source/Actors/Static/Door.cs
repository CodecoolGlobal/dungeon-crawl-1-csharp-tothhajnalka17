using DungeonCrawl.Actors;
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
        public override bool Detectable => false;


    }
}

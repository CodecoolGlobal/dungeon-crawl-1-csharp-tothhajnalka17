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
    public class Launcher : Actor
    {
        public override int DefaultSpriteId => 292;
        public override string DefaultName => "Launcher";
        public override bool Detectable => true;
        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }
    }
}

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
    public class Exit : Actor
    {
        public override int DefaultSpriteId => 432;
        public override string DefaultName => "Exit";
        public override bool Detectable => true;
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                var player = (Player)anotherActor;
                player.LevelClearCount += 1;
                MapLoader.LoadMap(player.LevelClearCount);
            }
            return false;
        }
    }
}

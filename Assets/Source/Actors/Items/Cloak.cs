using Assets.Source.Core;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors;
using DungeonCrawl.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawl.Actors.Items;

namespace Assets.Source.Actors.Items
{
    public class Cloak : Item
    {
        public override int DefaultSpriteId => 178;
        public override string DefaultName => "Cloak";
        public override bool Detectable => true;
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                Player player = (Player)anotherActor;
                player.AddToInvetory(this);
                UserInterface.Singleton.SetText("Blink Unlocked! Use Q to cast.", UserInterface.TextPosition.BottomCenter);
                CameraController.Singleton.Size -= 3;
                player.DistanceTimer = 4;
                player.BlinkUnlocked = true;
                ActorManager.Singleton.DestroyActor(this);
            }
            return false;
        }
    }
}

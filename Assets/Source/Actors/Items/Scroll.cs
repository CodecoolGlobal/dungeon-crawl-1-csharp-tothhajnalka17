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
    public class Scroll : Item
    {
        public override int DefaultSpriteId => 222;
        public override string DefaultName => "Key";
        public override bool Detectable => true;
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                Player player = (Player)anotherActor;
                player.AddToInvetory(this);
                UserInterface.Singleton.SetText("Obliviate Unlocked! Use F To Cast.", UserInterface.TextPosition.BottomCenter);
                CameraController.Singleton.Size -= 3;
                player.DistanceTimer = 2;
                player.ObliviateUnlocked = true;
                ActorManager.Singleton.DestroyActor(this);
            }
            return false;
        }
    }
}

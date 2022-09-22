using Assets.Source.Core;
using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core;
using DungeonCrawl.Actors.Items;

using UnityEngine;

namespace Assets.Source.Actors.Items
{
    public class Key : Item
    {
        public override int DefaultSpriteId => 559;
        public override string DefaultName => "Key";
        public override bool Detectable => true;
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                Player player = (Player)anotherActor;
                player.AddToInvetory(this);
                UserInterface.Singleton.SetText("You GrinGotts that key Hairy!", UserInterface.TextPosition.BottomCenter);
                CameraController.Singleton.Size -= 1;
                player.DistanceTimer = 3;
                ActorManager.Singleton.DestroyActor(this);
            }
            return false;
        }
    }
}

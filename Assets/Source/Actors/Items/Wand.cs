using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors;
using DungeonCrawl.Core;
using DungeonCrawl.Actors.Items;
using UnityEngine;
using Assets.Source.Core;

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
                UserInterface.Singleton.SetText("Flipendo Unlocked! Use E to cast.", UserInterface.TextPosition.BottomCenter);
                CameraController.Singleton.Size -= 3;
                player.DistanceTimer = 4;
                ActorManager.Singleton.DestroyActor(this);
            }
            return false;
        }
    }
}
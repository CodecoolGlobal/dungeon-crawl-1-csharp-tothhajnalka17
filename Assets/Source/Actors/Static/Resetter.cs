using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors;
using DungeonCrawl.Core;
using Assets.Source.Core;

namespace Assets.Source.Actors.Static
{
    public class Resetter : Actor
    {
        public override int DefaultSpriteId => 333;
        public override string DefaultName => "Resetter";
        public override bool Detectable => true;
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                ActorManager.Singleton.DestroyActor(anotherActor);
                UserInterface.Singleton.SetText($" ", UserInterface.TextPosition.MiddleCenter);
                ActorManager.Singleton.DestroyAllActors();
                CameraController.Singleton.Position = (0, 0);
                Player player = ActorManager.Singleton.Spawn<Player>(0, 0);
                MapLoader.LoadMap(0);
            }
            return false;
        }
    }
}
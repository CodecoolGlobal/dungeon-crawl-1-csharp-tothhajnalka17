using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors;
using DungeonCrawl.Core;

namespace Assets.Source.Actors.Static
{
    public class Teleport : Actor
    {
        public (int, int) TargetPosition = (0, 0);
        public Teleport()
        {
        }
        public override int DefaultSpriteId => 9;
        public override string DefaultName => "Teleport";
        public override bool Detectable => true;
        public override bool OnCollision(Actor anotherActor)
        {
            foreach (var actor in ActorManager.Singleton._allActors)
            {
                if (actor is Teleport && actor != this)
                {
                    anotherActor.Position = actor.Position;
                }
            }
            return false;
        }
    }
}

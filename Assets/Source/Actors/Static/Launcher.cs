using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors;
using DungeonCrawl.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Source.Actors.Projectile;

namespace Assets.Source.Actors.Static
{
    public class Launcher : Actor
    {
        public double Cooldown = 10;
        public override int DefaultSpriteId => 292;
        public override string DefaultName => "Launcher";
        public override bool Detectable => true;
        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }
        protected override void OnUpdate(float deltaTime)
        {
            Cooldown--;
            if (Cooldown < 1)
            {
                Launch(4);
            }
        }
        public void Launch(int howMany)
        {
            if (howMany.Equals(4))
            {

            }
        }
    }
}

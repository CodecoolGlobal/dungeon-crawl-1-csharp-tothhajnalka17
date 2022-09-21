using DungeonCrawl;
using DungeonCrawl.Actors;
using System.Collections;
using UnityEngine;

namespace Assets.Source.Actors.Projectile
{
    public abstract class Projectile : Actor
    {
        public int LifeTime = 0;
        public int Damage = 10;

        public Direction Direction;
        public override int DefaultSpriteId => 729;
        public override string DefaultName => "Book";
        public override bool Detectable => true;

        public Direction DefaultDirection = Direction.Up;
    }
}
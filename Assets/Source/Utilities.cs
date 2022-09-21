using System;
using System.Collections.Generic;

namespace DungeonCrawl
{
    public enum Direction : byte
    {
        Up,
        Down,
        Left,
        Right
    }

    public static class Utilities
    {
        public static (int x, int y) ToVector(this Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return (0, 1);
                case Direction.Down:
                    return (0, -1);
                case Direction.Left:
                    return (-1, 0);
                case Direction.Right:
                    return (1, 0);
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
        }

        public static Direction RandomDirection()
        {
            Random randomDirection = new Random();
            Direction[] directions = { Direction.Up, Direction.Down, Direction.Left, Direction.Right }; 
            int randomIndex = randomDirection.Next(directions.Length);
            return directions[randomIndex];
        }
    }
}

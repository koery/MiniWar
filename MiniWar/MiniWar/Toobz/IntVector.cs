using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MiniWar;

namespace VSZombie.Toobz
{
    public struct IntVector
    {
        public int X;
        public int Y;
        public IntVector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public IntVector(float x, float y)
        {
            X = (int)x;
            Y = (int)y;
        }

        public IntVector(Vector2 vector2)
        {
            X = (int)vector2.X;
            Y = (int)vector2.Y;
        }

        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }

        public Vector2 ToUIVector2()
        {
            return new Vector2(X, Y) * GameConfig.GirdSize + GameConfig.RelativeOrigin;
        }

        public Vector2 ToXNAUIVector2()
        {
            var v = new Vector2(X, Y) * GameConfig.GirdSize;
            return MGFramework.MGDirector.SharedDirector().ConvertToGamePos(v) + GameConfig.RelativeOrigin;
        }

        public static IntVector ToGridIntVector(Vector2 v)
        {
            return new IntVector((v - GameConfig.RelativeOrigin) / GameConfig.GirdSize);
        }

        public static IntVector operator +(IntVector v1, IntVector v2)
        {
            return new IntVector(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static IntVector operator -(IntVector v1, IntVector v2)
        {
            return new IntVector(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static IntVector operator *(IntVector v1, IntVector v2)
        {
            return new IntVector(v1.X * v2.X, v1.Y * v2.Y);
        }

        public static IntVector operator /(IntVector v1, IntVector v2)
        {
            return new IntVector(v1.X / v2.X, v1.Y / v2.Y);
        }

        public static bool operator ==(IntVector lPoint, IntVector rPoint)
        {
            return rPoint.X == lPoint.X && rPoint.Y == lPoint.Y;
        }

        public static bool operator !=(IntVector lPoint, IntVector rPoint)
        {
            return lPoint.X != rPoint.X || lPoint.Y != rPoint.Y;
        }
    }
}

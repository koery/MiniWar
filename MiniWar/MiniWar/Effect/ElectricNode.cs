using System;
using Microsoft.Xna.Framework;

namespace VSZombie.Effect
{
    public class ElectricNode
    {
        #region Properties

        private readonly float _speedFactor = 1f;
        public float Radius = 10;
        private Vector2 FiducialPoint { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        #endregion

        #region Constructor

        public ElectricNode(Vector2 fiducialPoint, float radius, float? speedFactor)
        {
            FiducialPoint = fiducialPoint;
            Radius = radius;
            _speedFactor = speedFactor ?? 1f;

            Reset();
        }

        #endregion

        #region Update & Draw

        public void Update()
        {
            if (_movementTime < _movementDuration)
            {
                Position += Velocity;
                _movementTime++;
            }
            else
            {
                Reset();
            }
        }

        #endregion

        #region Movement

        private const int MinSpeed = 1;
        private const int MaxSpeed = 10;
        private static readonly Random Random = new Random();
        private int _movementDuration;
        private int _movementTime;

        public void Reset()
        {
            //reset position
            Position = FiducialPoint;

            //reset Velocity
            var rawVelocity = new Vector2(Random.Next(MinSpeed, MaxSpeed), 0);
            float angle = MathHelper.ToRadians(Random.Next(360));
            Velocity = Vector2.Transform(rawVelocity, Matrix.CreateRotationZ(angle));
            Velocity = Velocity * _speedFactor;
            _movementDuration = (int)(Radius / Velocity.Length());
            _movementTime = 0;
        }

        #endregion
    }
}
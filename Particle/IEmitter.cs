using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject0
{
    public interface IEmitter
    {
        public Vector2 Position { get; }

        public Vector2 Velocity { get; }
    }
}

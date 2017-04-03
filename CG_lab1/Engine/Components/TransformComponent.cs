using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Engine.Core;

namespace Engine.Components
{
    public class TransformComponent : Component
    {
        //Holds data such as position, rotation and scaling
        Vector3 speed = new Vector3(0.1f, 0.1f, 0.1f);

        private Vector3 scale;
        private Vector3 position;
        private Matrix rotation;
        private Matrix objectWorld;

        public TransformComponent(Vector3 scale, Vector3 position, Matrix rotation, Matrix objectWorld, Vector3 speed)
        {
            this.scale = scale;
            this.position = position;
            this.rotation = rotation;
            this.objectWorld = objectWorld;
            this.speed = speed;
        }
    }
}

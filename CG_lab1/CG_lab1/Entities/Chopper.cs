﻿using Engine;
using Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Engine.Core;

namespace CG_lab1.Entities
{
    public class Chopper
    {
        public static Component[] createComponents(Vector3 scale, Vector3 position, Matrix rotation, Matrix objectWorld, Vector3 speed)
        {
            return new Component[]
            {
                new CameraComponent(),
                new HeightmapComponent(),
                new ModelComponent("Chopper"),
                new TransformComponent(scale, position, rotation, objectWorld, speed)
            };
        }
    }
}
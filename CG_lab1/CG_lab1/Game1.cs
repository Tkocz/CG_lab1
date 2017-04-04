using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Manager.Core;
using Manager;
using Manager.Subsystems;
using CG_lab1.Entities;
using Manager.Components;

namespace CG_lab1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : GameImpl
    {
        static Matrix world = Matrix.Identity;
        Entity chopper;
        public override void init()
        {
            Engine.GetInst().Subsystems.Add(new CameraSystem());
            Engine.GetInst().Subsystems.Add(new HeightmapSystem());
            Engine.GetInst().Subsystems.Add(new ModelSystem(world));
            Engine.GetInst().Subsystems.Add(new TransformSystem());
            Engine.GetInst().Subsystems.Add(new InputSystem());

            chopper = Engine.GetInst().addEntity(Chopper.createComponents(
                new Vector3(5, 5, 5), 
                new Vector3(0, 0, -50),
                world, 
                world,
                new Vector3(0.1f, 0.1f, 0.1f)
                ));
        }

        public override void update(GameTime gameTime)
        {
        }
    }
}
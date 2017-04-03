using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Engine.Core;
using Engine;
using Engine.Subsystems;
using CG_lab1.Entities;
using Engine.Components;

namespace CG_lab1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : GameImpl
    {
        Entity chopper;
        public override void init()
        {
            Engine.Engine.GetInst().Managers.Add(new CameraSystem());
            Engine.Engine.GetInst().Managers.Add(new HeightmapSystem());
            Engine.Engine.GetInst().Managers.Add(new ModelSystem());
            Engine.Engine.GetInst().Managers.Add(new TransformSystem());

            chopper = Engine.Engine.GetInst().addEntity(Chopper.createComponents(
                new Vector3(5, 5, 5), 
                new Vector3(0, 0, -50),
                Matrix.Identity, 
                Matrix.Identity,
                new Vector3(0.1f, 0.1f, 0.1f)
                ));
        }

        public override void update(GameTime gameTime, Matrix world, Matrix view, Matrix projection)
        {
            throw new NotImplementedException();
        }
    }
}
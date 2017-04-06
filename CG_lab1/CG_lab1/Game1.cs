using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Manager.Core;
using Manager;
using Manager.Subsystems;
using CG_lab1.Entities;
using Manager.Components;
using System.Collections.Generic;

namespace CG_lab1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : GameImpl
    {
        static Matrix world = Matrix.Identity;
        Entity chopper;
        private Model skyModel;
        private BasicEffect skyEffect;
        Matrix worldMatrix;
        private Matrix viewM, projM, skyworldM, shipworldM;
        private static float skyscale = 10000f;
        public override void init()
        {
            Engine.GetInst().Window.Title = "Get to the Choppaaaaargh!";
            Engine.GetInst().Subsystems.Add(new SkyboxSystem());
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
            Engine.GetInst().addEntity(Chopper.createComponents(
                new Vector3(5, 5, 5),
                new Vector3(20, 20, -50),
                world,
                world,
                new Vector3(0.1f, 0.1f, 0.1f)
                ));
            skyModel = Engine.GetInst().Content.Load<Model>("skybox");
            skyEffect = (BasicEffect)skyModel.Meshes[0].Effects[0];
        }

        public override void update(GameTime gameTime)
        {
            skyworldM = Matrix.CreateScale(skyscale, skyscale, skyscale);
            viewM = Matrix.CreateTranslation(new Vector3(20, 20, -50)) *
                 Matrix.CreateLookAt(new Vector3(20, 20, -50), new Vector3(20, 20, -50), Vector3.Up) * shipworldM;
            projM = Matrix.CreatePerspectiveFieldOfView(MathHelper.Pi / 3, 1f, 1f, 10f * skyscale);
            Engine.GetInst().GraphicsDevice.RasterizerState = RasterizerState.CullNone;
        }
        public override void draw(GameTime gameTime)
        {
            Engine.GetInst().GraphicsDevice.Clear(Color.DarkBlue);
            skyEffect.World = skyworldM;
            skyEffect.View = viewM;
            skyEffect.Projection = projM;
            skyModel.Meshes[0].Draw();
        }
    }
}
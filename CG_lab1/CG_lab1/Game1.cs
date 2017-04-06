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
        public override void init()
        {
			Engine.GetInst().addEntity(HeightMap.createComponents(
					"US_Canyon",
					"US_Canyon"
		   		));

            Engine.GetInst().Window.Title = "Get to the Choppaaaaargh!";
			Engine.GetInst().Subsystems.Add(new HeightmapSystem(world));
            Engine.GetInst().Subsystems.Add(new SkyboxSystem());
            Engine.GetInst().Subsystems.Add(new CameraSystem());
            Engine.GetInst().Subsystems.Add(new ModelSystem(world));
            Engine.GetInst().Subsystems.Add(new TransformSystem());
            Engine.GetInst().Subsystems.Add(new InputSystem());

            chopper = Engine.GetInst().addEntity(Chopper.createComponents(
                new Vector3(5, 5, 5), 
                new Vector3(0, 300, -50),
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
using Manager.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Subsystems
{
    public class SkyboxSystem : Core
    {
        private GraphicsDeviceManager graphics;
        private Model skyModel;//, shipModel;
        private BasicEffect skyEffect;
        //private Matrix world, view, proj;
        private Matrix viewM, projM, skyworldM, worldM;
        private static float skyscale = 10000f;
        private float slow = skyscale / 200f;  // step width of movements
        private float rotationX, rotationY, rotationZ;
        private Vector3 nullPos = Vector3.Zero;
        public SkyboxSystem()
        {
            skyModel = Engine.GetInst().Content.Load<Model>("skybox");
            skyEffect = (BasicEffect)skyModel.Meshes[0].Effects[0];
            graphics = Engine.GetInst().graphics;
        }
        public override void update(GameTime gameTime)
        {
            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var transformComponent = entity.GetComponent<TransformComponent>();
                if (transformComponent == null)
                    continue;
                var cameraComponent = entity.GetComponent<CameraComponent>();
                var position = transformComponent.position;
                var view = transformComponent.scale;

                var scale = transformComponent.scale;
                var rotation = transformComponent.rotation;
                var objectWorld = transformComponent.objectWorld;
                var elapsedGameTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                // Left (Negative X)
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                { position.X -= transformComponent.speed.X * elapsedGameTime; view.X -= transformComponent.speed.X * elapsedGameTime; }

                // Right (Positive X)
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                { position.X += transformComponent.speed.X * elapsedGameTime; view.X += transformComponent.speed.X * elapsedGameTime; }

                // Backward (Positive Z)
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                { position.Y -= transformComponent.speed.Y * elapsedGameTime; view.Y -= transformComponent.speed.Y * elapsedGameTime; }

                // Forward (Negative Z)
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                { position.Y += transformComponent.speed.Y * elapsedGameTime; view.Y += transformComponent.speed.Y * elapsedGameTime; }

                worldM = Matrix.CreateTranslation(nullPos);
                skyworldM = Matrix.CreateScale(skyscale, skyscale, skyscale);
                viewM = Matrix.CreateTranslation(-nullPos.X, -nullPos.Y, -nullPos.Z) *
                             Matrix.CreateRotationX(rotationX) *
                             Matrix.CreateRotationY(rotationY) *
                             Matrix.CreateRotationZ(rotationZ) *
                             Matrix.CreateLookAt(position, view, Vector3.Up) * worldM;
                projM = Matrix.CreatePerspectiveFieldOfView(MathHelper.Pi / 3, 1f, 1f, 10f * skyscale);
                graphics.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            }
        }
        public override void draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.DarkBlue);
            skyEffect.World = skyworldM;
            skyEffect.View = viewM;
            skyEffect.Projection = projM;
            skyModel.Meshes[0].Draw();
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Engine.chopper
{
    public class Chopper
    {
        private Game game;

        public Model model;

        Vector3 speed = new Vector3(0.1f, 0.1f, 0.1f);

        private Vector3 scale;
        private Vector3 position;
        private Matrix rotation;
        private Matrix objectWorld;

        public Chopper(Game game, Vector3 scale, Vector3 position)
        {
            this.game = game;
            this.scale = scale;
            this.position = position;

            objectWorld = Matrix.Identity;
            rotation = Matrix.Identity;
        }

        public void LoadContent()
        {
            model = game.Content.Load<Model>("Chopper");
        }

        public void Update(GameTime gameTime)
        {
            float elapsedGameTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // -----
            // Scale
            // -----

            // Enlarge (uniformly)
            if (Keyboard.GetState().IsKeyDown(Keys.Add))
                scale *= 1.1f;

            // Shrink (uniformly)
            if (Keyboard.GetState().IsKeyDown(Keys.Subtract))
                scale *= 0.9f;

            // --------
            // Tanslate
            // --------

            // Left (Negative X)
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                position.X -= speed.X * elapsedGameTime;

            // Right (Positive X)
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                position.X += speed.X * elapsedGameTime;

            // Backward (Positive Z)
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                position.Z += speed.Z * elapsedGameTime;

            // Forward (Negative Z)
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                position.Z -= speed.Z * elapsedGameTime;

            // Up (Positive Y)
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                position.Y += speed.Y * elapsedGameTime;

            // Down (Negative Y)
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                position.Y -= speed.Y * elapsedGameTime;

            // ------
            // Rotate
            // ------

            Vector3 axis = new Vector3(0, 0, 0);
            float angle = -elapsedGameTime * 0.01f;

            // Clockwise around positive Y-axis
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                axis = new Vector3(0, 1f, 0);

            // Clockwise around negative Y-axis
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                axis = new Vector3(0, -1f, 0);

            // Clockwise around positive X-axis
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                axis = new Vector3(1f, 0, 0);

            // Clockwise around negative X-axis
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                axis = new Vector3(-1f, 0, 0);

            // Clockwise around positive Z-axis
            if (Keyboard.GetState().IsKeyDown(Keys.C))
                axis = new Vector3(0, 0, 1f);

            // Clockwise around negative Z-axis
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
                axis = new Vector3(0, 0, -1f);

            Quaternion rot = Quaternion.CreateFromAxisAngle(axis, angle);
            rot.Normalize();
            rotation *= Matrix.CreateFromQuaternion(rot);

            // Reset to original (zero) rotation
            if (Keyboard.GetState().IsKeyDown(Keys.R))
                rotation = Matrix.Identity;
        }

        public void Draw(GameTime gameTime, Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh modelMesh in model.Meshes)
            {
                //System.Console.WriteLine(modelMesh.Name);
                foreach (BasicEffect effect in modelMesh.Effects)
                {
                    objectWorld = Matrix.CreateScale(scale) * rotation * Matrix.CreateTranslation(position);
                    effect.World = modelMesh.ParentBone.Transform * objectWorld * world;
                    effect.View = view;
                    effect.Projection = projection;

                    effect.EnableDefaultLighting();
                    effect.LightingEnabled = true;

                    foreach (EffectPass p in effect.CurrentTechnique.Passes)
                    {
                        p.Apply();
                        modelMesh.Draw();
                    }
                }
            }
        }
    }
}

using Manager.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Subsystems
{
    public class InputSystem : Core
    {
        public override void update(GameTime gameTime)
        {
            float elapsedGameTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var tC = entity.GetComponent<TransformComponent>();
                if (tC == null)
                    continue;
                var userInput = entity.GetComponent<InputComponent>();
                // -----
                // Scale
                // -----

                // Enlarge (uniformly)
                if (Keyboard.GetState().IsKeyDown(userInput.add))
                    tC.scale *= 1.1f;

                // Shrink (uniformly)
                if (Keyboard.GetState().IsKeyDown(userInput.sub))
                    tC.scale *= 0.9f;

                // --------
                // Tanslate
                // --------

                // Left (Negative X)
                if (Keyboard.GetState().IsKeyDown(userInput.a))
                    tC.position.X -= tC.speed.X * elapsedGameTime;

                // Right (Positive X)
                if (Keyboard.GetState().IsKeyDown(userInput.d))
                    tC.position.X += tC.speed.X * elapsedGameTime;

                // Backward (Positive Z)
                if (Keyboard.GetState().IsKeyDown(userInput.w))
                    tC.position.Z += tC.speed.Z * elapsedGameTime;

                // Forward (Negative Z)
                if (Keyboard.GetState().IsKeyDown(userInput.s))
                    tC.position.Z -= tC.speed.Z * elapsedGameTime;

                // Up (Positive Y)
                if (Keyboard.GetState().IsKeyDown(userInput.space))
                    tC.position.Y += tC.speed.Y * elapsedGameTime;

                // Down (Negative Y)
                if (Keyboard.GetState().IsKeyDown(userInput.lShift))
                    tC.position.Y -= tC.speed.Y * elapsedGameTime;

                // ------
                // Rotate
                // ------

                Vector3 axis = new Vector3(0, 0, 0);
                float angle = -elapsedGameTime * 0.01f;

                // Clockwise around positive Y-axis
                if (Keyboard.GetState().IsKeyDown(userInput.left))
                    axis = new Vector3(0, 1f, 0);

                // Clockwise around negative Y-axis
                if (Keyboard.GetState().IsKeyDown(userInput.right))
                    axis = new Vector3(0, -1f, 0);

                // Clockwise around positive X-axis
                if (Keyboard.GetState().IsKeyDown(userInput.up))
                    axis = new Vector3(1f, 0, 0);

                // Clockwise around negative X-axis
                if (Keyboard.GetState().IsKeyDown(userInput.down))
                    axis = new Vector3(-1f, 0, 0);

                // Clockwise around positive Z-axis
                if (Keyboard.GetState().IsKeyDown(userInput.c))
                    axis = new Vector3(0, 0, 1f);

                // Clockwise around negative Z-axis
                if (Keyboard.GetState().IsKeyDown(userInput.z))
                    axis = new Vector3(0, 0, -1f);

                Quaternion rot = Quaternion.CreateFromAxisAngle(axis, angle);
                rot.Normalize();
                tC.rotation *= Matrix.CreateFromQuaternion(rot);

                // Reset to original (zero) rotation
                if (Keyboard.GetState().IsKeyDown(userInput.r))
                    tC.rotation = Matrix.Identity;
            }
        }
    }
}

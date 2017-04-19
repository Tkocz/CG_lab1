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

                if (Keyboard.GetState().IsKeyDown(userInput.add))
                    tC.scale *= 1.1f; // should be changed to distance from camera

                if (Keyboard.GetState().IsKeyDown(userInput.sub))
                    tC.scale *= 0.9f; // should be changed to distance from camera

                if (Keyboard.GetState().IsKeyDown(userInput.a))
					tC.position += tC.speed.X * elapsedGameTime * tC.objectWorld.Left;

				if (Keyboard.GetState().IsKeyDown(userInput.d))
					tC.position += tC.speed.X * elapsedGameTime * tC.objectWorld.Right;

                if (Keyboard.GetState().IsKeyDown(userInput.w))
					tC.position += tC.speed.Z * elapsedGameTime * tC.objectWorld.Forward;

                if (Keyboard.GetState().IsKeyDown(userInput.s))
                    tC.position += tC.speed.Z * elapsedGameTime * tC.objectWorld.Backward;

                if (Keyboard.GetState().IsKeyDown(userInput.space))
                    tC.position += tC.speed.Y * elapsedGameTime * tC.objectWorld.Up;

                if (Keyboard.GetState().IsKeyDown(userInput.lShift))
                    tC.position += tC.speed.Y * elapsedGameTime * tC.objectWorld.Down;


                Vector3 axis = new Vector3(0, 0, 0);
                float angle = -elapsedGameTime * 0.001f;
                tC.orientation = Quaternion.Identity;
                if (Keyboard.GetState().IsKeyDown(userInput.left))
                {
                    //axis = new Vector3(0, -1f, 0);
                    tC.prevyaw = tC.yaw;
                    tC.yaw += 0.1f;
                }

                if (Keyboard.GetState().IsKeyDown(userInput.right))
                {
                    //axis = new Vector3(0, 1f, 0);
                    tC.prevyaw = tC.yaw;
                    tC.yaw -= 0.1f;
                }

                if (Keyboard.GetState().IsKeyDown(userInput.up))
                {
                    //axis = new Vector3(1f, 0, 0);
                    tC.prevpitch = tC.pitch;
                    tC.pitch += 0.1f;
                }


                if (Keyboard.GetState().IsKeyDown(userInput.down))
                {
                    tC.prevpitch = tC.pitch;
                    tC.pitch -= 0.1f;
                }

                if (Keyboard.GetState().IsKeyDown(userInput.q))
                {
                    tC.prevroll = tC.roll;
                    tC.roll += 0.1f;
                }

                if (Keyboard.GetState().IsKeyDown(userInput.e))
                {
                    tC.prevroll = tC.roll;
                    tC.roll -= 0.1f;
                }

                //Quaternion rot = Quaternion.CreateFromAxisAngle(axis, angle);
                Matrix.CreateFromAxisAngle(Vector3.Up, tC.pitch);

                var additionalRoation = Quaternion.CreateFromAxisAngle(Vector3.Up, tC.yaw) * Quaternion.CreateFromAxisAngle(Vector3.Right, tC.pitch) * Quaternion.CreateFromAxisAngle(Vector3.Forward, tC.roll);
                additionalRoation.Normalize();
                //rot.Normalize();
                tC.orientation *= additionalRoation;

                // Reset to original (zero) rotation
                if (Keyboard.GetState().IsKeyDown(userInput.r))
                    tC.orientation = Quaternion.Identity;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Manager;
using Manager.Components;

namespace Manager.Subsystems
{
    public class CameraSystem : Core
    {
        //Computes the view and projection matrix for all the CameraComponents.
        public override void update(GameTime gameTime)
        {
            CameraComponent camera = null;
            TransformComponent transform = null;
            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var cameraModel = entity.GetComponent<CameraComponent>();
                if (cameraModel != null)
                {
                    camera = cameraModel;
                    transform = entity.GetComponent<TransformComponent>();
                }
            }
            Quaternion cameraRotation = Quaternion.Identity;
                Quaternion objectRotation = Quaternion.Identity;
                cameraRotation = Quaternion.Lerp(cameraRotation, objectRotation, 0.1f);

            Vector3 campos = new Vector3(0, 0, 60f);
            //campos = Vector3.Transform(campos, Matrix.CreateFromQuaternion(cameraRotation));
            campos = Vector3.Transform(campos, transform.rotation);
            campos += transform.position;

            Vector3 camup = new Vector3(0, 1, 0);
            camup = Vector3.Transform(camup, Matrix.CreateFromQuaternion(cameraRotation));
            camera.view = Matrix.CreateLookAt(campos, transform.position, camup);
            camera.projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Engine.GetInst().GraphicsDevice.Viewport.AspectRatio, 0.2f, 500.0f);

            //transform.position = campos;
            camera.up = camup;
        }
    }
}

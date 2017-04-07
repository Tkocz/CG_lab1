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

			Vector3 transformedReference = Vector3.Transform(camera.offset, transform.rotation);
			Vector3 cameraPosition = transformedReference + transform.position;

            camera.view = Matrix.CreateLookAt(cameraPosition, transform.position, Vector3.Up);
   
            //transform.position = campos;
            camera.up = Vector3.Transform(Vector3.Up, Matrix.CreateFromQuaternion(cameraRotation));;
        }
    }
}

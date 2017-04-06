using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Manager.Core;

namespace Manager.Components
{
    public class CameraComponent : Component
    {
        public Matrix view;
        public Matrix projection;
        public CameraComponent()
        {
            view = Matrix.CreateLookAt(new Vector3(60, 500, -100), new Vector3(0, 0, 0), Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, Engine.GetInst().GraphicsDevice.Viewport.AspectRatio, 0.1f, 1000f);
        }
    }
}

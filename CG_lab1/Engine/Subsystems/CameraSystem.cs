using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Engine;

namespace Engine.Subsystems
{
    public class CameraSystem : Core
    {
        //Computes the view and projection matrix for all the CameraComponents.
        public override void update(GameTime gameTime, Matrix world, Matrix view, Matrix projection)
        {
        }
    }
}

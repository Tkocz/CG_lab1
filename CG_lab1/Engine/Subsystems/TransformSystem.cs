using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Engine.Subsystems
{
    public class TransformSystem : Core
    {
        //Computes the transformation matrices (world-matrices) for all TransformComponents.
        public override void update(GameTime gameTime, Matrix world, Matrix view, Matrix projection)
        {
        }
    }
}

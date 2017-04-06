using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Manager.Core;

namespace Manager.Components
{
    public class InputComponent : Component
    {
        public Keys add, sub, a, d, w, s, space, lShift, left, right, up, down, c, z, r;

        public InputComponent()
        {
            add = Keys.Add;             // Enlarge (uniformly)
            sub = Keys.Subtract;        // Ensmallen (uniformly)

            // --------
            // Translate
            // --------
            a = Keys.A;                 // Left (Negative X) 
            d = Keys.D;                 // Right (Positive X)                
            w = Keys.W;                 // Backward (Positive Z)                
            s = Keys.S;                 // Forward (Negative Z)
            space = Keys.Space;         // Up (Positive Y)
            lShift = Keys.LeftShift;    // Down (Negative Y)

            // ------
            // Rotate
            // ------
            left = Keys.Left;           // Clockwise around positive Y-axis                
            right = Keys.Right;         // Clockwise around negative Y-axis                
            up = Keys.Up;               // Clockwise around positive X-axis                
            down = Keys.Down;           // Clockwise around negative X-axis                
            c = Keys.C;                 // Clockwise around positive Z-axis                
            z = Keys.Z;                 // Clockwise around negative Z-axis 
            r = Keys.R;                 // Reset to original (zero) rotation
        }
    }
}

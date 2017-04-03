using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Engine.Core;

namespace Engine.Components
{
    public class ModelComponent : Component
    {
        //Holds a model and the data transforms for its meshes
        public Model model;
        public ModelComponent(string modelName)
        {
            model = Engine.GetInst().Content.Load<Model>(modelName);
        }
    }
}

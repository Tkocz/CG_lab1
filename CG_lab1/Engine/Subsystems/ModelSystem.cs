using Engine;
using Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Engine.Subsystems
{
    public class ModelSystem : Core
    {
        //Renders models and applies the correct transforms to the models’ submeshes.
        public override void draw(GameTime gameTime, Matrix world, Matrix view, Matrix projection)
        {
            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var modelComponent = entity.GetComponent<ModelComponent>();
                foreach (ModelMesh modelMesh in modelComponent.model.Meshes)
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
}

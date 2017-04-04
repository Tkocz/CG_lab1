using Manager;
using Manager.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Manager.Subsystems
{
    public class ModelSystem : Core
    {
        private Matrix world;

        public ModelSystem(Matrix world)
        {
            this.world = world;
        }

        //Renders models and applies the correct transforms to the models’ submeshes.
        public override void draw(GameTime gameTime)
        {
            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var modelComponent = entity.GetComponent<ModelComponent>();
                var transformComponent = entity.GetComponent<TransformComponent>();
                var cameraModel = entity.GetComponent<CameraComponent>();
                var scale = transformComponent.scale;
                var rotation = transformComponent.rotation;
                var objectWorld = transformComponent.objectWorld;


                foreach (ModelMesh modelMesh in modelComponent.model.Meshes)
                {
                    //System.Console.WriteLine(modelMesh.Name);
                    foreach (BasicEffect effect in modelMesh.Effects)
                    {
                        objectWorld = Matrix.CreateScale(scale) * rotation * Matrix.CreateTranslation(transformComponent.position);
                        effect.World = modelMesh.ParentBone.Transform * objectWorld * world;
                        effect.View = cameraModel.view;
                        effect.Projection = cameraModel.projection;

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

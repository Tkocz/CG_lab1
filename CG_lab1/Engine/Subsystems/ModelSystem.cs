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
        private static float skyscale = 10000f;
        private Matrix skyworldM, projM;

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
                if (modelComponent == null)
                    continue;
                var transformComponent = entity.GetComponent<TransformComponent>();
                var cameraComponent = entity.GetComponent<CameraComponent>();


                if (cameraComponent != null)
                {
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
                            effect.View = cameraComponent.view;
                            effect.Projection = cameraComponent.projection;

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
                else
                {
                    skyworldM = Matrix.CreateScale(skyscale, skyscale, skyscale);
                    projM = Matrix.CreatePerspectiveFieldOfView(MathHelper.Pi / 3, 1f, 1f, 10f * skyscale);
                    modelComponent.modelEffect.World = skyworldM;
                    modelComponent.modelEffect.View = Matrix.CreateLookAt(new Vector3(0, 0, 20), new Vector3(0, 0, 0), Vector3.Up);
                    modelComponent.modelEffect.Projection = projM;
                    modelComponent.model.Meshes[0].Draw();
                }
            }
        }
        public override void update(GameTime gameTime)
        {
            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var modelComponent = entity.GetComponent<ModelComponent>();
                if (!modelComponent.hasTransformable)
                    continue;
                var transformComponent = entity.GetComponent<TransformComponent>();
                var cameraComponent = entity.GetComponent<CameraComponent>();
                var scale = transformComponent.scale;
                var rotation = transformComponent.rotation;
                var objectWorld = transformComponent.objectWorld;
                var elapsedGameTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                foreach (ModelBone modelBone in modelComponent.model.Bones)
                {
                    if(modelBone.Name == "Main_Rotor")
                    {
                        Matrix MainRotorWorldMatrix;
                        MainRotorWorldMatrix = modelBone.Transform;
                        MainRotorWorldMatrix *= Matrix.CreateTranslation(-modelBone.Transform.Translation); 
                        MainRotorWorldMatrix *= Matrix.CreateRotationY(elapsedGameTime * 0.01f);
                        MainRotorWorldMatrix *= Matrix.CreateTranslation(modelBone.Transform.Translation);  
                        modelBone.Transform = MainRotorWorldMatrix;
                    }
                    if (modelBone.Name == "Back_Rotor")
                    {
                        Matrix BackRotorWorldMatrix;
                        BackRotorWorldMatrix = modelBone.Transform;
                        BackRotorWorldMatrix = modelBone.Transform;
                        BackRotorWorldMatrix *= Matrix.CreateTranslation(-modelBone.Transform.Translation);
                        BackRotorWorldMatrix *= Matrix.CreateRotationX(elapsedGameTime * 0.01f);
                        BackRotorWorldMatrix *= Matrix.CreateTranslation(modelBone.Transform.Translation);        
                        modelBone.Transform = BackRotorWorldMatrix;
                    }
                }
            }
        }
    }
}

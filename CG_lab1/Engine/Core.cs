using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Engine
{
    public abstract class Core
    {
        public virtual void update(GameTime gameTime, Matrix world, Matrix view, Matrix projection) { }
        public virtual void draw(GameTime gameTime, Matrix world, Matrix view, Matrix projection) { }
        public abstract class Component { }
        public class Entity
        {
            private readonly Dictionary<Type, Component> components = new Dictionary<Type, Component>();
            public int id;
            public void AddComponents(Component[] components)
            {
                foreach (var component in components)
                {
                    this.components.Add(component.GetType(), component);
                }
            }
            public T GetComponent<T>() where T : Component
            {
                Component component;
                components.TryGetValue(typeof(T), out component);
                return (T)component;
            }

        }
        public abstract class GameImpl
        {
            public abstract void init();
            public abstract void update(GameTime gameTime, Matrix world, Matrix view, Matrix projection);

        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using static Manager.Core;


namespace Manager
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Engine : Game
    {
        private static Engine inst;
        private GameImpl gameImpl;
        public GraphicsDeviceManager graphics;
        public readonly List<Core> Subsystems = new List<Core>();
        public readonly Dictionary<int, Entity> Entities = new Dictionary<int, Entity>();
        private int entityId = 1;

        public Entity addEntity(Component[] components)
        {
            var entity = new Entity();
            entity.id = entityId++;
            entity.AddComponents(components);
            Entities[entity.id] = entity;
            return entity;
        }
        public Engine(GameImpl gameImpl)
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 2000;
            graphics.PreferredBackBufferHeight = 2000;
            Content.RootDirectory = "Content";
            this.gameImpl = gameImpl;
        }
        public static void RunGame(GameImpl gameImpl)
        {
            inst = new Engine(gameImpl);
            inst.Run();
        }
        public static Engine GetInst()
        {
            return inst;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameImpl.init();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            //spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            foreach (var subsystem in Subsystems)
            {
                subsystem.update(gameTime);
            }
            gameImpl.update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            foreach (var subsystem in Subsystems)
            {
                subsystem.draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }
}
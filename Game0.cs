using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject0
{
    public class Game0 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        private SnakeSprite snake;
        private SnakeSprite[] snakes;
        private ChickenSprite chicken;
        private EggSprite[] eggs;
        private InputManager inputManager;

        public Game0()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            chicken = new ChickenSprite();
            //snake = new SnakeSprite() { Position = new Vector2(400, 400), snakeDirection = SnakeDirection.Left };
            snakes = new SnakeSprite[]
            {
                new SnakeSprite(){ Position = new Vector2(400, 350), snakeDirection = SnakeDirection.Right },
                new SnakeSprite(){ Position = new Vector2(300, 100), snakeDirection = SnakeDirection.Right },
                new SnakeSprite(){ Position = new Vector2(200, 250), snakeDirection = SnakeDirection.Left },
                new SnakeSprite(){ Position = new Vector2(350, 225), snakeDirection = SnakeDirection.Left }
            };

            eggs = new EggSprite[]
            {
                new EggSprite(new Vector2(400, 400))
            };


            inputManager = new InputManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
           
            // TODO: use this.Content to load your game content here
            
            //snake.LoadContent(Content);
            chicken.LoadContent(Content);
            foreach (var snake in snakes)
            {
                snake.LoadContent(Content);
            }
            foreach(var egg in eggs)
            {
                egg.LoadContent(Content);
            }


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            chicken.Update(gameTime);
            //snake.Update(gameTime);
            foreach(var snake in snakes)
            {
                snake.Update(gameTime);
            }

            foreach(var egg in eggs)
            {
                if(!egg.Collected && egg.Bounds.CollidesWith(chicken.Bounds))
                {
                    chicken.color = Color.Red;
                    egg.Collected = true;
                }
            }
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            chicken.Draw(gameTime, spriteBatch);
            //snake.Draw(gameTime, spriteBatch);
            foreach(var snake in snakes)
            {
                snake.Draw(gameTime, spriteBatch);
            }
            foreach(var egg in eggs)
            {
                egg.Draw(gameTime, spriteBatch);
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

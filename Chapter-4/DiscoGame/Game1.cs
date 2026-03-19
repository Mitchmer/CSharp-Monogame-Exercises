using System;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DiscoGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private const int ColorUpdateInterval = 500; // how many milliseconds must pass before the color can change
    private int Elapsed = 0; // running sum of the milliseconds passed since the last background color update
    private bool CanChangeColor = false; // flag to indicate whether to change the color of the background or not
    private Color BackgroundColor = Color.AliceBlue;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Elapsed += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

        /// If more than 500 milliseconds has passed since the last color change
        if (Elapsed >= ColorUpdateInterval) {
            CanChangeColor = true;
            BackgroundColor = new Color(RandomNumberGenerator.GetInt32(256),
                                        RandomNumberGenerator.GetInt32(256),
                                        RandomNumberGenerator.GetInt32(256));
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if (CanChangeColor) {
            GraphicsDevice.Clear(BackgroundColor);
            Elapsed = 0;
            CanChangeColor = false;
        }
        base.Draw(gameTime);
    }
}

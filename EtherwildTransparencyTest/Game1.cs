using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OverlayWindow;

namespace EtherwildTransparencyTest;

public class Game1 : OverlayGameSelf
{
  private SpriteBatch _spriteBatch;
  private Texture2D _pixelTexture;

  public Game1()
  {
    Content.RootDirectory = "Content";
  }

  protected override void Initialize()
  {
    // TODO: Add your initialization logic here

    base.Initialize();
  }

  protected override void LoadContent()
  {
    _spriteBatch = new SpriteBatch(GraphicsDevice);

    // Load a simple 1x1 white pixel texture
    _pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
    _pixelTexture.SetData([Color.White]);
    // TODO: use this.Content to load your game content here
  }

  protected override void Update(GameTime gameTime)
  {
    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
      Exit();

    // TODO: Add your update logic here

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime)
  {
    GraphicsDevice.Clear(Color.Transparent);

    // TODO: Add your drawing code here
    _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);

    Vector2 pointR = new Vector2(600, 600);
    var size = GetVirtualScreenAreaSize();
    for (int x = 0; x < size.Width; x++)
    {
      for (int y = 0; y < size.Height; y++)
      {
        var pos = new Vector2(x, y);
        var r = 1-Vector2.Distance(pos, pointR) / 300;
        if(r > 0)
          _spriteBatch.Draw(_pixelTexture, new Rectangle(x, y, 1, 1), new Color(1,0,0,r));
      }
    }
    _spriteBatch.End();

    base.Draw(gameTime);
  }
}

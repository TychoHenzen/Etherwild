using Etherwild.Boilerplate;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;

namespace Etherwild;
public class EtherwildGameWindow : OverlayGameSelf
{
    private SpriteBatch _spriteBatch;
    private MapRenderer _mapRenderer;
    private Player _player;
    private Rectangle _customViewport;
    private Matrix _scaleMatrix;

    public EtherwildGameWindow()
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
        _mapRenderer = new MapRenderer(GraphicsDevice, Content);
        _mapRenderer.LoadContent("Assets/NatureMap");
        _player = new Player(_mapRenderer.Map, 0, 6);

        // Load a simple 1x1 white pixel texture
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _player.Update(gameTime);
        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Transparent);
        
        // TODO: Add your drawing code here
        _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);
        
        _mapRenderer.Draw();
        _player.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

using EtherwildTransparencyTest.Boilerplate;
using EtherwildTransparencyTest.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OverlayWindow;
using KeyboardInput = EtherwildTransparencyTest.Core.KeyboardInput;

namespace EtherwildTransparencyTest.App;

public sealed class EtherwildGameWindow : Game
{
    protected GraphicsDeviceManager Graphics;
    private SpriteBatch _spriteBatch;
    private EtherwildGame _etherwildGame;

    public EtherwildGameWindow()
    {
        Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(Graphics.GraphicsDevice);
        var mapRenderer = new MapRenderer(new TiledMapLoader(Content));
        mapRenderer.LoadContent(GraphicsDevice,"Assets/NatureMap");

        var player = new Player(mapRenderer.Map, 0, 6);
        var inputHandler = new KeyboardInput();
        var playerMovement = new PlayerMovement(player, inputHandler, 150);

        _etherwildGame = new EtherwildGame(mapRenderer, playerMovement, inputHandler, player);
    }

    protected override void Update(GameTime gameTime)
    {
        _etherwildGame.Update(gameTime, this);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkRed);

        _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp);
        _etherwildGame.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}


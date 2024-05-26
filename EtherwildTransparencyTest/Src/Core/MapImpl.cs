using System.Runtime.InteropServices;
using EtherwildTransparencyTest.Boilerplate;
using EtherwildTransparencyTest.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace EtherwildTransparencyTest.Core;

public class TiledMapLoader : IMapLoader
{
    private readonly ContentManager _content;

    public TiledMapLoader(ContentManager content)
    {
        _content = content;
    }

    public TiledMap LoadMap(string mapAssetName)
    {
        return _content.Load<TiledMap>(mapAssetName);
    }
}

public class TiledMapRendererImpl : IMapRenderer
{
    private TiledMapRenderer _mapRenderer;

    public void Initialize(GraphicsDevice graphicsDevice,TiledMap map)
    {
        _mapRenderer = new TiledMapRenderer(graphicsDevice, map);
    }

    public void LoadContent(GraphicsDevice graphicsDevice, string mapName)
    {
        throw new System.NotImplementedException();
    }

    public void Draw(SpriteBatch spriteBatch, Matrix scaleMatrix)
    {
        _mapRenderer.Draw(scaleMatrix);
    }

}

public class DisplayScaler : IDisplayScaler
{
    public Matrix GetScaleMatrix()
    {
        float displayScale = GetWindowsDisplayScale();
        float finalScale = 3f / displayScale;
        return Matrix.CreateScale(finalScale, finalScale, 1f);
    }

    private float GetWindowsDisplayScale()
    {
        int scaledWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int scaledHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        var devMode = new Devmode();
        EnumDisplaySettings(null, ENUM_CURRENT_SETTINGS, ref devMode);
        uint nativeWidth = devMode.dmPelsWidth;
        uint nativeHeight = devMode.dmPelsHeight;

        float scaleX = (float)nativeWidth / scaledWidth;
        float scaleY = (float)nativeHeight / scaledHeight;

        return (scaleX + scaleY) / 2;
    }

    [DllImport("user32.dll")]
    private static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref Devmode devMode);

    private const int ENUM_CURRENT_SETTINGS = -1;
}
public class KeyboardInput : IPlayerInput, IInputHandler
{
    public Vector2 GetMovementDirection()
    {
        Vector2 movement = Vector2.Zero;

        if (Keyboard.GetState().IsKeyDown(Keys.W))
            movement.Y -= 1;
        if (Keyboard.GetState().IsKeyDown(Keys.S))
            movement.Y += 1;
        if (Keyboard.GetState().IsKeyDown(Keys.A))
            movement.X -= 1;
        if (Keyboard.GetState().IsKeyDown(Keys.D))
            movement.X += 1;

        return movement;
    }

    public bool IsExitKeyPressed()
    {
        return GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
               Keyboard.GetState().IsKeyDown(Keys.Escape);
    }
}
public class PlayerMovement : IPlayerMovement
{
    private readonly Player _player;
    private readonly IPlayerInput _input;
    private readonly float _speed;

    public PlayerMovement(Player player, IPlayerInput input, float speed)
    {
        _player = player;
        _input = input;
        _speed = speed;
    }

    public void Update(GameTime gameTime)
    {
        Vector2 movement = _input.GetMovementDirection();
        movement *= _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        _player.Position += movement;
    }
}

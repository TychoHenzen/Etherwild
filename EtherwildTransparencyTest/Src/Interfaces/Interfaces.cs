using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;

namespace EtherwildTransparencyTest.Interfaces;

public interface IMapLoader
{
    TiledMap LoadMap(string mapAssetName);
}

public interface IMapRenderer
{
    void Initialize(GraphicsDevice graphicsDevice, TiledMap map);
    void LoadContent(GraphicsDevice graphicsDevice,string mapName);
    void Draw(SpriteBatch spriteBatch,Matrix scaleMatrix);
}

public interface IDisplayScaler
{
    Matrix GetScaleMatrix();
}
public interface IPlayerMovement
{
    void Update(GameTime gameTime);
}
public interface IPlayerInput
{
    Vector2 GetMovementDirection();
}

public interface IInputHandler
{
    bool IsExitKeyPressed();
    // Add other input related methods here
}

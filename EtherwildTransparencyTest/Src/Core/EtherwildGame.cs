using EtherwildTransparencyTest.Boilerplate;
using EtherwildTransparencyTest.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EtherwildTransparencyTest.Core;

public class EtherwildGame
{
    private readonly IMapRenderer _mapRenderer;
    private readonly IPlayerMovement _playerMovement;
    private readonly IInputHandler _inputHandler;
    private readonly Player _player;

    public EtherwildGame(IMapRenderer mapRenderer, IPlayerMovement playerMovement, IInputHandler inputHandler, Player player)
    {
        _mapRenderer = mapRenderer;
        _playerMovement = playerMovement;
        _inputHandler = inputHandler;
        _player = player;
    }

    public void LoadContent(GraphicsDevice graphicsDevice,string mapName)
    {
        _mapRenderer.LoadContent(graphicsDevice,mapName);
    }

    public void Update(GameTime gameTime)
    {
        if (_inputHandler.IsExitKeyPressed())
            Exit();  // Handle exit in your game loop

        _playerMovement.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _mapRenderer.Draw(spriteBatch, Matrix.Identity);
        _player.Draw(spriteBatch);
    }

    private void Exit()
    {
        // Handle exit logic
    }
}

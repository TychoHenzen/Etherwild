using EtherwildTransparencyTest.App;
using EtherwildTransparencyTest.Boilerplate;
using EtherwildTransparencyTest.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EtherwildTransparencyTest.Core;

public sealed class EtherwildGame(
    IMapRenderer mapRenderer,
    IPlayerMovement playerMovement,
    IInputHandler inputHandler,
    Player player)
{
    public void Update(GameTime gameTime, Game window)
    {
        if (inputHandler.IsExitKeyPressed())
            window.Exit();

        playerMovement.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        mapRenderer.Draw(spriteBatch, Matrix.Identity);
        player.Draw(spriteBatch);
    }
}

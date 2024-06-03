using EtherwildTransparencyTest.App;
using EtherwildTransparencyTest.Boilerplate;
using EtherwildTransparencyTest.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = EtherwildTransparencyTest.Interfaces.IDrawable;

namespace EtherwildTransparencyTest.Core;

public sealed class EtherwildGame(
    IDrawable mapRenderer,
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
        mapRenderer.Draw(spriteBatch);
        player.Draw(spriteBatch);
    }
}

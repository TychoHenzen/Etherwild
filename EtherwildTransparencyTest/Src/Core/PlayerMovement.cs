using EtherwildTransparencyTest.App;
using EtherwildTransparencyTest.Boilerplate;
using EtherwildTransparencyTest.Interfaces;
using Microsoft.Xna.Framework;

namespace EtherwildTransparencyTest.Core;

public sealed class PlayerMovement(Player player, IPlayerInput input, float speed) : IPlayerMovement
{
    public void Update(GameTime gameTime)
    {
        var movement = input.GetMovementDirection();
        movement *= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        player.Position += movement;
    }
}

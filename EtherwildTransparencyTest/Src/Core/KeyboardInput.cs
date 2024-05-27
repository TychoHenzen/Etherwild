using EtherwildTransparencyTest.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EtherwildTransparencyTest.Core;

public sealed class KeyboardInput : IPlayerInput, IInputHandler
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

  public bool IsExitKeyPressed() =>
    Keyboard.GetState().IsKeyDown(Keys.Escape);
}

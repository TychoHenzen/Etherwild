using System.Reflection.Metadata;
using EtherwildTransparencyTest.App;
using EtherwildTransparencyTest.Core;
using EtherwildTransparencyTest.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EtherwildTransparencyTest.Events;

public class StartDrawEvent : NotifyEveryoneEvent<SpriteBatch, VoidT>
{
  public StartDrawEvent(string name) : base(name)
  {
  }
}

public class DrawEvent : NotifyEveryoneEvent<SpriteBatch, VoidT>
{
  public DrawEvent(string name) : base(name)
  {
  }
}

public class EndDrawEvent : NotifyEveryoneEvent<SpriteBatch, VoidT>
{
  public EndDrawEvent(string name) : base(name)
  {
  }
}


public class SwitchSceneEvent : NotifyEveryoneEvent<IScene, VoidT>
{
  public SwitchSceneEvent(string name) : base(name)
  {
  }
}


public class LoadAssetsEvent : NotifyEveryoneEvent<SpriteBatch, VoidT>
{
  public LoadAssetsEvent(string name) : base(name)
  {
  }
}

public class UpdateEvent : NotifyEveryoneEvent<GameTime, VoidT>
{
  public UpdateEvent(string name) : base(name)
  {
  }
}

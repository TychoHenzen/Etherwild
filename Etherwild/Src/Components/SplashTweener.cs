using System;
using Etherwild.Controllers;
using EtherwildTransparencyTest.App;
using EtherwildTransparencyTest.Controllers;
using EtherwildTransparencyTest.Core;
using EtherwildTransparencyTest.Entities;
using EtherwildTransparencyTest.Events;
using EtherwildTransparencyTest.Scenes;
using Microsoft.Xna.Framework;

namespace Etherwild.Components;

public class SplashTweener : BaseComponent
{
  private Action? _loadTest;
  private ulong _updateEvent;
  public SplashTweener(Entity owner)
    : base(owner)
  {
    Console.WriteLine("Creating tweener for " + Owner.Name);
  }

  public override void RegisterEvents(EventController events)
  {
    Console.WriteLine("Registering events for " + Owner.Name);
    _updateEvent = events.Listen<UpdateEvent, GameTime, VoidT>().Register(OnUpdate);
    _loadTest = () => events.Publish<SwitchSceneEvent, IScene, VoidT>().RegisterParameterProvider(() => new SplashScreen());
  }

  private VoidT OnUpdate(GameTime t)
  {
    if (t.TotalGameTime.TotalMilliseconds % 10_000 > 9900)
    {
      _loadTest?.Invoke();
    }

    return new VoidT();
  }

  public override void ClearEvents(EventController events)
  {
    Console.WriteLine("Clearing events for " + Owner.Name);
    events.Listen<UpdateEvent, GameTime, VoidT>().Remove(_updateEvent);
  }
}

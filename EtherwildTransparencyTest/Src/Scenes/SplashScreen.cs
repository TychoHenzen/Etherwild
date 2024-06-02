using System;
using EtherwildTransparencyTest.Controllers;

namespace EtherwildTransparencyTest.Scenes;

public class SplashScreen : IScene
{
    public void RegisterEvents(EventController events)
    {
        Console.Write("Loaded Splash screen");
    }

    public void ClearEvents(EventController events)
    {
        throw new System.NotImplementedException();
    }
}
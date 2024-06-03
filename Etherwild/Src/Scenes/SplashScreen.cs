using System;
using System.Collections.Generic;
using Etherwild.Components;
using Etherwild.Controllers;
using EtherwildTransparencyTest.Controllers;
using EtherwildTransparencyTest.Entities;

namespace EtherwildTransparencyTest.Scenes;

public class SplashScreen : IScene
{
    private List<Entity> entities = [];
    public void RegisterEvents(EventController events)
    {
        foreach (var entity in entities)
        {
            entity.RegisterEvents(events);
        }
    }
    public void ClearEvents(EventController events)
    {
        foreach (var entity in entities)
        {
            entity.ClearEvents(events);
        }
    }

    public void InstantiateObjects(EntityController ctrl)
    {
        var splash = ctrl.CreateEntity("SplashImage");
        splash.AddComponent<SplashTweener>();
        entities.Add(splash);
    }

    public void RemoveObjects(EntityController ctrl)
    {
        foreach (var entity in entities)
        {
            ctrl.DestroyEntity(entity);
        }
    }
}
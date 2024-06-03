using System;
using Etherwild.Controllers;
using EtherwildTransparencyTest.Controllers;
using EtherwildTransparencyTest.Core;
using EtherwildTransparencyTest.Events;
using EtherwildTransparencyTest.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Etherwild.App;

public sealed class EtherwildGameWindow : Game
{
    private GraphicsDeviceManager Graphics;
    private SpriteBatch? _spriteBatch;
    private EntityController _entities;
    private EventController _events;
    private IScene activeScene;
    private GameTime _time;

    public EtherwildGameWindow()
    {
        Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        _entities = new EntityController();
        _events = new EventController();
        _events.Listen<SwitchSceneEvent,IScene, VoidT>().Register(OnSwitchScene);
        
        _events.Listen<StartDrawEvent, SpriteBatch, VoidT>().Register(batch =>
        {
            GraphicsDevice.Clear(Color.DarkRed);
            batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp);
            return new VoidT();
        });
        _events.Listen<EndDrawEvent, SpriteBatch, VoidT>().Register(batch =>
        {
            batch.End();
            return new VoidT();
        });
        _events.Publish<StartDrawEvent,SpriteBatch, VoidT>().RegisterParameterProvider(() => _spriteBatch!);
        _events.Publish<DrawEvent,SpriteBatch, VoidT>().RegisterParameterProvider(() => _spriteBatch!);
        _events.Publish<EndDrawEvent,SpriteBatch, VoidT>().RegisterParameterProvider(() => _spriteBatch!);
        
        OnSwitchScene(new SplashScreen());
    }

    private VoidT OnSwitchScene(IScene arg)
    {
        Console.WriteLine("Switching to scene " +arg.GetType().Name);
        if (activeScene != null)
        {
            activeScene.ClearEvents(_events);
            activeScene.RemoveObjects(_entities);
        }
        activeScene = arg;
        activeScene.InstantiateObjects(_entities);
        activeScene.RegisterEvents(_events);
        _events.Publish<SwitchSceneEvent, IScene, VoidT>().RegisterParameterProvider(null);
        return new VoidT();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(Graphics.GraphicsDevice);
        _events.ExecuteEvents<LoadAssetsEvent>();
        // var mapRenderer = new MapRenderer(new TiledMapLoader(Content));
        // mapRenderer.LoadContent(GraphicsDevice,"Assets/NatureMap");
        //
        // var player = new Player(mapRenderer.Map, 0, 6);
        // var inputHandler = new KeyboardInput();
        // var playerMovement = new PlayerMovement(player, inputHandler, 150);

        // _etherwildGame = new EtherwildGame(mapRenderer, playerMovement, inputHandler, player);
    }

    protected override void Update(GameTime gameTime)
    {
        _events.ExecuteEvents<SwitchSceneEvent>();
        _events.Publish<UpdateEvent, GameTime, VoidT>().Execute(gameTime);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _events.Publish<StartDrawEvent, SpriteBatch, VoidT>().Execute(_spriteBatch);
        _events.Publish<DrawEvent, SpriteBatch, VoidT>().Execute(_spriteBatch);
        _events.Publish<EndDrawEvent, SpriteBatch, VoidT>().Execute(_spriteBatch);
        base.Draw(gameTime);
    }
}


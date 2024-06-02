using EtherwildTransparencyTest.Boilerplate;
using EtherwildTransparencyTest.Controllers;
using EtherwildTransparencyTest.Core;
using EtherwildTransparencyTest.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OverlayWindow;
using KeyboardInput = EtherwildTransparencyTest.Core.KeyboardInput;

namespace EtherwildTransparencyTest.App;

public sealed class EtherwildGameWindow : Game
{
    protected GraphicsDeviceManager Graphics;
    private SpriteBatch _spriteBatch;
    private EtherwildGame _etherwildGame;
    private EntityController _entities;
    private EventController _events;
    private IScene activeScene;

    public EtherwildGameWindow()
    {
        Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        _entities = new EntityController();
        _events = new EventController();
        _events.CreateEvent<SpriteBatch, VoidT>(Constants.Sequence_Draw-500, "StartDraw");
        _events.CreateEvent<SpriteBatch, VoidT>(Constants.Sequence_Draw, "Draw");
        _events.CreateEvent<SpriteBatch, VoidT>(Constants.Sequence_Draw+500, "EndDraw");
        _events.CreateEvent<GameTime, VoidT>(Constants.Sequence_Update, "Update");
        _events.CreateEvent<GameTime, VoidT>(Constants.Sequence_Load, "LoadContent");
        _events.CreateEvent<IScene, VoidT>(Constants.Sequence_SceneSwitch, "SceneSwitch");
        _events.Listen<IScene, VoidT>(Constants.Sequence_SceneSwitch).Register(OnSwitchScene);
        _events.Publish<IScene, VoidT>(Constants.Sequence_SceneSwitch).RegisterParameterProvider(() => new SplashScreen());
        
        _events.Listen<SpriteBatch, VoidT>("StartDraw").Register(batch =>
        {
            GraphicsDevice.Clear(Color.DarkRed);
            batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp);
            return new VoidT();
        });
        _events.Listen<SpriteBatch, VoidT>("EndDraw").Register(batch =>
        {
            batch.End();
            return new VoidT();
        });
    }

    private VoidT OnSwitchScene(IScene arg)
    {
        activeScene = arg;
        activeScene.RegisterEvents(_events);
        _events.Publish<IScene, VoidT>("SceneSwitch").RegisterParameterProvider(null);
        return new VoidT();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(Graphics.GraphicsDevice);
        
        _events.ExecuteEvents(Constants.Sequence_Load);
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
        // _etherwildGame.Update(gameTime, this);
        
        _events.ExecuteEvents(Constants.Sequence_SceneSwitch-500);
        _events.ExecuteEvents(Constants.Sequence_Update-500);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _events.ExecuteEvents(Constants.Sequence_Draw-500);
        base.Draw(gameTime);
    }
}


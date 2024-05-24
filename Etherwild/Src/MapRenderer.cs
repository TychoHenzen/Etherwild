using System.Runtime.InteropServices;
using Etherwild.Adapters;

namespace Etherwild;
public class MapRenderer
{
    private IGraphicsDevice _graphicsDevice;
    private IContentManager _content;
    private ITiledMap _map;
    private IGameDrawable _mapRenderer;
    private ITransform _scaleTransform;

    public MapRenderer(IGraphicsDevice graphicsDevice, IContentManager content, ITransform scale)
    {
        _graphicsDevice = graphicsDevice;
        _content = content;
        _scaleTransform = scale;
    }

    public void LoadContent(string mapAssetName)
    {
        _map = _content.Load<ITiledMap>(mapAssetName);
        // Assume _mapRenderer is initialized here using factory or DI
    }

    public void Draw()
    {
        _graphicsDevice.Draw(_mapRenderer, _scaleTransform, new MyRectangle(0,0,0,0), new MyColor(255,255,255,255));
    }
}
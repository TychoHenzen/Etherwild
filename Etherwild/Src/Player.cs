

using Etherwild.Adapters;

namespace Etherwild;public class Player : IPositionable, IMovable
{
    private ITiledSet _tileset;
    private IVector2 _position;
    private float _speed = 150;
    private int _tileX, _tileY;

    public IVector2 Position { get; set; }

    public IRectangle BoundingBox => 
        new MyRectangle((int)_position.X, (int)_position.Y, _tileset.TileWidth, _tileset.TileHeight);

    public Player(ITiledSet tileset, int tileX, int tileY)
    {
        _tileset = tileset;
        _tileX = tileX;
        _tileY = tileY;
        _position = new MyVector2(100, 100); // Initial position
    }

    public void Update(IGameTime gameTime, IInputReader inputReader)
    {
        IVector2 movement = MyVector2.Zero;

        if (inputReader.IsKeyPressed(Keys.W))
            movement.Y -= 1;
        if (inputReader.IsKeyPressed(Keys.S))
            movement.Y += 1;
        if (inputReader.IsKeyPressed(Keys.A))
            movement.X -= 1;
        if (inputReader.IsKeyPressed(Keys.D))
            movement.X += 1;

        movement *= _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        _position += movement;
    }

    public void Draw(IGraphicsDevice context)
    {
        
        var rect = _tileset.GetRegion(_tileX, _tileY).Bounds;
        context.Draw(_tileset.Texture, _position, rect, IColor.White);
    }
}
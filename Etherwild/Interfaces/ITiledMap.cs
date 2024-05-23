using Etherwild.Adapters;

namespace Etherwild;

public interface IGraphicsDevice
{
  void Draw(IGameDrawable drawable, IVector2 position, IRectangle rect, IColor col);
  // Add other necessary graphics device methods
}

public interface IContentManager
{
  T Load<T>(string assetName);
}

public interface IGameDrawable
{
  void Draw(IGraphicsDevice graphicsDevice, ITransform transform);
}

public interface ITransform
{
  IMatrix ScaleMatrix { get; }
}

public interface ITiledMap
{
  // Define necessary map properties and methods
}
public interface ITextureRegion2D
{
  // Define necessary map properties and methods
  IRectangle Bounds { get; }
}
public interface ITiledSet
{
  // Define necessary map properties and methods
  int TileWidth { get; }
  int TileHeight { get; }
  IGameDrawable Texture { get; }
  ITextureRegion2D GetRegion(int tileX, int tileY);
}
public interface IMatrix
{
  // Define necessary map properties and methods
}
public interface IPositionable
{
  IVector2 Position { get; set; }
  IRectangle BoundingBox { get; }
}

public interface IMovable
{
  void Update(IGameTime gameTime, IInputReader inputReader);
}
public interface IInputReader
{
  bool IsKeyPressed(Keys key);
}

public interface IVector2
{
  float X { get; set; }
  float Y { get; set; }

  public static IVector2 operator +(IVector2 lhs, IVector2 rhs) => new MyVector2(lhs.X + rhs.X, lhs.Y + rhs.Y);
  public static IVector2 operator *(IVector2 lhs, float rhs) => new MyVector2(lhs.X * rhs, lhs.Y * rhs);
}
public interface IRectangle
{

}
public interface IColor
{
  static IColor White => new MyColor(0, 0, 0, 1);
}
public interface IGameTime
{
  TimeSpan ElapsedGameTime { get; }
}
namespace Etherwild.Adapters;

public sealed record MyVector2(float X, float Y) : IVector2
{
  public static IVector2 Zero => new MyVector2(0, 0);
  public float X { get; set; } = X;
  public float Y { get; set; } = Y;
}

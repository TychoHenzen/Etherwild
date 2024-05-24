namespace Etherwild.Adapters;

public record MyRectangle(int X, int Y, int Width, int Height) : IRectangle ;
public record MyColor(byte R, byte G, byte B, byte A) : IColor;
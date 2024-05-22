using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;

namespace EtherwildTransparencyTest.Boilerplate;
public class Player
{
    private TiledMapTileset _tileset;
    private Vector2 _position;
    private float speed = 150;
    private int x,y = 0;

    public Rectangle BoundingBox => 
        new((int)_position.X, (int)_position.Y, _tileset.TileWidth, _tileset.TileHeight);

    public Player(TiledMapTileset tiles, int x, int y)
    {
        _tileset = tiles;
        this.x = x;
        this.y = y;
        _position = new Vector2(100, 100); // Initial position
    }

    public void Update(GameTime gameTime)
    {
        Vector2 movement = Vector2.Zero;

        if (Keyboard.GetState().IsKeyDown(Keys.W))
            movement.Y -= 1;
        if (Keyboard.GetState().IsKeyDown(Keys.S))
            movement.Y += 1;
        if (Keyboard.GetState().IsKeyDown(Keys.A))
            movement.X -= 1;
        if (Keyboard.GetState().IsKeyDown(Keys.D))
            movement.X += 1;

        movement *= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        // // Check for collisions
        // Vector2 newPosition = _position + movement;
        // Rectangle newBoundingBox = new Rectangle((int)newPosition.X, (int)newPosition.Y, _sourceRectangle.Width, _sourceRectangle.Height);
        //
        // foreach (var rect in collisionRects)
        // {
        //     if (newBoundingBox.Intersects(rect))
        //     {
        //         movement = Vector2.Zero;
        //         break;
        //     }
        // }

        _position += movement;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var rect = _tileset.GetRegion(x, y).Bounds;
        spriteBatch.Draw(_tileset.Texture, _position, rect, Color.White);
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Spaceship;

public class Ship
{
    public Vector2 Position;
    public readonly int MovementSpeed = 300;
    public int Radius = 30;

    public Ship(Vector2 defaultPosition)
    {
        Position = defaultPosition;
    }

    public void Update(GameTime gameTime)
    {
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (Keyboard.GetState().IsKeyDown(Keys.Up) && Position.Y > 50)
        {
            Position.Y -= MovementSpeed * deltaTime;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Down) &&
            Position.Y < Globals.Graphics.PreferredBackBufferHeight - 50)
        {
            Position.Y += MovementSpeed * deltaTime;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Left) && Position.X > 50)
        {
            Position.X -= MovementSpeed * deltaTime;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Right) &&
            Position.X < Globals.Graphics.PreferredBackBufferWidth - 50)
        {
            Position.X += MovementSpeed * deltaTime;
        }
    }
}
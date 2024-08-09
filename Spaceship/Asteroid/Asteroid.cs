using System;
using Microsoft.Xna.Framework;

namespace Spaceship.Asteroid;

public class Asteroid
{
    public Vector2 Position = new Vector2(0,0);
    public bool ShouldBeRemoved = false;
    public int Radius = 55;
    
    private readonly float _movementSpeed = 0f;
    
    public Asteroid(int speedModifier, int maxX, int maxY)
    {
        var rnd = new Random();
        Position.Y = rnd.Next(50, maxY);
        Position.X = maxX + 100;
        
        _movementSpeed = rnd.Next(200 * speedModifier, 400 * speedModifier);
    }

    public void Update(GameTime gameTime)
    {
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        Position.X -= _movementSpeed * deltaTime;

        if (Position.X < -100)
            ShouldBeRemoved = true;
    }
}
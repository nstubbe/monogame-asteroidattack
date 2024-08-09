using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Spaceship.Asteroid;

public class AsteroidManager
{
    public List<Asteroid> Asteroids = new List<Asteroid>();
    public double SpeedModifier = 1;
    
    private double _timeUntilNextAsteroidSpawn = 2;
    private int _maxAsteroidCount = 5;
    
    public void Update(GameTime gameTime)
    {
        _timeUntilNextAsteroidSpawn -= gameTime.ElapsedGameTime.TotalSeconds;

        if (_timeUntilNextAsteroidSpawn <= 0 && Asteroids.Count <= _maxAsteroidCount)
        {
            SpeedModifier += gameTime.ElapsedGameTime.TotalSeconds * 1.5;
            Asteroids.Add(new Asteroid((int)SpeedModifier, Globals.Graphics.PreferredBackBufferWidth,
                Globals.Graphics.PreferredBackBufferHeight));

            var rnd = new Random();
            _timeUntilNextAsteroidSpawn = (rnd.Next(0, 2) + 0.5f) / (SpeedModifier * 2);
        }

        foreach (var asteroid in Asteroids)
        {
            asteroid.Update(gameTime);
        }
        
        var asteroidsToDispose = Asteroids.Where(a => a.ShouldBeRemoved).ToList();
        foreach (var asteroid in asteroidsToDispose)
            Asteroids.Remove(asteroid);
    }
}
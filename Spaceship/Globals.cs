using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spaceship;

public static class Globals
{
    public static GraphicsDeviceManager Graphics;

    public static void Init(GraphicsDeviceManager graphics)
    {
        Graphics = graphics;
    }
}
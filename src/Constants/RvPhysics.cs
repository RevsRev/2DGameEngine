using Microsoft.Xna.Framework;

public class RvPhysics
{
    public static readonly float PIXELS_TO_METERS = 50;
    public static readonly float ACCELERATION_DUE_TO_GRAVITY = 9.81f * PIXELS_TO_METERS;
    public static readonly Vector2 GRAVITY = new Vector2(0, ACCELERATION_DUE_TO_GRAVITY);
}
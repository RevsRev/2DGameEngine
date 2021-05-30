using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class RvFlyingEye : RvPhysicalObject
{
    public static readonly int ANIMATION_FLYING_EYE_FLIGHT = 0;

    public RvFlyingEye(Vector2 position, Vector2 velocity, float mass = 0.1f) : base(position, velocity, mass, null)
    {
        setHitbox(new Shapes.RvHorizontalRectangle(position, 50, 75));
        setImmovable(true);
    }

    public void initSprite()
    {
        List<RvAnimation> animations = new List<RvAnimation>();
        animations.Add(RvAnimation.factory(RvContentFiles.FLYING_EYE + "FlyingEye_Flight", 1, 8, ANIMATION_FLYING_EYE_FLIGHT, true, new Rectangle(45, 45, 15, 15)));
        RvDrawableObject sprite = new RvDrawableObject(animations);
        sprite.setCurrentAnimation(ANIMATION_FLYING_EYE_FLIGHT);
        setSprite(sprite);
    }
}
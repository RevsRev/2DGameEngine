using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class RvFlyingEye : RvPhysicalObject
{
    public static readonly int ANIMATION_FLYING_EYE_FLIGHT = 0;

    public RvFlyingEye(Game game, Vector2 position, Vector2 velocity, float mass = 0.1f) : base(game, position, velocity, mass, null)
    {
        initSprite(game);
        setHitbox(new Shapes.RvHorizontalRectangle(position, 50, 75));
        setImmovable(true);
    }

    public void initSprite(Game game)
    {
        List<RvAnimation> animations = new List<RvAnimation>();

        animations.Add(RvAnimation.factory(game, RvContentFiles.FLYING_EYE + "FlyingEye_Flight", 1, 8, ANIMATION_FLYING_EYE_FLIGHT, true, new Rectangle(45, 45, 15, 15)));

        RvDrawableObject sprite = new RvDrawableObject(animations);
        sprite.setCurrentAnimation(ANIMATION_FLYING_EYE_FLIGHT);
        setSprite(sprite);
    }
}
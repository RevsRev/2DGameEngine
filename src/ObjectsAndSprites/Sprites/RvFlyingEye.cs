using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class RvFlyingEye : RvCollidableSprite
{
    public static readonly int ANIMATION_FLYING_EYE_FLIGHT = 0;

    public RvFlyingEye(Vector2 position) : this(position, new Shapes.RvHorizontalRectangle(position, 50, 50))
    {
        
    }

    public RvFlyingEye(Vector2 position, RvAbstractShape shape) : this(position, Vector2.Zero, shape, getFlyingEyeAnimations())
    {

    }

    public RvFlyingEye(Vector2 position, Vector2 velocity, RvAbstractShape shape, List<RvAnimation> animations, float layer = 0.0f, bool visible = true)
      : base(position, velocity, shape, animations, layer, visible)
    {
    }

    private static List<RvAnimation> getFlyingEyeAnimations()
    {
        List<RvAnimation> animations = new List<RvAnimation>();
        animations.Add(RvAnimation.factory(RvContentFiles.FLYING_EYE + "FlyingEye_Flight", 1, 8, ANIMATION_FLYING_EYE_FLIGHT, true, new Rectangle(45, 45, 15, 15)));
        return animations;
    }
}

public class RvFlyingEyeWrapper : RvCollidableSriteWrapper
{
    public RvFlyingEyeWrapper(Vector2 position, Vector2 velocity, RvAbstractShapeWrapper shape, List<RvAnimationWrapper> animations, float layer = 0.0f, bool visible = true) 
      : base(position, velocity, shape, animations, layer, visible)
    {

    }

    public override RvFlyingEye unWrap()
    {
        return new RvFlyingEye(position, velocity, shapeWrapper.unWrap(), RvWrapperUtils.unwrapVector<RvAnimation, RvAnimationWrapper>(animationWrappers), layer, visible);
    }
}
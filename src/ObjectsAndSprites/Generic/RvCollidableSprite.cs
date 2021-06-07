using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class RvCollidableSprite : RvSprite, RvCollidableI
{
    public RvCollidableSprite(Vector2 position, Vector2 velocity, RvAbstractShape shape, List<RvAnimation> animations, float layer = 0.0f, bool visible = true)
      : base(position, velocity, shape, animations, layer, visible)
    {

    }

    public virtual void doCollision()
    {
        //implement
    }

    public RvAbstractShape getHitbox()
    {
        return shape;
    }
}

public class RvCollidableSriteWrapper : RvSpriteWrapper
{
    public RvCollidableSriteWrapper(Vector2 position, Vector2 velocity, RvAbstractShapeWrapper shape, List<RvAnimationWrapper> animations, float layer = 0.0f, bool visible = true) 
      : base(position, velocity, shape, animations, layer, visible)
    {

    }

    public override RvCollidableSprite unWrap()
    {
        return new RvCollidableSprite(position, velocity, shapeWrapper.unWrap(), RvWrapperUtils.unwrapVector<RvAnimation, RvAnimationWrapper>(animationWrappers), layer, visible);
    }
}
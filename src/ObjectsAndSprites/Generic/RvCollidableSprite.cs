using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class RvCollidableSrite : RvSprite, RvCollidableI
{
    public RvCollidableSrite(Vector2 position, Vector2 velocity, RvAbstractShape shape, List<RvAnimation> animations, float layer = 0.0f, bool visible = true)
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

    public override RvSprite unWrap()
    {
        return (RvCollidableSrite)base.unWrap();
    }
}
using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class RvCollidableSrite : RvSprite, RvCollidableI
{
    public RvCollidableSrite(Vector2 position, RvAbstractShape shape, List<RvAnimation> animations, float layer = 0.0f, bool visible = true)
      : base(position, shape, animations, layer, visible)
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
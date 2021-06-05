using Microsoft.Xna.Framework;

public interface RvCollidableI
{
    RvAbstractShape getHitbox();
    void doCollision();
}
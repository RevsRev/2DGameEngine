using Microsoft.Xna.Framework;

//not currently used anywhere because c# won't let these methods be visible on a class that implements it like java does:(

public interface RvPhysicalObjectI
{   
    bool doesPhysics()
    {
        return false;
    }
    Vector2 getForces()
    {
        return RvPhysics.GRAVITY;
    }
    float getMass()
    {
        return 1.0f;
    }

    public void doPhysics(GameTime gameTime, ref Vector2 velocity) => reallyDoPhysics(gameTime, ref velocity);

    void reallyDoPhysics(GameTime gameTime, ref Vector2 velocity)
    {
        if (doesPhysics())
        {
            accelerate(gameTime, ref velocity, getForces()/getMass());
        }
    }
    void accelerate(GameTime gameTime, ref Vector2 velocity, Vector2 acceleration)
    {
        velocity += gameTime.ElapsedGameTime.Seconds * acceleration;
    }
}
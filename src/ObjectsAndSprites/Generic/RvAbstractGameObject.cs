using Microsoft.Xna.Framework;
using Newtonsoft.Json;

public abstract class RvAbstractGameObject : RvAbstractWrappable, RvUpdatableI
{
    protected Vector2 position;
    protected Vector2 velocity;
    protected RvAbstractShape shape;

    public RvAbstractGameObject(Vector2 position, RvAbstractShape shape)
      : this(position, Vector2.Zero, shape)
    {
    }

    public RvAbstractGameObject(Vector2 position, Vector2 velocity, RvAbstractShape shape)
    {
        this.position = position;
        this.velocity = velocity;
        this.shape = shape;
    }

    public abstract override RvAbstractGameObjectWrapper wrap();

    public virtual void update(GameTime gameTime)
    {
        doPhysics(gameTime);
        position += velocity * gameTime.ElapsedGameTime.Seconds;
        shape.setTranslation(position);
    }

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

    void doPhysics(GameTime gameTime)
    {
        if (doesPhysics())
        {
            accelerate(gameTime, getForces()/getMass());
        }
    }
    void accelerate(GameTime gameTime, Vector2 acceleration)
    {
        velocity += gameTime.ElapsedGameTime.Seconds * acceleration;
    }
}

public abstract class RvAbstractGameObjectWrapper : RvAbstractWrapper
{
    [JsonProperty] protected Vector2 position {get; set;}
    [JsonProperty] protected Vector2 velocity {get; set;}
    [JsonProperty] protected RvAbstractShapeWrapper shapeWrapper {get; set;}

    public RvAbstractGameObjectWrapper(Vector2 position, Vector2 velocity, RvAbstractShapeWrapper shapeWrapper)
    {
        this.position = position;
        this.velocity = velocity;
        this.shapeWrapper = shapeWrapper;
    }

    public abstract override RvAbstractGameObject unWrap();
}
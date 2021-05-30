using Microsoft.Xna.Framework;
using Newtonsoft.Json;

public abstract class RvAbstractGameObject : RvAbstractWrappable, RvUpdatableI
{
    protected Vector2 position {get; set;}
    protected RvAbstractShape shape {get; set;}

    public RvAbstractGameObject(Vector2 position, RvAbstractShape shape)
    {
        this.position = position;
        this.shape = shape;
    }

    public abstract override RvAbstractGameObjectWrapper wrap();

    public abstract void update(GameTime gameTime);
}

public abstract class RvAbstractGameObjectWrapper : RvAbstractWrapper
{
    [JsonProperty] protected Vector2 position {get; set;}
    [JsonProperty] protected RvAbstractShape shape {get; set;}

    public RvAbstractGameObjectWrapper(Vector2 position, RvAbstractShape shape)
    {
        this.position = position;
        this.shape = shape;
    }

    public abstract override RvAbstractGameObject unWrap();
}
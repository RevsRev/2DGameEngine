using Microsoft.Xna.Framework;
using Newtonsoft.Json;

public abstract class RvAbstractGameObject : RvAbstractWrappable, RvUpdatableI
{
    private Vector2 position {get; set;}
    private RvAbstractShape shape {get; set;}

    public RvAbstractGameObject(Vector2 position, RvAbstractShape shape)
    {
        this.position = position;
        this.shape = shape;
    }

    public abstract override RvAbstractGameObjectWrapper wrap();

    public abstract void update();
}

public abstract class RvAbstractGameObjectWrapper : RvAbstractWrapper
{
    [JsonProperty] private Vector2 position {get; set;}
    [JsonProperty] private RvAbstractShape shape {get; set;}

    public RvAbstractGameObjectWrapper(Vector2 position, RvAbstractShape shape)
    {
        this.position = position;
        this.shape = shape;
    }

    public abstract override RvAbstractGameObject unWrap();
}
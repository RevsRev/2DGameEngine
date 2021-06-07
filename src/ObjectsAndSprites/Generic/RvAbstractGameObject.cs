using Microsoft.Xna.Framework;
using Newtonsoft.Json;

public abstract class RvAbstractGameObject :    RvAbstractWrappable, 
                                                RvUpdatableI, 
                                                RvDisposableI, 
                                                RvPopupMenuListenerI, 
                                                RvMouseListenerI
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

        RvMouse.the().addListener(this);
        registerHandlers();
    }

    public void dispose()
    {
        RvMouse.the().removeListener(this);
        disposeHandlers();
    }
    public abstract void disposeHandlers();
    public abstract void registerHandlers();

    public abstract override RvAbstractGameObjectWrapper wrap();

    public virtual void update(GameTime gameTime)
    {
        doPhysics(gameTime);
        position += velocity * gameTime.ElapsedGameTime.Seconds;
        shape.setTranslation(position);
    }

    public Rectangle getClickableRegion()
    {
        return shape.getRectangle();
    }
    public void doDrag(Vector2 mouseCoords, Vector2 anchorPoint)
    {
        Vector2 screenPosition = mouseCoords - anchorPoint;
        position = RvEditor.mapScreenCoordsToGameCoords(screenPosition);
        shape.setTranslation(position);
    }

    public void doClick(RvMouseEvent e)
    {
        RvPopupMenuListenerI.onClick(e, this);
    }

    public RvPopupMenu buildPopupMenu()
    {
        RvPopupMenu retval = new RvPopupMenu();
        retval.addPopupMenuItem("Properties");
        retval.addPopupMenuItem("Remove");
        return retval;
    }

    public void performPopupMenuAction(string actionStr)
    {
        //not doing anything for now
    }

    public RvAbstractShape getShape()
    {
        return shape;
    }
    public Vector2 getPosition()
    {
        return position;
    }
    public float getHitboxWidth()
    {
        return shape.getWidth();
    }
    public float getHitboxHeight()
    {
        return shape.getHeight();
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
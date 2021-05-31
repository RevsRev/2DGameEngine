using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Shapes;
using System;
using Newtonsoft.Json;

public class RvPhysicalObject : RvAbstractGameObject, RvMouseListenerI, RvPopupMenuListenerI, RvDisposableI
{
    public static readonly float EPSILON                    = 0.01f; //for dealing with small floats.
    public static readonly float OBJECT_DEPTH               = 1.0f; //for collision detection
    public static readonly float AIRBORNE_LIMIT             = 0.05f;
    public static readonly float MINIMUM_HORIZONTAL_VELOCITY = 10.0f;
    public static readonly float DEFAULT_JUMP_IMPULSE       = 10.0f;

    protected float mass;
    protected Vector2 velocity;
    protected RvDrawableObject sprite;
    protected bool immovable = false; //for platforms and suchlike.

    //Computational variables
    private bool isDoingACollision = false;
    private float timeSinceLastCollision = 0.0f;


    public RvPhysicalObject(Vector2 position, Vector2 velocity, float mass, RvAbstractShape hitBox = null) : this(position, velocity, null, mass, false, hitBox)
    {
    }

    public RvPhysicalObject(Vector2 position, Vector2 velocity, RvDrawableObject sprite, float mass, bool immovable, RvAbstractShape hitBox = null) : base(position, hitBox)
    {
        this.velocity = velocity;
        this.sprite = sprite;
        this.mass = mass;
        this.immovable = immovable;

        RvMouse.the().addMouseListener(this);
    }

    public override RvPhysicalObjectWrapper wrap()
    {
        //haven't handled null sprite here. Not sure if it'll be a problem or not...
        return new RvPhysicalObjectWrapper(position, velocity, sprite.wrap(), mass, immovable, shape.wrap());
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

    public void performPopupMenuAction(String actionStr)
    {
        //not doing anything for now
    }

    public void dispose()
    {
        //need to implement
    }

    public void setSprite(RvDrawableObject sprite)
    {
        this.sprite = sprite;
    }
    public RvDrawableObject getSprite()
    {
        return sprite;
    }

    public void setHitbox(RvHorizontalRectangle hitBox)
    {
        this.shape = shape;
    }

    public override void update(GameTime gameTime)
    {
        updateKinematics(gameTime, getForceDueToFriction());

        if (isDoingACollision)
        {
            timeSinceLastCollision = 0.0f;
        }
        else
        {
            timeSinceLastCollision = MathHelper.Min(timeSinceLastCollision + (float)gameTime.ElapsedGameTime.TotalSeconds, 1.0f);
        }

        if (Math.Abs(velocity.X) < MINIMUM_HORIZONTAL_VELOCITY)
        {
            velocity.X = 0;
        }

        position += Vector2.Multiply(velocity, (float)gameTime.ElapsedGameTime.TotalSeconds);
        shape.setTranslation(position);
        sprite.update(gameTime);
    }

    public bool isAirborne()
    {
        //This obviously isn't ideal, but it works for the time being.
        return (!isDoingACollision && timeSinceLastCollision > AIRBORNE_LIMIT);
    }
    public bool isFalling()
    {
        return isAirborne() && velocity.Y > 0;
    }
    public bool isRising()
    {
        return isAirborne() && velocity.Y < 0;
    }

    public void updateKinematics(GameTime gameTime, Vector2 force)
    {
        if (!immovable)
        {
            velocity += Vector2.Multiply(force, (float)gameTime.ElapsedGameTime.TotalSeconds/mass);
        }
    }
    public void accelerate(GameTime gameTime, Vector2 acceleration)
    {
        if (!immovable)
        {
            velocity += Vector2.Multiply(acceleration, (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }

    //to be overriden in child classes.
    public virtual Vector2 getForceDueToFriction()
    {
        return Vector2.Zero;
    }

    public void move(Vector2 translation)
    {
        if (!immovable)
        {
            position += translation;
        }
    }

    public bool collidesWith(RvPhysicalObject otherObject)
    {
        return ((RvShapeI)shape).overlapping(otherObject.shape);
    }

    public Vector2 getRestoringDirection(RvPhysicalObject otherObject)
    {
        return otherObject.shape.getRestoringDirection(shape.sampleEdgePoints());
    }

    public void draw()
    {
        sprite.Draw(shape.getRectangle());

        if (RvDebug.isDebugMode())
        {
            //todo - fix this
            //shape.drawBoundary();
        }
    }

    public void setVelocity(Vector2 velocity)
    {
        this.velocity = velocity;
    }
    public void setVelocity(float vX, float vY)
    {
        velocity.X = vX;
        velocity.Y = vY;
    }
    public Vector2 getVelocity()
    {
        return velocity;
    }

    public void setImmovable(bool immovable)
    {
        this.immovable = immovable;
    }
    public bool getImmovable()
    {
        return immovable;
    }

    public void setIsDoingACollision(bool isDoingACollision)
    {
        this.isDoingACollision = isDoingACollision;
    }
    public bool getIsDoingACollision()
    {
        return isDoingACollision;
    }
    public float getMass()
    {
        return mass;
    }

    public bool doJump(GameTime gameTime)
    {
        return doJump(gameTime, DEFAULT_JUMP_IMPULSE);
    }

    public bool doJump(GameTime gameTime, float impulse)
    {
        if (isAirborne())
        {
            return false;
        }
        Vector2 force = new Vector2(0, -impulse/(float)gameTime.ElapsedGameTime.TotalSeconds);
        updateKinematics(gameTime, force);
        return true;
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
    public RvShapeI getHitbox()
    {
        return shape;
    }
    public void setPosition(Vector2 position)
    {
        this.position = position;
    }
    public void setPositionAndHitbox(Vector2 position)
    {
        this.position = position;
        shape.setTranslation(position);
    }
}

public class RvPhysicalObjectWrapper : RvAbstractGameObjectWrapper
{
    [JsonProperty] public float mass;
    [JsonProperty] public Vector2 velocity;
    [JsonProperty] public RvDrawableObjectWrapper spriteWrapper;

    [JsonProperty] public bool immovable = false;

    public RvPhysicalObjectWrapper(Vector2 position, Vector2 velocity, RvDrawableObjectWrapper spriteWrapper, float mass, bool immovable, RvAbstractShapeWrapper hitBox) : base(position, hitBox)
    {
        this.mass = mass;
        this.velocity = velocity;
        this.spriteWrapper = spriteWrapper;
        this.immovable = immovable;
    }

    public override RvPhysicalObject unWrap()
    {
        RvDrawableObject sprite = spriteWrapper.unWrap();
        return new RvPhysicalObject(position, velocity, sprite, mass, immovable, shapeWrapper.unWrap());
    }
}
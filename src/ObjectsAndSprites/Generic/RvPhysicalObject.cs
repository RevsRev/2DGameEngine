using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Shapes;
using System;
using Newtonsoft.Json;

public class RvPhysicalObject : GameComponent, RvMouseListenerI, RvPopupMenuListenerI, RvDisposable
{
    public static readonly float EPSILON                    = 0.01f; //for dealing with small floats.
    public static readonly float OBJECT_DEPTH               = 1.0f; //for collision detection
    public static readonly float AIRBORNE_LIMIT             = 0.05f;
    public static readonly float MINIMUM_HORIZONTAL_VELOCITY = 10.0f;
    public static readonly float DEFAULT_JUMP_IMPULSE       = 10.0f;

    //Physical variables
    protected Vector2 position;
    protected float mass;
    protected RvHorizontalRectangle hitBox;
    protected Vector2 velocity;
    protected RvDrawableObject sprite;
    protected bool immovable = false; //for platforms and suchlike.

    //Computational variables
    private bool isDoingACollision = false;
    private float timeSinceLastCollision = 0.0f;


    public RvPhysicalObject(Game game, Vector2 position, Vector2 velocity, float mass, RvHorizontalRectangle hitBox = null) : base(game)
    {
        this.position = position;
        this.velocity = velocity;
        this.mass = mass;
        this.hitBox = hitBox;

        RvMouse.the().addMouseListener(this);
    }

    public RvPhysicalObject(Game game, Vector2 position, Vector2 velocity, RvDrawableObject sprite, float mass, bool immovable, RvHorizontalRectangle hitBox = null) : base(game)
    {
        this.position = position;
        this.velocity = velocity;
        this.sprite = sprite;
        this.mass = mass;
        this.immovable = immovable;
        this.hitBox = hitBox;

        RvMouse.the().addMouseListener(this);
    }

    public static RvPhysicalObject factory(Game game, RvPhysicalObjectWrapper w)
    {
        return w.createObject(game);
    }

    public virtual RvPhysicalObjectWrapper createWrapper()
    {
        //haven't handled null sprite here. Not sure if it'll be a problem or not...
        return new RvPhysicalObjectWrapper(position, velocity, sprite.createWrapper(), mass, immovable, hitBox);
    }

    public Rectangle getClickableRegion()
    {
        return hitBox.getRectangle();
    }
    public void doDrag(Vector2 mouseCoords, Vector2 anchorPoint)
    {
        Vector2 screenPosition = mouseCoords - anchorPoint;
        position = RvEditor.mapScreenCoordsToGameCoords(screenPosition);
        hitBox.setTranslation(position);
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
        this.hitBox = hitBox;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

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
        hitBox.setTranslation(position);
        sprite.Update(gameTime);
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
        return ((RvShapeI)hitBox).overlapping(otherObject.hitBox);
    }

    public Vector2 getRestoringDirection(RvPhysicalObject otherObject)
    {
        return otherObject.hitBox.getRestoringDirection(hitBox.sampleEdgePoints());
    }

    public void draw()
    {
        sprite.Draw(hitBox.getRectangle());

        if (RvDebug.isDebugMode())
        {
            hitBox.drawBoundary();
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
        return hitBox.getWidth();
    }
    public float getHitboxHeight()
    {
        return hitBox.getHeight();
    }
    public RvHorizontalRectangle getHitbox()
    {
        return hitBox;
    }
    public void setPosition(Vector2 position)
    {
        this.position = position;
    }
    public void setPositionAndHitbox(Vector2 position)
    {
        this.position = position;
        hitBox.setTranslation(position);
    }
}

public class RvPhysicalObjectWrapper
{
    [JsonProperty] public Vector2 position;
    [JsonProperty] public float mass;
    [JsonProperty] public RvHorizontalRectangle hitBox;
    [JsonProperty] public Vector2 velocity;
    [JsonProperty] public RvDrawableObjectWrapper spriteWrapper;

    [JsonProperty] public bool immovable = false;

    public RvPhysicalObjectWrapper(Vector2 position, Vector2 velocity, RvDrawableObjectWrapper spriteWrapper, float mass, bool immovable, RvHorizontalRectangle hitBox)
    {
        this.position = position;
        this.mass = mass;
        this.hitBox = hitBox;
        this.velocity = velocity;
        this.spriteWrapper = spriteWrapper;
        this.immovable = immovable;
    }

    public virtual RvPhysicalObject createObject(Game game)
    {
        RvDrawableObject sprite = RvDrawableObject.factory(game, spriteWrapper);
        return new RvPhysicalObject(game, position, velocity, sprite, mass, immovable, hitBox);
    }
}
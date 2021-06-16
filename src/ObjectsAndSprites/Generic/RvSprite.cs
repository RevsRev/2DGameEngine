using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Newtonsoft.Json;

public class RvSprite : RvAbstractGameObject, RvDrawableI
{
    //layers for drawing. This should be enough for now. Leaving gaps to add more.
    public const int SPRITE_LAYER_FRONT = 0;
    public const int SPRITE_LAYER_FRONT_MID = 10;
    public const int SPRITE_LAYER_MID = 20;
    public const int SPRITE_LAYER_MID_BACK = 30;
    public const int SPRITE_LAYER_BACK = 40;

    // public static readonly float SPRITE_DRAWING_LAYER_FRONT = 0.5f + SPRITE_LAYER_FRONT/100.0f;
    // public static readonly float SPRITE_DRAWING_LAYER_MID_FRONT = 0.5f + SPRITE_LAYER_FRONT_MID/100.0f;
    // public static readonly float SRPITE_DRAWING_LAYER_MID = 0.5f + SPRITE_LAYER_MID/100.0f;
    // public static readonly float SPRITE_DRAWING_LAYER_MID_BACK = 0.5f + SPRITE_LAYER_MID_BACK/100.0f;
    // public static readonly float SRPITE_DRAWING_LAYER_BACK = 0.5f + SPRITE_LAYER_BACK/100.0f;

    protected List<RvAnimation> animations;
    int currentAnimation = 0;

    protected bool visible = true;
    protected int layerNumber = SPRITE_LAYER_MID;

    public RvSprite(Vector2 position, RvAbstractShape shape, List<RvAnimation> animations, int layerNumber = SPRITE_LAYER_MID, bool visible = true)
      : this(position, Vector2.Zero, shape, animations, layerNumber, visible)
      {

      }
    public RvSprite(Vector2 position, Vector2 velocity, RvAbstractShape shape, List<RvAnimation> animations, int layerNumber = SPRITE_LAYER_MID, bool visible = true) : base(position, velocity, shape)
    {
        this.animations = animations;
        this.layerNumber = layerNumber;
        this.visible = visible;
    }

    public override RvSpriteWrapper wrap()
    {
        List<RvAnimationWrapper> animationWrappers = new List<RvAnimationWrapper>();
        for (int i=0; i<animations.Count; i++)
        {
            RvAnimation animation = animations[i];
            animationWrappers.Add(animation.wrap());
        }

        return new RvSpriteWrapper(position, velocity, shape.wrap(), animationWrappers, layerNumber, visible);
    }


    //to do - make an abstract handler class.
    //Then keep a vector of handlers on the class...
    public override void registerHandlers()
    {
        RvDrawableHandler.the().addDrawable(this);
    }
    public override void disposeHandlers()
    {
        RvDrawableHandler.the().removeDrawable(this);
    }

    public void setCurrentAnimation(int animationId)
    {
        for (int i=0; i<animations.Count; i++)
        {
            if (animations[i].getId() == animationId)
            {
                currentAnimation = i;
                animations[i].reset();
                return;
            }
        }
        //couldn't find the animation. Don't do anything.
    }
    public int getCurrentAnimationId()
    {
        return animations[currentAnimation].getId();
    }

    public void nextAnimation()
    {
        currentAnimation = (currentAnimation + 1)%animations.Count;
    }

    public override void update(GameTime gameTime)
    {
        base.update(gameTime);
        animations[currentAnimation].update(gameTime);
    }

    public void draw()
    {
        if (!visible)
        {
            return;
        }
        animations[currentAnimation].draw(shape.getRectangle(), getDrawingLayer());
    }


    public void setLayerNumber(int layerNumber)
    {
        this.layerNumber = layerNumber;
    }
    public int getLayerNumber()
    {
        return layerNumber;
    }

    public float getDrawingLayer()
    {
        return 0.5f + layerNumber/100.0f;
    }

    public void setVisible(bool visible)
    {
        this.visible = visible;
    }
}

public class RvSpriteWrapper : RvAbstractGameObjectWrapper
{
    [JsonProperty] public  List<RvAnimationWrapper> animationWrappers;
    [JsonProperty] public bool visible = true;
    [JsonProperty] public int layerNumber = RvSprite.SPRITE_LAYER_MID;

    public RvSpriteWrapper(Vector2 position, Vector2 velocity, RvAbstractShapeWrapper shape, List<RvAnimationWrapper> animations, int layerNumber = RvSprite.SPRITE_LAYER_MID, bool visible = true) 
      : base(position, velocity, shape)
    {
        this.animationWrappers = animations;
        this.visible = visible;
        this.layerNumber = layerNumber;
    }

    public override RvSprite unWrap()
    {
        List<RvAnimation> animations = new List<RvAnimation>();
        for (int i=0; i<animationWrappers.Count; i++)
        {
            animations.Add(animationWrappers[i].unWrap());
        }
        return new RvSprite(position, velocity, shapeWrapper.unWrap(), animations, layerNumber, visible);
    }
}
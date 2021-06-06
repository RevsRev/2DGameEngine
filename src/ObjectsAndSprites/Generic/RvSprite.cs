using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Newtonsoft.Json;

public class RvSprite : RvAbstractGameObject, RvDrawableI
{
    protected List<RvAnimation> animations;
    int currentAnimation = 0;

    protected bool visible = true;
    protected float layer = RvSpriteBatch.DEFAULT_DRAWING_LAYER;

    public RvSprite(Vector2 position, RvAbstractShape shape, List<RvAnimation> animations, float layer = 0.0f, bool visible = true)
      : this(position, Vector2.Zero, shape, animations, layer, visible)
      {

      }
    public RvSprite(Vector2 position, Vector2 velocity, RvAbstractShape shape, List<RvAnimation> animations, float layer = 0.0f, bool visible = true) : base(position, velocity, shape)
    {
        this.animations = animations;
        this.layer = layer;
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

        return new RvSpriteWrapper(position, velocity, shape.wrap(), animationWrappers, layer, visible);
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
        animations[currentAnimation].draw(shape.getRectangle(), layer);
    }


    public void setLayer(float layer)
    {
        this.layer = layer;
    }
    public float getLayer()
    {
        return layer;
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
    [JsonProperty] public float layer = 0.0f;

    public RvSpriteWrapper(Vector2 position, Vector2 velocity, RvAbstractShapeWrapper shape, List<RvAnimationWrapper> animations, float layer = 0.0f, bool visible = true) 
      : base(position, velocity, shape)
    {
        this.animationWrappers = animations;
        this.visible = visible;
        this.layer = layer;
    }

    public override RvSprite unWrap()
    {
        List<RvAnimation> animations = new List<RvAnimation>();
        for (int i=0; i<animationWrappers.Count; i++)
        {
            animations.Add(animationWrappers[i].unWrap());
        }
        return new RvSprite(position, velocity, shapeWrapper.unWrap(), animations, layer, visible);
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Newtonsoft.Json;

public class RvDrawableObject : RvAbstractWrappable
{
    protected List<RvAnimation> animations;
    int currentAnimation = 0;

    private bool visible = true;
    private float layer = RvSpriteBatch.DEFAULT_DRAWING_LAYER;

    public RvDrawableObject(List<RvAnimation> animations, float layer = 0.0f, bool visible = true)
    {
        this.animations = animations;
        this.layer = layer;
        this.visible = visible;
    }

    public override RvDrawableObjectWrapper wrap()
    {
        List<RvAnimationWrapper> animationWrappers = new List<RvAnimationWrapper>();
        for (int i=0; i<animations.Count; i++)
        {
            RvAnimation animation = animations[i];
            animationWrappers.Add(animation.wrap());
        }

        return new RvDrawableObjectWrapper(animationWrappers, layer, visible);
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

    public virtual void update(GameTime gameTime)
    {
        animations[currentAnimation].update(gameTime);
    }

    public virtual void Draw(Rectangle destinationRectangle)
    {
        if (!visible)
        {
            return;
        }
        animations[currentAnimation].Draw(destinationRectangle, layer);
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

public class RvDrawableObjectWrapper : RvAbstractWrapper
{
    [JsonProperty] public  List<RvAnimationWrapper> animationWrappers;

    [JsonProperty] public bool visible = true;
    [JsonProperty] public float layer = 0.0f;

    public RvDrawableObjectWrapper(List<RvAnimationWrapper> animations, float layer = 0.0f, bool visible = true)
    {
        this.animationWrappers = animations;
        this.visible = visible;
        this.layer = layer;
    }

    public override RvDrawableObject unWrap()
    {
        List<RvAnimation> animations = new List<RvAnimation>();
        for (int i=0; i<animationWrappers.Count; i++)
        {
            animations.Add(animationWrappers[i].unWrap());
        }
        return new RvDrawableObject(animations, layer, visible);
    }
}
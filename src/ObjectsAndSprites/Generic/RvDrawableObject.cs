using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Newtonsoft.Json;

public class RvDrawableObject
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

    public static RvDrawableObject factory(Game game, RvDrawableObjectWrapper w)
    {
        List<RvAnimation> rvAnims = new List<RvAnimation>();
        for (int i=0; i<w.animations.Count; i++)
        {
            RvAnimation animation = RvAnimation.factory(game, w.animations[i]);
            rvAnims.Add(animation);
        }
        return new RvDrawableObject(rvAnims, w.layer, w.visible);
    }

    public RvDrawableObjectWrapper createWrapper()
    {
        List<RvAnimationWrapper> animationWrappers = new List<RvAnimationWrapper>();
        for (int i=0; i<animations.Count; i++)
        {
            RvAnimation animation = animations[i];
            animationWrappers.Add(animation.createWrapper());
        }

        return new RvDrawableObjectWrapper(animationWrappers, visible, layer);
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

    public virtual void Update(GameTime gameTime)
    {
        animations[currentAnimation].Update(gameTime);
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

public class RvDrawableObjectWrapper
{
    [JsonProperty] public  List<RvAnimationWrapper> animations;

    [JsonProperty] public bool visible = true;
    [JsonProperty] public float layer = 0.0f;

    public RvDrawableObjectWrapper(List<RvAnimationWrapper> animations, bool visible, float layer)
    {
        this.animations = animations;
        this.visible = visible;
        this.layer = layer;
    }
}
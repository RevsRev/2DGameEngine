using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Newtonsoft.Json;

public class RvScenery : RvDrawableObject
{
    public static readonly Single SCENERY_LAYER_DEPTH = 1.0f;

    private Vector2 position;
    private int width;
    private int height;

    public RvScenery(List<RvAnimation> animations, Vector2 position, int width, int height) : base(animations)
    {
        this.position = position;
        this.width = width;
        this.height = height;

        setLayer(SCENERY_LAYER_DEPTH);
    }

    public static RvScenery factory(Game game, string textureName, Vector2 position, int width, int height, int animationId)
    {
        List<RvAnimation> animations = new List<RvAnimation>{RvAnimation.factory(game, textureName, 1, 1, animationId)};
        return new RvScenery(animations, position, width, height);
    }

    public static RvScenery factory(Game game, RvSceneryWrapper w)
    {
        List<RvAnimationWrapper> animationWrappers = w.animationWrappers;
        List<RvAnimation> animations = new List<RvAnimation>();

        for (int i=0; i<animationWrappers.Count; i++)
        {
            animations.Add(RvAnimation.factory(game, animationWrappers[i]));
        }
        return new RvScenery(animations, w.position, w.width, w.height);
    }

    new public RvSceneryWrapper createWrapper()
    {
        List<RvAnimationWrapper> animationWrappers = new List<RvAnimationWrapper>();
        for (int i=0; i<animations.Count; i++)
        {
            RvAnimation animation = animations[i];
            animationWrappers.Add(animation.createWrapper());
        }
        return new RvSceneryWrapper(animationWrappers, position, width, height);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public void Draw(RvSpriteBatch spriteBatch)
    {
        Rectangle pallet = new Rectangle((int)(position.X), (int)(position.Y), width, height);
        base.Draw(spriteBatch, pallet);
    }
}

public class RvSceneryWrapper
{
    [JsonProperty] public List<RvAnimationWrapper> animationWrappers;

    [JsonProperty] public Vector2 position;
    [JsonProperty] public int width;
    [JsonProperty] public int height;

    public RvSceneryWrapper(List<RvAnimationWrapper> animationWrappers, Vector2 position, int width, int height)
    {
        this.animationWrappers = animationWrappers;
        this.position = position;
        this.width = width;
        this.height= height;
    }
}
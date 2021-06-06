/*
Removed 06/06/2021 as part of sprite/object rewrite.
*/

// using System.Collections.Generic;
// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using System;
// using Newtonsoft.Json;

// public class RvScenery : RvDrawableObject
// {
//     public static readonly Single SCENERY_LAYER_DEPTH = 1.0f;

//     private Vector2 position;
//     private int width;
//     private int height;

//     public RvScenery(List<RvAnimation> animations, Vector2 position, int width, int height) : base(animations)
//     {
//         this.position = position;
//         this.width = width;
//         this.height = height;

//         setLayer(SCENERY_LAYER_DEPTH);
//     }

//     public static RvScenery factory(Game game, string textureName, Vector2 position, int width, int height, int animationId)
//     {
//         List<RvAnimation> animations = new List<RvAnimation>{RvAnimation.factory(textureName, 1, 1, animationId)};
//         return new RvScenery(animations, position, width, height);
//     }

//     public override RvSceneryWrapper wrap()
//     {
//         List<RvAnimationWrapper> animationWrappers = new List<RvAnimationWrapper>();
//         for (int i=0; i<animations.Count; i++)
//         {
//             animationWrappers.Add(animations[i].wrap());
//         }
//         return new RvSceneryWrapper(animationWrappers, position, width, height);
//     }

//     public override void update(GameTime gameTime)
//     {
//         base.update(gameTime);
//     }

//     public void Draw()
//     {
//         Rectangle pallet = new Rectangle((int)(position.X), (int)(position.Y), width, height);
//         base.Draw(pallet);
//     }
// }

// public class RvSceneryWrapper : RvSpriteWrapper
// {
//     [JsonProperty] public Vector2 position;
//     [JsonProperty] public int width;
//     [JsonProperty] public int height;

//     public RvSceneryWrapper(List<RvAnimationWrapper> animationWrappers, Vector2 position, int width, int height) : base(animationWrappers)
//     {
//         this.position = position;
//         this.width = width;
//         this.height= height;
//     }

//     public override RvScenery unWrap()
//     {
//         List<RvAnimation> animations = new List<RvAnimation>();
//         for (int i=0; i<animationWrappers.Count; i++)
//         {
//             animations.Add(animationWrappers[i].unWrap());
//         }
//         return new RvScenery(animations, position, width, height);
//     }
// }
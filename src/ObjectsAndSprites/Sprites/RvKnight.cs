using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Shapes;

public class RvKnight : RvCollidableSprite
{
    public static readonly int ANIMATION_ID_KNIGHT_IDLE         = 0;
    public static readonly int ANIMATION_ID_KNIGHT_RUN          = 1;
    public static readonly int ANIMATION_ID_KNIGHT_JAB          = 2;
    public static readonly int ANIMATION_ID_KNIGHT_SWIPE        = 3;
    public static readonly int ANIMATION_ID_KNIGHT_SMASH        = 4;
    public static readonly int ANIMATION_ID_KNIGHT_JUMP         = 5;
    public static readonly int ANIMATION_ID_KNIGHT_LANDING      = 6;
    public static readonly int ANIMATION_ID_KNIGHT_FALLING      = 7;
    public static readonly int ANIMATION_ID_KNIGHT_RISING       = 8;

    public static readonly float RUNNING_FORCE                           = 100.0f;
    public static readonly float COEFFICIENT_OF_DRAG_SECOND_ORDER        = 0.0001f;
    public static readonly float COEFFICIENT_OF_DRAG_FIRST_ORDER         = 0.0f;

    //private bool facingRight = true; //to do.

    public RvKnight(Vector2 position) : this(position, new Shapes.RvHorizontalRectangle(position, 50, 75))
    {

    }

    public RvKnight(Vector2 position, RvAbstractShape shape) : this(position, Vector2.Zero, shape, getKnightAnimations())
    {
    }

    public RvKnight(Vector2 position, Vector2 velocity, RvAbstractShape shape, List<RvAnimation> animations, int layerNumber = RvSprite.SPRITE_LAYER_MID, bool visible = true)
      : base(position, velocity, shape, animations, layerNumber, visible)
    {

    }

    public override RvKnightWrapper wrap()
    {
        //haven't handled null sprite here. Not sure if it'll be a problem or not...
        return new RvKnightWrapper(position, velocity, shape.wrap(), RvWrapperUtils.wrapVector<RvAnimation, RvAnimationWrapper>(animations), layerNumber, visible);
    }

    public static List<RvAnimation> getKnightAnimations()
    {
        List<RvAnimation> animations = new List<RvAnimation>();

        animations.Add(RvAnimation.factory(RvContentFiles.KNIGHT + "noBKG_KnightRun_strip", 1, 8, ANIMATION_ID_KNIGHT_RUN, true, new Rectangle(0,25, 50, 75)));
        animations.Add(RvAnimation.factory(RvContentFiles.KNIGHT + "noBKG_KnightIdle_strip", 1, 15, ANIMATION_ID_KNIGHT_IDLE, true, new Rectangle(0, 25, 80, 75)));
        animations.Add(RvAnimation.factory(RvContentFiles.KNIGHT + "noBKG_KnightJab_strip", 1, 4, ANIMATION_ID_KNIGHT_JAB, true, new Rectangle(0,25, 50, 75)));
        animations.Add(RvAnimation.factory(RvContentFiles.KNIGHT + "noBKG_KnightSwipe_strip", 1, 5, ANIMATION_ID_KNIGHT_SWIPE, true, new Rectangle(20, 25, 40, 75)));
        animations.Add(RvAnimation.factory(RvContentFiles.KNIGHT + "noBKG_KnightSmash_strip", 1, 6, ANIMATION_ID_KNIGHT_SMASH, true, new Rectangle(17, 25, 33, 75)));
        animations.Add(RvAnimation.factory(RvContentFiles.KNIGHT + "noBKG_KnightJump_strip", 1, 2, ANIMATION_ID_KNIGHT_JUMP, true, new Rectangle(0, 25, 100, 75)));
        animations.Add(RvAnimation.factory(RvContentFiles.KNIGHT + "noBKG_KnightLanding_strip", 1, 3, ANIMATION_ID_KNIGHT_LANDING, true, new Rectangle(25, 0, 50, 100)));
        animations.Add(RvAnimation.factory(RvContentFiles.KNIGHT + "noBKG_KnightFalling_strip", 1, 1, ANIMATION_ID_KNIGHT_FALLING));
        animations.Add(RvAnimation.factory(RvContentFiles.KNIGHT + "noBKG_KnightRising_strip", 1, 1, ANIMATION_ID_KNIGHT_RISING, true, new Rectangle(0, 25, 50, 75)));

        return animations;
    }

    public override void update(GameTime gameTime)
    {
        // doControlling(gameTime);
        // selectAnimation();
        base.update(gameTime);
    }

    // public override Vector2 getForceDueToFriction()
    // {
    //     Vector2 velocity = getVelocity();
    //     float lengthSquared = velocity.LengthSquared();

    //     if (getIsDoingACollision())
    //     {
    //         float coefficientOfFriction = 1.0f; //to do - make depend on materials, etc.
    //         if (velocity.Length() > EPSILON)
    //         {
    //             Vector2 normVel = Vector2.Normalize(velocity);
    //             return -coefficientOfFriction * getMass() * LinearAlgebra.absDet(normVel, RvPhysics.GRAVITY) * normVel;
    //         }
    //     }

    //     if (lengthSquared > EPSILON)
    //     {
    //         return -Vector2.Multiply(Vector2.Normalize(velocity), velocity.LengthSquared()*COEFFICIENT_OF_DRAG_SECOND_ORDER)
    //                 ;//-Vector2.Multiply(Vector2.Normalize(velocity), velocity.Length()*COEFFICIENT_OF_DRAG_FIRST_ORDER);
    //     }
    //     return Vector2.Zero;
    // }

    // public void doControlling(GameTime gameTime)
    // {
    //     var kState = Keyboard.GetState();
        
    //     if (kState.IsKeyDown(Keys.D))
    //     {
    //         updateKinematics(gameTime, new Vector2(RUNNING_FORCE, 0));
    //         if (!isAirborne() && getSprite().getCurrentAnimationId() != ANIMATION_ID_KNIGHT_RUN)
    //         {
    //             getSprite().setCurrentAnimation(ANIMATION_ID_KNIGHT_RUN);
    //         }
    //     }
        
    //     if (kState.IsKeyDown(Keys.A))
    //     {
    //         updateKinematics(gameTime, new Vector2(-RUNNING_FORCE, 0));
    //         if (!isAirborne() && getSprite().getCurrentAnimationId() != ANIMATION_ID_KNIGHT_RUN)
    //         {
    //             getSprite().setCurrentAnimation(ANIMATION_ID_KNIGHT_RUN);
    //         }
    //     }

    //     if (kState.IsKeyDown(Keys.Space))
    //     {
    //         if (doJump(gameTime))
    //         {
    //             getSprite().setCurrentAnimation(ANIMATION_ID_KNIGHT_JUMP);
    //         }
    //     }
    // }

    // public void selectAnimation()
    // {
    //     if (isFalling())
    //     {
    //         getSprite().setCurrentAnimation(ANIMATION_ID_KNIGHT_FALLING);
    //     }
    //     if (isRising())
    //     {
    //         getSprite().setCurrentAnimation(ANIMATION_ID_KNIGHT_RISING);
    //     }
    //     if (!isAirborne() && getVelocity().Length() < MINIMUM_HORIZONTAL_VELOCITY)
    //     {
    //         getSprite().setCurrentAnimation(ANIMATION_ID_KNIGHT_IDLE);
    //     }
    // }
}

public class RvKnightWrapper : RvCollidableSriteWrapper
{
    public RvKnightWrapper(Vector2 position, Vector2 velocity, RvAbstractShapeWrapper shape, List<RvAnimationWrapper> animations, int layerNumber = RvSprite.SPRITE_LAYER_MID, bool visible = true) 
      : base(position, velocity, shape, animations, layerNumber, visible)
    {

    }

    public override RvKnight unWrap()
    {
        return new RvKnight(position, velocity, shapeWrapper.unWrap(), RvWrapperUtils.unwrapVector<RvAnimation, RvAnimationWrapper>(animationWrappers), layerNumber, visible);
    }
}
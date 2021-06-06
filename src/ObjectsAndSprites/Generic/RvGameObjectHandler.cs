using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

public class RvGameObjectHandler : RvAbstractWrappable, RvUpdatableI
{
    private List<RvAbstractGameObject> objects; //probably should live somewhere else, but this will do for now!

    public RvGameObjectHandler(List<RvAbstractGameObject>objects)
    {
        this.objects = objects;
    }
    public static RvGameObjectHandler factory()
    {
        return new RvGameObjectHandler(new List<RvAbstractGameObject>());
    }

    public override RvGameObjectHandlerWrapper wrap()
    {
        List<RvAbstractGameObjectWrapper> objectWrappers = new List<RvAbstractGameObjectWrapper>();
        for (int i=0; i<objects.Count; i++)
        {
            objectWrappers.Add(objects[i].wrap());
        }
        return new RvGameObjectHandlerWrapper(objectWrappers);
    }

    public void update(GameTime gameTime)
    {
        updateObjects(gameTime);
        // doPhysics(gameTime);
    }

    //06/06/2021 Handle drawing in the drawable handler now.
    // public void Draw()
    // {
    //     for (int i=0; i<objects.Count; i++)
    //     {
    //         objects[i].draw();
    //     }
    // }

    //Things like this will be handled in a collision handler.
    // public void doPhysics(GameTime gameTime)
    // {
    //     //Do stuff that doesn't require interactions.
    //     for (int i=0; i<objects.Count; i++)
    //     {
    //         objects[i].accelerate(gameTime, RvPhysics.GRAVITY);
    //         objects[i].setIsDoingACollision(false);
    //     }

    //     //Now handle collisions. This logic should make sure that objects can't pass through eachother.
    //     for (int i=0; i<objects.Count; i++)
    //     {
    //         RvAbstractGameObject objOne = objects[i];

    //         for (int j=i+1; j<objects.Count; j++)
    //         {
    //             RvAbstractGameObject objTwo = objects[j];

    //             if (objOne.collidesWith(objTwo))
    //             {
    //                 Vector2 ObjOneRestoringDir = objOne.getRestoringDirection(objTwo);
    //                 Vector2 ObjTwoRestoringDir = objTwo.getRestoringDirection(objOne);

    //                 objOne.setIsDoingACollision(true);
    //                 objTwo.setIsDoingACollision(true);

    //                 objOne.move(ObjOneRestoringDir);
    //                 objTwo.move(ObjTwoRestoringDir);

    //                 if (!objOne.getImmovable())
    //                 {
    //                     objOne.setVelocity(LinearAlgebra.project(objOne.getVelocity(), ObjOneRestoringDir));
    //                 }
    //                 if (!objTwo.getImmovable())
    //                 {
    //                     objTwo.setVelocity(LinearAlgebra.project(objTwo.getVelocity(), ObjTwoRestoringDir));
    //                 }
    //             }
    //         }
    //     }
    // }

    private void updateObjects(GameTime gameTime)
    {
        for (int i=0; i<objects.Count; i++)
        {
            objects[i].update(gameTime);
        }
    }

    public List<RvAbstractGameObject> getObjectsAt(Vector2 point)
    {
        List<RvAbstractGameObject> retval = new List<RvAbstractGameObject>();

        for (int i=0; i<objects.Count; i++)
        {
            if (objects[i].getShape().isInterior(point))
            {
                retval.Add(objects[i]);
            }
        }
        return retval;
    }

    public void addObject(RvAbstractGameObject obj)
    {
        objects.Add(obj);
    }
    public void removeObjectAt(Vector2 position)
    {
        List<RvAbstractGameObject> objectsAtPos = getObjectsAt(position);
        if (objectsAtPos.Count > 0)
        {
            RvAbstractGameObject obj = objectsAtPos[0];
            objects.Remove(obj);
        }
    }
}

public class RvGameObjectHandlerWrapper : RvAbstractWrapper
{
    [JsonProperty] public List<RvAbstractGameObjectWrapper> objectWrappers;

    public RvGameObjectHandlerWrapper(List<RvAbstractGameObjectWrapper> objects)
    {
        this.objectWrappers = objects;
    }

    public override RvGameObjectHandler unWrap()
    {
        return new RvGameObjectHandler(RvWrapperUtils.unwrapVector<RvAbstractGameObject, RvAbstractGameObjectWrapper>(objectWrappers));
    }
}
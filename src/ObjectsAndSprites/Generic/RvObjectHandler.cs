using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

public class RvObjectHandler
{
    private List<RvPhysicalObject> objects; //probably should live somewhere else, but this will do for now!

    public RvObjectHandler(List<RvPhysicalObject>objects)
    {
        this.objects = objects;
    }

    public static RvObjectHandler factory(Game game, RvObjectHandlerWrapper w)
    {
        List<RvPhysicalObjectWrapper> objectWrappers = w.objects;
        List<RvPhysicalObject> objects = new List<RvPhysicalObject>();
        for (int i=0; i<objectWrappers.Count; i++)
        {
            objects.Add(RvPhysicalObject.factory(game, objectWrappers[i]));
        }
        return new RvObjectHandler(objects);
    }

    public RvObjectHandlerWrapper createWrapper()
    {
        List<RvPhysicalObjectWrapper> objectWrappers = new List<RvPhysicalObjectWrapper>();
        for (int i=0; i<objects.Count; i++)
        {
            objectWrappers.Add(objects[i].createWrapper());
        }
        return new RvObjectHandlerWrapper(objectWrappers);
    }

    public void Update(GameTime gameTime)
    {
        updateObjects(gameTime);
        doPhysics(gameTime);
    }

    public void Draw()
    {
        for (int i=0; i<objects.Count; i++)
        {
            objects[i].draw();
        }
    }

    public void doPhysics(GameTime gameTime)
    {
        //Do stuff that doesn't require interactions.
        for (int i=0; i<objects.Count; i++)
        {
            objects[i].accelerate(gameTime, RvPhysics.GRAVITY);
            objects[i].setIsDoingACollision(false);
        }

        //Now handle collisions. This logic should make sure that objects can't pass through eachother.
        for (int i=0; i<objects.Count; i++)
        {
            RvPhysicalObject objOne = objects[i];

            for (int j=i+1; j<objects.Count; j++)
            {
                RvPhysicalObject objTwo = objects[j];

                if (objOne.collidesWith(objTwo))
                {
                    Vector2 ObjOneRestoringDir = objOne.getRestoringDirection(objTwo);
                    Vector2 ObjTwoRestoringDir = objTwo.getRestoringDirection(objOne);

                    objOne.setIsDoingACollision(true);
                    objTwo.setIsDoingACollision(true);

                    objOne.move(ObjOneRestoringDir);
                    objTwo.move(ObjTwoRestoringDir);

                    if (!objOne.getImmovable())
                    {
                        objOne.setVelocity(LinearAlgebra.project(objOne.getVelocity(), ObjOneRestoringDir));
                    }
                    if (!objTwo.getImmovable())
                    {
                        objTwo.setVelocity(LinearAlgebra.project(objTwo.getVelocity(), ObjTwoRestoringDir));
                    }
                }
            }
        }
    }

    private void updateObjects(GameTime gameTime)
    {
        for (int i=0; i<objects.Count; i++)
        {
            objects[i].Update(gameTime);
        }
    }

    public List<RvPhysicalObject> getObjectsAt(Vector2 point)
    {
        List<RvPhysicalObject> retval = new List<RvPhysicalObject>();

        for (int i=0; i<objects.Count; i++)
        {
            if (objects[i].getHitbox().isInterior(point))
            {
                retval.Add(objects[i]);
            }
        }
        return retval;
    }

    public void addObject(RvPhysicalObject physicalObject)
    {
        objects.Add(physicalObject);
    }
    public void removeObjectAt(Vector2 position)
    {
        List<RvPhysicalObject> objectsAtPos = getObjectsAt(position);
        if (objectsAtPos.Count > 0)
        {
            RvPhysicalObject obj = objectsAtPos[0];
            objects.Remove(obj);
        }
    }
}

public class RvObjectHandlerWrapper
{
    [JsonProperty] public List<RvPhysicalObjectWrapper> objects;

    public RvObjectHandlerWrapper(List<RvPhysicalObjectWrapper> objects)
    {
        this.objects = objects;
    }
}
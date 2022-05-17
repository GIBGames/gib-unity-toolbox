using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* List Extensions
Toast's useful Unity Scripts
https://github.com/dorktoast/ToastsUsefulUnityScripts
Released under MIT
*/
public static class ToastsTransformExtensions
{
    // These methods allow you to write to the transform.position.x/y/z property.

    /// <summary>
    /// Sets the transform position x property.
    /// </summary>
    public static Vector3 ChangeX(this Transform transform, float x)
    {
        Vector3 position = transform.position;
        position.x = x;
        transform.position = position;
        return position;
    }

    /// <summary>
    /// Sets the transform position y property.
    /// </summary>
    public static Vector3 ChangeY(this Transform transform, float y)
    {
        Vector3 position = transform.position;
        position.y = y;
        transform.position = position;
        return position;
    }

    /// <summary>
    /// Sets the transform position z property.
    /// </summary>
    public static Vector3 ChangeZ(this Transform transform, float z)
    {
        Vector3 position = transform.position;
        position.z = z;
        transform.position = position;
        return position;
    }

    // These extensions allow additional data from Vector3s.

    /// <summary>
    /// Returns a new Vector3 that ignores the Y axis.
    /// </summary>
    public static Vector3 Flatten(this Vector3 vector)
    {
        return new Vector3(vector.x, 0f, vector.z);
    }

    /// <summary>
    /// Returns Vector3 Distance that ignores the Y axis.
    /// </summary>

    public static float FlatDistance(this Vector3 origin, Vector3 destination)
    {
        return Vector3.Distance(origin.Flatten(), destination.Flatten());
    }

    /// <summary>
    /// Returns the rotation to look at a Vector3
    /// </summary>
    public static Quaternion LookAtRotation(this Transform self, Vector3 target)
    {
        return Quaternion.LookRotation(target - self.position);
    }

    /// <summary>
    /// Returns the rotation to look at a Transform
    /// </summary>
    public static Quaternion LookAtRotation(this Transform self, Transform target)
    {
        return LookAtRotation(self, target.position);
    }

    /// <summary>
    /// Returns the rotation to look at a GameObject
    /// </summary>
    public static Quaternion LookAtRotation(this Transform self, GameObject target)
    {
        return LookAtRotation(self, target.transform.position);
    }

}

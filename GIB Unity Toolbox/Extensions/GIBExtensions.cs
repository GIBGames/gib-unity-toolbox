using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///     Useful unity script extensions.
/// </summary>
public static class GIBExtensions
{
    #region Camera Extensions

    /// <summary>
    /// Check if the target Renderer is visible.
    /// </summary>
    /// <param name="renderer">Target renderer.</param>
    /// <returns></returns>
    public static bool IsObjectVisible(this Camera @this, Renderer renderer) =>
        GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(@this), renderer.bounds);

    #endregion

    #region Transform Extensions

    /// <summary>
    ///     Sets the transform position x property.
    /// </summary>
    public static Vector3 SetX(this Transform transform, float x)
    {
        Vector3 position = transform.position;
        position.x = x;
        transform.position = position;
        return position;
    }

    /// <summary>
    ///     Sets the transform position y property.
    /// </summary>
    public static Vector3 SetY(this Transform transform, float y)
    {
        Vector3 position = transform.position;
        position.y = y;
        transform.position = position;
        return position;
    }

    /// <summary>
    ///     Sets the transform position z property.
    /// </summary>
    public static Vector3 SetZ(this Transform transform, float z)
    {
        Vector3 position = transform.position;
        position.z = z;
        transform.position = position;
        return position;
    }

    // These extensions allow additional data from Vector3s.

    /// <summary>
    ///     Returns a new Vector3 that ignores the Y axis.
    /// </summary>
    public static Vector3 Flatten(this Vector3 vector) =>
        new Vector3(vector.x, 0f, vector.z);

    /// <summary>
    ///     Returns Vector3 Distance that ignores the Y axis.
    /// </summary>
    public static float FlatDistance(this Vector3 origin, Vector3 destination) =>
        Vector3.Distance(origin.Flatten(), destination.Flatten());

    /// <summary>
    ///     Get a Vector3 which is the target distance along the Forward axis from the transform.
    /// </summary>
    public static Vector3 ForwardPoint(this Transform origin, float distance) =>
        origin.position + (origin.forward * distance);

    /// <summary>
    ///     Get the rotation to look at a Vector3.
    /// </summary>
    /// <returns>A <see cref="Quaternion" /> representing the rotation to look at <paramref name="target" />.</returns>
    public static Quaternion LookAtRotation(this Transform self, Vector3 target) =>
        Quaternion.LookRotation(target - self.position);

    /// <summary>
    ///     Get the rotation to look at a Transform.
    /// </summary>
    /// <returns>A <see cref="Quaternion" /> representing the rotation to look at <paramref name="target" />.</returns>
    public static Quaternion LookAtRotation(this Transform self, Transform target) =>
        LookAtRotation(self, target.position);

    /// <summary>
    ///     Get the rotation to look at a GameObject.
    /// </summary>
    /// <returns>A <see cref="Quaternion" /> representing the rotation to look at <paramref name="target" />.</returns>
    public static Quaternion LookAtRotation(this Transform self, GameObject target) =>
        LookAtRotation(self, target.transform.position);

    /// <summary>
    /// Returns a location around a target point.
    /// </summary>
    /// <param name="point">Target point.</param>
    /// <param name="pivot">Amount of pivot over time.</param>
    /// <param name="angle">Target angle.</param>
    /// <returns></returns>
    public static Vector3 RotatePivotPoint(Vector3 point, Vector3 pivot, Quaternion angle)
    {
        return angle * (point - pivot) + pivot;
    }

    #endregion

    #region Unity Extensions
    /// <summary>
    /// Checks if a GameObject has a MonoBehaviour.
    /// </summary>
    /// <typeparam name="T">A MonoBehaviour.</typeparam>
    /// <param name="gameObject">Target GameObject.</param>
    public static bool HasComponent<T>(this GameObject gameObject) where T : MonoBehaviour =>
        gameObject.GetComponent<T>() != null;

    /// <summary>
    /// Change a Rigidbody's direction without altering its velocity.
    /// </summary>
    /// <param name="direction">Target direction.</param>
    public static void SetDirection(this Rigidbody rb, Vector3 direction)
    {
        rb.velocity = direction.normalized * rb.velocity.magnitude;
    }

    /// <summary>
    /// Checks if a LayerMask contains a specific layer.
    /// </summary>
    /// <param name="layerNumber">Target mask layer.</param>
    public static bool Contains(this LayerMask mask, int layerNumber) =>
        mask == (mask | (1 << layerNumber));

    #endregion

    #region List Extensions

    /// <summary>
    ///     Add multiple values to a List at once.
    /// </summary>
    /// <example>
    /// Example usage:
    /// <code>SomeList.AddMulti("a", "z");</code>
    /// </example>
    public static void AddMulti<T>(this List<T> list, params T[] elements) =>
        list.AddRange(elements);

    /// <summary>
    ///     Get list containing all of the children of the called GameObject.
    /// </summary>
    /// <example>
    /// Example usage:
    /// <code>List<GameObject> targetChildren = targetGameObject.GetChildren();</code>
    /// </example>
    /// <param name="go">Target <see cref="GameObject" />.</param>
    public static List<GameObject> GetChildren(this GameObject go) =>
        (from Transform tran in go.transform select tran.gameObject).ToList();



    #endregion

    #region String Extensions

    /// <summary>
    /// Returns the left part of this string instance.
    /// </summary>
    /// <param name="chars">Number of characters to return.</param>
    public static string Left(this string input, int chars)
    {
        return input.Substring(0, Math.Min(input.Length, chars));
    }

    /// <summary>
    /// Returns the right part of the string instance.
    /// </summary>
    /// <param name="chars">Number of characters to return.</param>
    public static string Right(this string input, int chars)
    {
        return input.Substring(Math.Max(input.Length - chars, 0), Math.Min(chars, input.Length));
    }

    #endregion
}
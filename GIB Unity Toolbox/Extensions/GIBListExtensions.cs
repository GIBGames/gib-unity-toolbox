using System;
using System.Collections.Generic;
using UnityEngine;

/* List Extensions
GIB Games Unity Toolbox
https://github.com/GIBGames/gib-unity-toolbox
Released under MIT
*/

public static class GIBListExtensions
{
    /// <summary>
    /// Add multiple values to a List at once. Format: list.AddMulti("a", "z");
    /// </summary>
    public static void AddMulti<T>(this List<T> list, params T[] elements)
    {
        list.AddRange(elements);
    }

    /// <summary>
    /// Returns a list of GameObjects containing all of the children of the called GameObject.
    /// </summary>
    public static List<GameObject> GetChildren(this GameObject go)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform tran in go.transform)
        {
            children.Add(tran.gameObject);
        }
        return children;
    }

    /// <summary>
    /// Shuffle the list in place using the Fisher-Yates method.
    /// </summary>
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    /// <summary>
    /// Return a random item from the list.
    /// </summary>
    public static T RandomItem<T>(this IList<T> list)
    {
        if (list.Count == 0)
            throw new IndexOutOfRangeException("Cannot select a random item from an empty list");
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
}
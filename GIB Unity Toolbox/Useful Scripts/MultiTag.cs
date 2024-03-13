/**
 * Copyright 2024 Sam Swicegood
 * MultiTag.cs
 * Created by: Toast <sam@gib.games>
 * Created on: 3/11/2023
 * Licensable under MIT
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains an array of strings that allow for an object to have more than one tag.
/// </summary>
public class MultiTag : MonoBehaviour, ISerializationCallbackReceiver
{
    // A HashSet is used on the backend to improve lookup performance.
    private HashSet<string> tags = new HashSet<string>();

    // This list will only be used for serialization purposes.
    [SerializeField] private List<string> tagsList = new List<string>();

    private void OnEnable()
    {
        MultiTagManager.RegisterMultiTag(this);
    }

    private void OnDisable()
    {
        MultiTagManager.UnregisterMultiTag(this);
    }

    public void AddTag(string tag)
    {
        tag = tag.Trim();
        if (!string.IsNullOrEmpty(tag) && !HasTag(tag))
        {
            tags.Add(tag);
            tagsList.Add(tag);
        }
    }

    public void RemoveTag(string tag)
    {
        if (HasTag(tag))
        {
            tags.Remove(tag);
            tagsList.Remove(tag);
        }
    }

    public bool HasTag(string tag) => tags.Contains(tag);
    public void OnAfterDeserialize()
    {
        // After deserialization, update the HashSet to reflect the serialized List.
        tags = new HashSet<string>(tagsList);
    }

    public void OnBeforeSerialize()
    {
        // Only necessary if you modify the HashSet during runtime and need to ensure
        // the List is synchronized before serialization.
        // This assumes 'tags' is your runtime-modifiable HashSet.
        if (tags.Count + 1 == tagsList.Count)
        {
            tagsList.RemoveAt(tagsList.Count-1);
            tagsList.Add("");
            return;
        }
        if (tags.Count != tagsList.Count)
        {
            tagsList.Clear();
            tagsList.AddRange(tags);
        }
    }

    public IEnumerable<string> GetCurrentTags()
    {
        return tags;
    }

    public static void RebuildMultiTags()
    {
        MultiTagManager.RebuildObjectList();
    }
}

/// <summary>
/// Utilities related to the <see cref="MultiTag"/> behavior.
/// </summary>
public static class MultiTagUtility
{
    /// <summary>
    /// Returns the first active GameObject in the scene with the corresponding tags, including <see cref="MultiTag"/> objects.
    /// Returns null if no matching objects are found.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="ignoreBuiltIn">Ignore the Built-in Unity tag field.</param>
    /// <remarks>If ignoreBuiltIn is false, this will check built-in tag first.</remarks>
    public static GameObject FindObjectWithTags(string tag, bool ignoreBuiltIn = false)
    {
        // Check for objects with built-in tag
        if (!ignoreBuiltIn)
        {
            GameObject result = GameObject.FindWithTag(tag);
            if (result != null) return result;
        }

        // Check for objects with MultiTag
        foreach (var multiTag in MultiTagManager.GetAllMultiTags())
        {
            if (multiTag.HasTag(tag))
                return multiTag.gameObject;
        }

        return null;
    }

    /// <summary>
    /// Returns all active GameObjects in the scene with the corresponding tags, including <see cref="MultiTag"/> objects.
    /// </summary>
    /// <returns>A GameObject array.</returns>
    /// <param name="tag"></param>
    /// <param name="ignoreBuiltIn">Ignore the Built-in Unity tag field.</param>
    /// <remarks>If ignoreBuiltIn is false, this will check built-in tag first.</remarks>
    public static GameObject[] FindObjectsWithTags(string tag, bool ignoreBuiltIn = false)
    {
        return (GameObject[])FindWithTags(tag, ignoreBuiltIn);
    }

    /// <summary>
    /// Returns all active GameObjects in the scene with the corresponding tags, including <see cref="MultiTag"/> objects.
    /// </summary>
    /// <returns>A GameObject <see cref="IEnumerable"/>.</returns>
    /// <param name="tag"></param>
    /// <param name="ignoreBuiltIn">Ignore the Built-in Unity tag field.</param>
    /// <remarks>If ignoreBuiltIn is false, this will check built-in tag first.</remarks>
    public static IEnumerable<GameObject> FindWithTags(string tag, bool ignoreBuiltIn = false)
    {
        var foundObjects = new List<GameObject>();
        if (!ignoreBuiltIn)
        {
            foundObjects.AddRange(GameObject.FindGameObjectsWithTag(tag));
        }

        // Include objects with MultiTag
        foreach (var multiTag in MultiTagManager.GetAllMultiTags())
        {
            if (multiTag.HasTag(tag) && (!ignoreBuiltIn || !foundObjects.Contains(multiTag.gameObject)))
            {
                foundObjects.Add(multiTag.gameObject);
            }
        }

        return foundObjects;
    }

    /// <summary>
    /// Checks if the GameObject is tagged with the target tag, including checking for a <see cref="MultiTag"/>.
    /// </summary>
    /// <param name="tag">Tags to check for.</param>
    /// <remarks>If ignoreBuiltIn is false, this will check built-in tag first.</remarks>
    public static bool CompareTags(this GameObject go, string tag, bool ignoreBuiltIn = false)
    {
        if (!ignoreBuiltIn)
        {
            if (go.CompareTag(tag)) return true;
        }

        if (go.TryGetComponent(out MultiTag multiTag))
        {
            return multiTag.HasTag(tag);
        }

        return false;
    }
}

/// <summary>
/// Manages registration and tracking of all MultiTag components in the scene. 
/// Provides methods to register, unregister, retrieve, and rebuild the list of MultiTag components.
/// </summary>
public static class MultiTagManager
{
    private static HashSet<MultiTag> allMultiTags = new HashSet<MultiTag>();
    private static HashSet<string> possibleTags = new HashSet<string> { "EditorOnly, MainCamera, Player"};

    /// <summary>
    /// Registers a MultiTag component. If the component is already registered, this method does nothing.
    /// </summary>
    /// <param name="multiTag">The MultiTag component to register.</param>
    public static void RegisterMultiTag(MultiTag multiTag)
    {
        if (!allMultiTags.Contains(multiTag))
        {
            allMultiTags.Add(multiTag);
        }
    }

    /// <summary>
    /// Unregisters a MultiTag component. If the component is not currently registered, this method does nothing.
    /// </summary>
    /// <param name="multiTag">The MultiTag component to unregister.</param>
    public static void UnregisterMultiTag(MultiTag multiTag)
    {
        allMultiTags.Remove(multiTag);
    }

    /// <summary>
    /// Retrieves all registered MultiTag components.
    /// </summary>
    /// <returns>An IEnumerable of all registered MultiTag components.</returns>
    public static IEnumerable<MultiTag> GetAllMultiTags()
    {
        return allMultiTags;
    }

    /// <summary>
    /// Rebuilds the list of registered MultiTag components, updating it to reflect the current state of the scene.
    /// This can be useful after significant changes to the scene's object hierarchy or after loading a new scene.
    /// </summary>
    public static void RebuildObjectList()
    {
        allMultiTags.Clear();
        allMultiTags = new HashSet<MultiTag>(Object.FindObjectsOfType<MultiTag>());
    }

    public static IEnumerable<string> GetPossibleTags()
    {
        possibleTags = new HashSet<string> { "EditorOnly, MainCamera, Player" };

        foreach(MultiTag multiTag in allMultiTags)
        {
            foreach(string s in multiTag.GetCurrentTags())
            {
                if(!possibleTags.Contains(s))
                {
                    possibleTags.Add(s);
                }
            }
        }

        return possibleTags;
    }
}

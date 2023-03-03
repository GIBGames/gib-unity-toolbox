using UnityEditor;

namespace GIB.EditorUtilities
{
    /// <summary>
    /// Locks the inspector window.
    /// </summary>
    static class EditorMenus
    {
        [MenuItem("GIB//Fix Box/Toggle Inspector Lock &#q", false,21)]
        static void ToggleInspectorLock()
        {
            ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
            ActiveEditorTracker.sharedTracker.ForceRebuild();
        }
    }
}
#if UNITY_EDITOR
using UnityEditor;

/* Lock The Inspector
GIB Games Unity Toolbox
https://github.com/GIBGames/gib-unity-toolbox
Released under MIT
*/

static class EditorMenus
{
    [MenuItem("Window/Toggle Inspector Lock &#q")] 
    static void ToggleInspectorLock()
    {
        ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
        ActiveEditorTracker.sharedTracker.ForceRebuild();
    }
}
#endif
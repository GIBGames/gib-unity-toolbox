using UnityEditor;

/* Lock The Inspector
Toast's useful Unity Scripts
https://github.com/dorktoast/ToastsUsefulUnityScripts
Released under MIT
*/

static class EditorMenus
{
    [MenuItem("Window/Toast's Script Library/Toggle Inspector Lock &#q")] 
    static void ToggleInspectorLock()
    {
        ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
        ActiveEditorTracker.sharedTracker.ForceRebuild();
    }
}
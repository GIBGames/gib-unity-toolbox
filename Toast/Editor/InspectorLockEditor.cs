using UnityEditor;

/* Lock The Inspector

by your host, the man they call Toast
Discord: dorktoast#0801

based on a script by SgtOkiDoki

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
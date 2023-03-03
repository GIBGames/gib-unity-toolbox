using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace GIB.EditorUtilities
{
    /// <summary>
    /// Performs script compilation requests.
    /// </summary>
    public class CompilationWindow : EditorWindow
    {
        [MenuItem("GIB//Fix Box/Request Script Compilation",false,21)]
        private static void ShowWindow()
        {
            CompilationPipeline.RequestScriptCompilation();
        }
    }
}
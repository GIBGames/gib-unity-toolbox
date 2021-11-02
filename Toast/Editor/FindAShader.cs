#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/* Find-a-Shader

by your host, the man they call Toast
Discord: dorktoast#0801

Based on a script by hzigy and robertbu

Released under MIT
*/

namespace Toast
{
    public class FindAShader : EditorWindow
    {
        string s = ""; // textbox string
        string sList = "Found shaders will appear here."; // Empty list text
        bool exact; // Determines if you're doing an exact search or a "contains" search
        Vector2 scrollPos; // for scrolling

        [MenuItem("Window/Toast's Script Library/Find-a-Shader")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(FindAShader), false, "Find-A-Shader"); // Makes the window appear.
        }

        public void OnGUI()
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(
                "This tool finds materials that have a specified shader.",
                EditorStyles.wordWrappedLabel);
            GUILayout.EndHorizontal();

            //The window itself is here.
            GUILayout.Label("Find Shader:");

            EditorGUILayout.BeginHorizontal();
            s = GUILayout.TextField(s);
            exact = EditorGUILayout.Toggle("Exact Search", exact, GUILayout.Width(60)); // Exact search checkbox
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Find Materials"))
            {
                ShaderSearch(s);
            }

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width), GUILayout.Height(position.height - 60));
            GUILayout.Label(sList);
            EditorGUILayout.EndScrollView();
        }

        private void ShaderSearch(string shaderName)
        {
            //The search, when the user clicks "Find Materials"
            int foundCount = 0;
            sList = "Materials with Shader: " + shaderName + ":\n\n";

            List<Material> armat = new List<Material>();
            //Basically this finds all Renderer objects and puts them in an array that is the size foundCount
            Renderer[] arrend = (Renderer[])Resources.FindObjectsOfTypeAll(typeof(Renderer));
            foreach (Renderer rend in arrend)
            {
                foreach (Material mat in rend.sharedMaterials)
                {
                    if (!armat.Contains(mat))
                    {
                        armat.Add(mat);
                    }
                }
            }

            foreach (Material mat in armat)
            {
                if (mat != null && mat.shader != null && mat.shader.name != null)
                {
                    if ((exact && mat.shader.name == shaderName) || (!exact && mat.shader.name.Contains(shaderName)))
                    {
                        sList += ">" + mat.name + "\n";
                        foundCount++;
                    }
                }
            }

            sList += "\n" + foundCount + " materials with " + shaderName + " found.";
        }
    }
}
#endif
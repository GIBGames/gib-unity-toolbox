﻿#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

/* Replace Missing Textures
Toast's useful Unity Scripts
https://github.com/dorktoast/ToastsUsefulUnityScripts
Released under MIT
*/

namespace Toast
{
    public class ReplaceMissingTextures : EditorWindow
    {
        public Object NewMat;
        public bool ChildOnly;
        private MeshRenderer[] renderers;
        float labelWidth = 150f;

        [MenuItem("Window/Toast's Script Library/Replace Missing Textures")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ReplaceMissingTextures), false, "Replace Missing Textures");
        }

        public void OnGUI()
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(
                "This tool finds Missing Textures and replaces them all" +
                "with the specified texture.",
                EditorStyles.wordWrappedLabel);
            GUILayout.EndHorizontal();

            EditorGUILayout.Space();

            GUILayout.BeginHorizontal();
            NewMat = EditorGUILayout.ObjectField(NewMat, typeof(Material), true) as Material;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            ChildOnly = EditorGUILayout.Toggle("Children of Selected Only", ChildOnly);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Find And Replace textures"))
            {
                if (NewMat == null)
                {
                    ShowNotification(new GUIContent("New Material empty!"));
                    return;
                }

                if (ChildOnly)
                {
                    GameObject selectedObject = Selection.activeGameObject;
                    renderers = selectedObject.GetComponentsInChildren<MeshRenderer>();
                }
                else
                {
                    renderers = FindObjectsOfType<MeshRenderer>();
                }
                Debug.Log($"Replace Missing Textures: Replacing (Children only = {ChildOnly})...");
                FindInSelected((Material)NewMat, renderers);
            }
            GUILayout.EndHorizontal();
        }
        private static void FindInSelected(Material newMat, MeshRenderer[] rend)
        {
            if (newMat == null)
            {
                Debug.LogError($"Material passed to {nameof(FindInSelected)} was null.");
                return;
            }

            foreach (MeshRenderer r in rend)
            {
                Material oldMat = r.sharedMaterial;
                if (oldMat != null) continue;
                Undo.RecordObject(r, "Modified Material");
                r.sharedMaterial = newMat;
            }
        }

    }
}
#endif
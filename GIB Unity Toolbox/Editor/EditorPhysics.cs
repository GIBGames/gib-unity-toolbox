#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Editor Physics
GIB Games Unity Toolbox
https://github.com/GIBGames/gib-unity-toolbox
Released under MIT
*/

namespace GIB
{
    public class EditorPhysics : EditorWindow
    {
#region Create Instance
        [MenuItem("GIB/Utilities/Editor Physics")]
        private static void CreateInstance()
        {
            CreateWindow<EditorPhysics>("Editor Physics").Show();
        }
#endregion

        private void OnEnable()
        {
            EditorApplication.update += OnEditorUpdate;
            SceneView.duringSceneGui += OnSceneGUI;
        }
        private void OnDisable()
        {
            EditorApplication.update -= OnEditorUpdate;
            SceneView.duringSceneGui -= OnSceneGUI;
        }
        private void OnDestroy()
        {
            //Make sure we are not simulating after this is destroyed.
            StopSimulation();
        }

#region Variables
        private int Tickrate = 33;
        private float Speed = 1f;
        private Vector3 Gravity = new Vector3(0, -9.8f, 0f);

        private List<HoldRigidbody> frozeSceneRigidbodies = new List<HoldRigidbody>(2048);
        private List<PlayingRigidbody> simulatedObjects = new List<PlayingRigidbody>(64);
        private bool _isSimulating;
        private Vector3 oldGravity;
#endregion

        private void StartSimulation()
        {
            if (_isSimulating)
                return;
            _isSimulating = true;


            //Clear selected objects
            simulatedObjects.Clear();

            //Find rigidbodies from our selection
            foreach (var item in Selection.gameObjects)
                simulatedObjects.Add(new PlayingRigidbody(item));


            //Take simulation to manual
            oldGravity = Physics.gravity;
            Physics.autoSimulation = false;
            Physics.gravity = Gravity;

            //Add some callbacks from editor
            Undo.undoRedoPerformed += OnUndoPerformed;
            SceneManager.activeSceneChanged += OnSceneChanged;


            //Clear List [Making sure]
            frozeSceneRigidbodies.Clear();

            //Find All rigidbodies
            var rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var root in rootObjects)
            {
                var objs = root.GetComponentsInChildren<Rigidbody>(false);
                foreach (var obj in objs)
                {
                    //If object is not the object we want to simulate
                    bool found = false;
                    foreach (var item in simulatedObjects)
                    {
                        if (item.Target == obj)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        frozeSceneRigidbodies.Add(new HoldRigidbody(obj));
                }
            }

            //Froze all rigidbodies except ours
            foreach (var item in frozeSceneRigidbodies)
                item.Freeze();

            //Register objects to be undoable
            for (int i = 0; i < simulatedObjects.Count; i++)
                Undo.RegisterFullObjectHierarchyUndo(simulatedObjects[i].Target, "Editor Physics Run");

        }
        private void StopSimulation()
        {
            if (!_isSimulating)
                return;
            _isSimulating = false;

            //Hand the simulation back to Unity
            Physics.autoSimulation = true;
            Physics.gravity = oldGravity;

            //Remove the callbacks from editor
            Undo.undoRedoPerformed -= OnUndoPerformed;
            SceneManager.activeSceneChanged -= OnSceneChanged;

            //Revert all other rigidbodies to old state
            foreach (var item in frozeSceneRigidbodies)
                item.Revert();

            //Reset to old state
            foreach (var item in simulatedObjects)
                item.Stop();

            //Clear referances
            frozeSceneRigidbodies.Clear();
            simulatedObjects.Clear();
        }
        private void ToggleSimulation()
        {
            if (_isSimulating)
                StopSimulation();
            else
                StartSimulation();
        }

        //Unity callbacks
        private void OnEditorUpdate()
        {
            if (_isSimulating)
            {
                Physics.gravity = Gravity;
                Physics.Simulate((1f / Tickrate) * Speed);
            }
        }
        private void OnUndoPerformed()
        {
            if (_isSimulating)
            {
                StopSimulation();
            }
        }
        private void OnSceneChanged(Scene arg0, Scene arg1)
        {
            StopSimulation();
        }
        private void OnSceneGUI(SceneView obj)
        {
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Semicolon)
            {
                Event.current.Use();
                ToggleSimulation();
            }
        }

        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(
                "This tool simulates physics in-editor, allowing for precise flush-to-floor" +
                "placement of objects, as well as simulation of debris and other scattered things.",
                EditorStyles.wordWrappedLabel);
            GUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical("box");
            {
                Tickrate = (int)EditorGUILayout.Slider("Tickrate", Tickrate, 8, 128);
                Speed = EditorGUILayout.Slider("Speed", Speed, 0.001f, 2f);
                Gravity = EditorGUILayout.Vector3Field("Gravity", Gravity);
            }
            EditorGUILayout.EndVertical();

            if (!_isSimulating)
            {
                if (GUILayout.Button("Simulate"))
                {
                    //Start the simulation
                    StartSimulation();
                }
            }
            else
            {
                if (GUILayout.Button("Stop Simulate"))
                {
                    StopSimulation();
                }
            }
        }

#region Classes
        class HoldRigidbody : IEquatable<Rigidbody>
        {
            public Rigidbody Target;
            public bool isKinematic;
            public RigidbodyConstraints Constraints;

            public HoldRigidbody(Rigidbody rigid)
            {
                this.Target = rigid;
                this.isKinematic = rigid.isKinematic;
                this.Constraints = rigid.constraints;
            }


            public void Freeze()
            {
                this.Target.isKinematic = true;
                this.Target.constraints = RigidbodyConstraints.FreezeAll;
            }
            public void Revert()
            {
                this.Target.velocity = Vector3.zero;
                this.Target.angularVelocity = Vector3.zero;
                this.Target.isKinematic = this.isKinematic;
                this.Target.constraints = this.Constraints;
            }


            public bool Equals(Rigidbody other)
            {
                return this.Target == other;
            }
        }
        class PlayingRigidbody
        {
            public Rigidbody Target;
            public bool actuallyHadRigidbody;
            public CollisionDetectionMode oldDetectionMode;

            public PlayingRigidbody(GameObject obj)
            {
                Target = obj.GetComponent<Rigidbody>();
                actuallyHadRigidbody = Target;
                if (!Target)
                    Target = obj.AddComponent<Rigidbody>();

                oldDetectionMode = Target.collisionDetectionMode;
                Target.collisionDetectionMode = CollisionDetectionMode.Continuous;
                Target.velocity = Vector3.zero;
                Target.angularVelocity = Vector3.zero;
            }
            public void Stop()
            {
                if (actuallyHadRigidbody)
                {
                    Target.collisionDetectionMode = oldDetectionMode;
                    Target.velocity = Vector3.zero;
                    Target.angularVelocity = Vector3.zero;
                }
                else
                {
                    DestroyImmediate(Target);
                    Target = null;
                }
            }
        }
#endregion

    }
}
#endif
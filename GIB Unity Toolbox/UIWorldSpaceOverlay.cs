using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* World Space Overlay
GIB Games Unity Toolbox
https://github.com/GIBGames/gib-unity-toolbox
Released under MIT
*/

namespace GIB
{
    /// <summary>
    /// Renders UI elements over the world geometry.
    /// Useful if subtitles or UI elements exist in world space, and you want them to always be visible.
    /// </summary>

    public class UIWorldSpaceOverlay : MonoBehaviour
    {
        private const string shaderTestMode = "unity_GUIZTestMode"; //This must be set for this to work.

        [SerializeField] UnityEngine.Rendering.CompareFunction desiredUIComparison = UnityEngine.Rendering.CompareFunction.Always;

        [Tooltip("Set to blank to automatically populate from the child UI elements")]
        [SerializeField] Graphic[] uiElementsToApplyTo;

        //Allows us to reuse materials
        private Dictionary<Material, Material> materialMappings = new Dictionary<Material, Material>();

        protected virtual void Start()
        {
            if (uiElementsToApplyTo.Length == 0)
            {
                uiElementsToApplyTo = gameObject.GetComponentsInChildren<Graphic>();
            }
            foreach (var graphic in uiElementsToApplyTo)
            {
                Material material = graphic.materialForRendering;
                if (material == null)
                {
                    Debug.LogError($"{nameof(UIWorldSpaceOverlay)}: skipping target without material {graphic.name}.{graphic.GetType().Name}");
                    continue;
                }
                if (!materialMappings.TryGetValue(material, out Material materialCopy))
                {
                    materialCopy = new Material(material);
                    materialMappings.Add(material, materialCopy);
                }
                materialCopy.SetInt(shaderTestMode, (int)desiredUIComparison);
                graphic.material = materialCopy;
            }
        }
    }
}
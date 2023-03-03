using UnityEngine;
using UnityEngine.Rendering;

namespace GIB
{
    /// <summary>
    /// Makes an object render over everything else in the scene.
    /// </summary>
    public class WorldSpaceOverlayElement : MonoBehaviour
    {
        private const string ShaderTestMode = "unity_GUIZTestMode"; //The magic property we need to set

        [SerializeField]
        private CompareFunction desiredUIComparison = CompareFunction.Always; //If you want to try out other effects
        
        [SerializeField] private Renderer thisGraphic;
        [SerializeField] private Material thisMaterial;

        protected virtual void Awake()
        {
            thisGraphic = GetComponent<Renderer>();
            thisMaterial = thisGraphic.material;

            if (!thisMaterial)
            {
                Debug.LogError(
                    $"{nameof(WorldSpaceOverlayElement)}: skipping target without material {thisGraphic.name}.{thisGraphic.GetType().Name}");
                Destroy(this);
            }

            Material newMat = new Material(thisMaterial);
            
            newMat.SetInt(ShaderTestMode, (int)desiredUIComparison);

            thisGraphic.material = newMat;
        }
    }
}
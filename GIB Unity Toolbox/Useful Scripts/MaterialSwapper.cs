using UnityEngine;
using UnityEngine.Serialization;

namespace GIB
{
    /// <summary>
    /// Exchanges one material for another target material.
    /// </summary>
    public class MaterialSwapper : MonoBehaviour
    {
        [FormerlySerializedAs("targetRenderer")]
        [SerializeField] private Renderer _targetRenderer;

        /// <summary>
        /// An array of target materials. This must be the same 
        /// Length as target Renderer's material array.
        /// </summary>
        [FormerlySerializedAs("newMaterials")]
        [Tooltip("Must be the same Length as target Renderer's material array.")]
        public Material[] _swapToMaterials;

        private Material[] _originalMaterials;
        private bool _isSwapped;

        /// <summary>
        /// Whether the materials have already been swapped.
        /// </summary>
        public bool IsSwapped => _isSwapped;

        private void Start()
        {
            _originalMaterials = _targetRenderer.materials;

            if (_targetRenderer == null)
            {
                _targetRenderer = GetComponent<MeshRenderer>();
            }
        }

        /// <summary>
        /// Sets a Renderer's materials to the specified Material array.
        /// </summary>
        private void SetMaterials(Material[] materials)
        {
            if (materials.Length != _originalMaterials.Length)
            {
                Debug.LogWarning($"{gameObject.name}> Attempted to set material array with invalid length in {nameof(MaterialSwapper)}");
            }
            else
            {
                _targetRenderer.materials = materials;
            }
        }

        /// <summary>
        /// Swaps a Renderer's materials between old and new.
        /// </summary>
        public void SwapMaterials()
        {
            if (IsSwapped)
            {
                ResetToOriginalMaterials();
            }
            else
            {
                ApplySwappedMaterials();
            }
        }

        /// <summary>
        /// Applies the target materials.
        /// </summary>
        public void ApplySwappedMaterials()
        {
            SetMaterials(_swapToMaterials);
            _isSwapped = true;
        }

        /// <summary>
        /// Applies the original materials.
        /// </summary>
        public void ResetToOriginalMaterials()
        {
            SetMaterials(_originalMaterials);
            _isSwapped = false;
        }
    }
}

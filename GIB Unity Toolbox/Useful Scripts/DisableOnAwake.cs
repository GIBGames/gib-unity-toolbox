using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GIB
{
    /// <summary>
    /// Disables the GameObject when the component is awake
    /// </summary>
    public class DisableOnAwake : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(false);
        }
    }
}
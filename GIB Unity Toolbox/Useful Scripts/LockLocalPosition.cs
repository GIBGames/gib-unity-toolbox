using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GIB
{
    /// <summary>
    /// Locks a position in local space.
    /// </summary>
    public class LockLocalPosition : MonoBehaviour
    {
        [SerializeField] private bool locked;

        [SerializeField] private Vector3 startPos;

        private void Start()
        {
            startPos = transform.localPosition;
        }

        private void FixedUpdate()
        {
            if (locked)
                transform.localPosition = startPos;
        }



    }
}
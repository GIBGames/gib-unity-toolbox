using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GIB
{
    /// <summary>
    /// Used for determining if an object is colliding. For debug purposes only.
    /// </summary>
    /// <remarks>If you're using this script you must have spent hours
    /// trying to figure out what the hell is colliding with the thing.
    /// If that's the case I am so sorry."</remarks>
    public class DebugFindCollider : MonoBehaviour
    {
        [SerializeField] private bool DetectCollisions;
        [SerializeField] private bool DetectTriggerEnter;
        [SerializeField] private bool DetectTriggerExit;
        private void OnCollisionEnter(Collision collision)
        {
            if (DetectCollisions)
                Debug.Log($"colliding with {collision.collider.gameObject.name}");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (DetectTriggerEnter)
                Debug.Log($"colliding with {other.gameObject.name}");
        }

        private void OnTriggerExit(Collider other)
        {
            if (DetectTriggerExit)
                Debug.Log($"colliding with {other.gameObject.name}");
        }
    }
}
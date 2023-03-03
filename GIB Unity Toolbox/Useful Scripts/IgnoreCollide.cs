using System.Collections.Generic;
using UnityEngine;

namespace GIB
{
    /// <summary>
    ///     Sets a list of colliders that the object should not collide with
    /// </summary>
    public class IgnoreCollide : MonoBehaviour
    {
        /// <summary>
        /// Colliders which should be ignored by this object's collider.
        /// </summary>
        public List<Collider> CollidersToIgnore;

        private void Start()
        {
            Collider thisCol = GetComponent<Collider>();
            if (CollidersToIgnore != null)
            {
                foreach (Collider col in CollidersToIgnore)
                {
                    if (col && col.enabled) Physics.IgnoreCollision(thisCol, col, true);
                }
            }
        }
    }
}
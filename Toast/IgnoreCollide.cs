using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Ignore Collide
Toast's useful Unity Scripts
https://github.com/dorktoast/ToastsUsefulUnityScripts
Released under MIT
*/

namespace Toast
{
    public class IgnoreCollide : MonoBehaviour
    {
        public List<Collider> CollidersToIgnore;

        void Start()
        {
            var thisCol = GetComponent<Collider>();
            if (CollidersToIgnore != null)
            {
                foreach (var col in CollidersToIgnore)
                {
                    if (col && col.enabled)
                    {
                        Physics.IgnoreCollision(thisCol, col, true);
                    }
                }
            }
        }
    }
}
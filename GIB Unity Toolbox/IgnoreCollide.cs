using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Ignore Collide
GIB Games Unity Toolbox
https://github.com/GIBGames/gib-unity-toolbox
Released under MIT
*/

namespace GIB
{
	/// <summary>
	/// Sets a collider to ignore collisions from specific other colliders.
	/// </summary>
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
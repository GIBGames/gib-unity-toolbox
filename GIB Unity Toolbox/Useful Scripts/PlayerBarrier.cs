using UnityEngine;

namespace GIB
{
    /// <summary>
    /// A barrier that players cannot walk through or teleport through.
    /// </summary>
    public class PlayerBarrier : MonoBehaviour
    {
#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            BoxCollider box = transform.GetComponent<BoxCollider>();
            if (box != null)
            {
                Gizmos.color = new Color(.6f, .5f, .2f, .2f);
                Gizmos.matrix = transform.localToWorldMatrix; // following Gizmos will be drawn in this transform's local space.
                Gizmos.DrawCube(box.center, box.size);
            }
        }
#endif
    }
}
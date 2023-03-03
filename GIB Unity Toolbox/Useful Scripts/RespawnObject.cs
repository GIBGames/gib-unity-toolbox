using UnityEngine;

namespace GIB
{
    /// <summary>
    /// Respawns objects with rigidbodies if they fall to below a certain height.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class RespawnObject : MonoBehaviour
    {
        [SerializeField] private float respawnHeight = -50;
        [SerializeField] private bool destroyInstead = false;
        private Vector3 startPos;
        private Quaternion startRot;

        private Rigidbody rb;

        private bool respawnNextFrame;

        private void Start()
        {
            startRot = transform.rotation;

            rb = transform.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (respawnNextFrame)
                DoRespawn();

            if (!rb.IsSleeping() && !rb.isKinematic)
                RespawnCheck();
        }

        /// <summary>
        /// Check the Object's current height and respawn if it is below the threshhold.
        /// This method is called on FixedUpdate if the Rigidbody is not sleeping or Kinematic.
        /// </summary>
        public void RespawnCheck()
        {
            if (startPos.y + respawnHeight > transform.position.y)
            {
                if (destroyInstead)
                {
                    Debug.Log($"{this.gameObject.name}> Dropped to {respawnHeight}, destroying");
                    Destroy(this.gameObject);
                    return;
                }
                StartRespawn();
            }
        }

        /// <summary>
        /// Force this object to respawn on next FixedUpdate.
        /// </summary>
        public void StartRespawn()
        {
            respawnNextFrame = true;
            Debug.LogWarning($"{gameObject.name}> Dropped to {respawnHeight}, respawning", gameObject);
        }

        private void DoRespawn()
        {
            //move the object back to its start
            transform.position = startPos;
            transform.rotation = startRot;

            //Set velocity to 0 so object doesn't keep falling
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep();

            respawnNextFrame = false;
        }
    }
}
//SFS 2/21/21

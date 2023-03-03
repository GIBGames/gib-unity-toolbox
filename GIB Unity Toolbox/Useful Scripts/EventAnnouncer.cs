using UnityEngine;

namespace GIB
{
    /// <summary>
    /// Sends a specified event to the <see cref="EventController"/>.
    /// <remarks>Preferred method to use is <see cref="GameUtilities.Announce(string)"/>.</remarks>
    /// </summary>
    public class EventAnnouncer : MonoBehaviour
    {
        [SerializeField] private string eventName;

        public void AnnounceEvent()
        {
            EventController.Announce(eventName);
        }

        public void AnnounceEvent(string name)
        {
            EventController.Announce(name);
        }
    }
}

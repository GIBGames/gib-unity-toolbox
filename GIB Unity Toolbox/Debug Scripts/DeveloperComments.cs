using UnityEngine;

namespace GIB
{
    /// <summary>
    /// Literally just a text box for developer commentary.
    /// </summary>
    /// <remarks>Text is stripped out on build.</remarks>
    public class DeveloperComments : MonoBehaviour
    {
#if UNITY_EDITOR
        /// <summary>
        /// A block of developer text.
        /// </summary>
        [TextArea] public string devText;
#endif
    }
}

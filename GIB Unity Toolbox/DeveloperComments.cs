﻿using UnityEngine;

namespace GIB
{
    /// <summary>
    /// Literally just a text box for developer commentary.
    /// </summary>
    public class DeveloperComments : MonoBehaviour
    {
        [SerializeField, TextArea] private string devText;
    }
}

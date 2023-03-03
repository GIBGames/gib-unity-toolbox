using UnityEngine;

namespace GIB
{
	/// <summary>
	/// Objects with this component will not be destroyed when new scenes are loaded.
	/// </summary>
	public class DontDestroyOnLoad : MonoBehaviour
	{
		void Awake()
		{
			DontDestroyOnLoad(this);
		}
	}
	
	/*
	Do not stand at my grave and weep,
	I am not there, I do not sleep.
	I am in a thousand winds that blow,
	I am the softly falling snow.
	I am the gentle showers of rain,
	I am the fields of ripening grain.
	I am in the morning hush,
	I am in the graceful rush
	Of beautiful birds in circling flight,
	I am the starshine of the night.
	I am in the flowers that bloom,
	I am in a quiet room.
	I am in the birds that sing,
	I am in each lovely thing.
	Do not stand at my grave and cry,
	I am not there. I do not die. 
	
	— Mary Elizabeth Frye
	*/
}

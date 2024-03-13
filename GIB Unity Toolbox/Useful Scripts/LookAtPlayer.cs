using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
	public bool IsLooking;
    private Transform _camera;

    private void Start()
    {
        _camera = Camera.main.transform;
    }

    private void FixedUpdate()
    {
		if(IsLooking)
			transform.LookAt(Camera.main.transform);
    }
}

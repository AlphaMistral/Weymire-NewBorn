using UnityEngine;
using System.Collections;

public class TestRotateAround : MonoBehaviour 
{
	public Transform anchorPoint;
	[SerializeField] private float rotateSpeed;

	void Update ()
	{
		transform.RotateAround(anchorPoint.transform.position, Vector3.forward, rotateSpeed * Time.deltaTime);
		transform.rotation = Quaternion.identity;
	}

}

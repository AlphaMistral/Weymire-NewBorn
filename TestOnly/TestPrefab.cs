using UnityEngine;
using System.Collections;

public class TestPrefab : MonoBehaviour 
{
	public Rigidbody ff;

	void Start ()
	{
		Rigidbody x = Instantiate(ff, transform.position, transform.rotation) as Rigidbody;
		x.velocity = new Vector3(0f, 1f, 1f);
	}
}

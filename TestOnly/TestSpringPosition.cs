using UnityEngine;
using System.Collections;

public class TestSpringPosition : MonoBehaviour 
{
	void Start ()
	{
		GetComponent<SpringPosition>().target = transform.localPosition;
	}
}

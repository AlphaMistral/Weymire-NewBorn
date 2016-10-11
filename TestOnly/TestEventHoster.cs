using UnityEngine;
using System.Collections;

public class TestEventHoster : MonoBehaviour 
{
	public delegate void Fucker ();
	public event Fucker FuckEvent;

	void OnEnable ()
	{
		
	}

	void Start ()
	{
		StartCoroutine(routine ());
	}

	IEnumerator routine ()
	{
		yield return new WaitForSeconds(1f);
		//FuckEvent();
	}
}

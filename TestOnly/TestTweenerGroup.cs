using UnityEngine;
using System.Collections;

public class TestTweenerGroup : MonoBehaviour 
{
	private void Awake ()
	{
		EventDelegate del = new EventDelegate ();
		del.methodName = "Play";
	}
}

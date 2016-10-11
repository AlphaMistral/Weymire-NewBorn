using UnityEngine;
using System.Collections;

public class TestChildChangeEvent : MonoBehaviour 
{
	void ChildGet (GameObject other)
	{
		Debug.Log("I have received a new Child! " + other.name);
	}

	void ChildLose (GameObject other)
	{
		Debug.Log("I have lost a Child!" + other.name);
	}
}

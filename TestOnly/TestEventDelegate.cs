using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestEventDelegate : MonoBehaviour 
{
	public List<EventDelegate> toExe = new List <EventDelegate> ();

	void Start ()
	{
		StartCoroutine(Operate ());
	}

	/// <summary>
	/// Please Note that the EventDelegate.Execute () Receives a list as the parameter only! 
	/// </summary>
	IEnumerator Operate ()
	{
		Debug.Log("Here!");
		yield return new WaitForSeconds(1f);
		EventDelegate.Execute(toExe);
	}

	public void HAHA ()
	{
		Debug.Log("HAHA");
	}

	public void HEHE ()
	{
		Debug.Log ("HEHE");
	}
}

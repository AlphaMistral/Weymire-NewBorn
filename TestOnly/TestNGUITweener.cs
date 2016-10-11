using UnityEngine;
using System.Collections;

public class TestNGUITweener : MonoBehaviour 
{
	void Start ()
	{
		StartCoroutine(PlayTween ());
	}

	IEnumerator PlayTween ()
	{
		yield return new WaitForSeconds(3f);
		GetComponent <UIPlayTween>().Play(true);
		yield return new WaitForSeconds(3f);
		GetComponent <UIPlayTween>().Play(false);
		yield return new WaitForSeconds(3f);
		GetComponent <UIPlayTween>().Play(true);
	}
}

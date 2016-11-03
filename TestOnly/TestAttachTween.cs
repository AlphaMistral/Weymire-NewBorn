using UnityEngine;
using System.Collections;

public class TestAttachTween : MonoBehaviour 
{
	private void Awake ()
	{
		TweenPosition tp = TweenPosition.Begin(gameObject, 0.4f, new Vector3(151f, -690f, 0f));
		tp.ignoreTimeScale = false;
		tp.method = UITweener.Method.EaseIn;
	}
}

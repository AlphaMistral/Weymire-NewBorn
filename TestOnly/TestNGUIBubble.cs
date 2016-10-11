using UnityEngine;
using System.Collections;

public class TestNGUIBubble : MonoBehaviour 
{
	public Transform target;
	public int absolute;
	public float relative;

	public GameObject other;

	void Start ()
	{
		StartCoroutine(Fuck());
	}

	void Awake ()
	{
		
	}

	IEnumerator Fuck ()
	{
		while (true)
		{
			GameObject o = NGUITools.AddChild(transform.parent.gameObject, other);
			GetComponent<UILabel>().bottomAnchor.target = o.transform;
			GetComponent<UILabel>().bottomAnchor.absolute = absolute;
			GetComponent<UILabel>().bottomAnchor.relative = 1;
			o.GetComponent<UILabel>().text = "asdfasfagawqegwqt asf3t 1\t3 5werrf 23t qetr wdfas g 4 tzsdgfvawsdg 34 easdrg fas f";
			yield return null;
			break;
		}
	}
}


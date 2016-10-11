using UnityEngine;
using System.Collections;

public class TestStrangeBubble : MonoBehaviour 
{
	public int times;

	public UILabel labelPrefab;

	public UILabel currentLabel = null;

	public UILabel testLabel;

	private int x = 0;

	void Start ()
	{
		//StartCoroutine(ff());
		StartCoroutine(gg());
	}

	IEnumerator ff ()
	{
		while (x < times)
		{
			x++;
			UILabel test = NGUITools.AddChild(gameObject, labelPrefab.gameObject).GetComponent<UILabel> ();
			if (currentLabel == null)
			{
				currentLabel = test;
				yield return new WaitForSeconds(1f);
			}
			else
			{
				currentLabel.bottomAnchor.target = test.transform;
				currentLabel.bottomAnchor.absolute = 10;
				currentLabel = test;
				yield return new WaitForSeconds(1f);
			}
		}
	}

	IEnumerator gg()
	{
		while (x < times)
		{
			x++;
			UILabel test = NGUITools.AddChild(gameObject, labelPrefab.gameObject).GetComponent<UILabel> ();
			testLabel.bottomAnchor.target = test.transform;
			testLabel.bottomAnchor.absolute = 10;
			yield return new WaitForSeconds(3f);
		}
	}

}

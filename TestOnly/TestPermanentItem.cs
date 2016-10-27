using UnityEngine;
using System.Collections;

public class TestPermanentItem : MonoBehaviour 
{
	private void Awake ()
	{
		PermanentItem item = PermanentItem.GetItemByID(0);
		Debug.Log(item.SpriteName);
	}
}

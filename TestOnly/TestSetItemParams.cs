using UnityEngine;
using System.Collections;

public class TestSetItemParams : MonoBehaviour
{
	public string itemName;
	public string introduction;
	public int index;
	public string modelName;
	public string spriteName;

	private void Awake ()
	{
		GetComponent<ItemView> ().item = PermanentItem.CreateTemporaryItem(index, itemName, introduction, spriteName, modelName);
	}
}

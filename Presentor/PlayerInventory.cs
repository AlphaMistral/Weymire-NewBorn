using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;

public class InventoryState
{
	public LensItem lensItem;
	public Item item;
}

public class PlayerInventory : MonoBehaviour 
{
	public LensItem EquippedLens
	{
		get
		{
			return equippedLens;
		}
		private set
		{
			inventoryState.lensItem = value;
		}
	}

	public Item EquippedItem
	{
		get
		{
			return equippedItem;
		}
		private set
		{
			inventoryState.item = value;
		}
	}

	private LensItem equippedLens;
	private Item equippedItem;

	[SerializeField]
	private BackPack backPack;
	[SerializeField]
	private LensPack lensPack;

	private static PlayerInventory instance;

	private static float UPDATE_FREQ;

	private InventoryState inventoryState;

	private List <Item> inventoryItems = new List <Item> ();

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			DestroyImmediate(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		inventoryItems = new List <Item>();
	}

	void Start ()
	{
		//Start the corresponding coroutines here.
		//Item item1 = ReadItemFromFile (0);
		//Item item2 = ReadItemFromFile (0);
		//inventoryItems.Add(item1);
		//inventoryItems.Add(item2);
		TestAddItem ();
	}

	private static void LoadItems()
	{
		
	}

	private static IEnumerator CheckItemVarables()
	{
		while (true)
		{

			yield return new WaitForSeconds(UPDATE_FREQ);
		}
	}

	public static void EquipItem(Item item)
	{
		
	}

	/// <summary>
	/// Put the items in the list into the playerInventory and set them under the control of the invertory system.
	/// </summary>
	/// <param name="items">Items.</param>
	public static void AddNewItemList(List<Item> items)
	{
		//instance.backPack.InsertItemList(items);
		foreach (Item item in items)
			AddNewItem(item);
	}

	/// <summary>
	/// Just in case, a function which conveniently process a single item is also provided.
	/// </summary>
	/// <param name="item">Item.</param>
	public static void AddNewItem(Item item)
	{
		//instance.backPack.InsertItem(item);
		instance.inventoryItems.Add (item);
		//DialogueLua.SetVariable(Consts.VariableName.backPackPermanentItemName + item.Index, 1);
		instance.backPack.UpdateBackPackContent();
	}

	/// <summary>
	/// This method is seldomly invoked since only up to 4 different lenses are provided in the game. However when a new lens is unlocked, call for this method. 
	/// </summary>
	/// <param name="type">Type.</param>
	public static void UnlockNewLens(Consts.LensType type)
	{
		
	}

	/// <summary>
	/// Use the Equipped Item. If no item is equipped nothing would be done. 
	/// </summary>
	public static void UseItem()
	{
		if (instance.equippedItem == null)
			return;
		if (instance.equippedItem.IsPermanent)
		{
			instance.inventoryItems.Remove(instance.equippedItem);
			instance.equippedItem = instance.inventoryState.item = null;
		}
	}

	/// <summary>
	/// Equip the indicated lens.
	/// </summary>
	/// <param name="lens">Lens.</param>
	public static void EquipLens(LensItem lens)
	{
		
	}

	/// <summary>
	/// Reads the concise item information from the item file. The index is the line number of the item. 
	/// </summary>
	/// <returns>The item from file.</returns>
	/// <param name="index">Index.</param>
	private static Item ReadItemFromFile (int index)
	{
		string itemString = InfoSaver.GetStringFromResource(Consts.FileName.items, index);
		string[] strings = itemString.Split('#');
		int idx = int.Parse (strings[0]);
		string name = strings[1];
		string modelName = strings[2];
		string introduction = strings[3];
		bool p = int.Parse(strings[4]) == 1;
		return new Item(idx, name, modelName, introduction, p);
	}

	public static void AddItemByIndex (int index)
	{
		Item toAdd = ReadItemFromFile(index);
		AddNewItem(toAdd);
	}

	public static InventoryState GetInventoryState ()
	{
		return instance.inventoryState;
	}

	public static List<Item> GetItemList ()
	{
		return instance.inventoryItems;
	}

	public void TestAddItem ()
	{
		Item item = ReadItemFromFile(0);
		AddNewItem(item);
	}
}

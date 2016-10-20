using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;

public class InventoryState
{
	public LensItem lensItem;
	public DisplayItem item;
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

	public DisplayItem EquippedItem
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
	private DisplayItem equippedItem;

	[SerializeField]
	private BackPack backPack;
	[SerializeField]
	private LensPack lensPack;

	private static PlayerInventory instance;

	private static float UPDATE_FREQ;

	private InventoryState inventoryState;

	private List <DisplayItem> inventoryItems = new List <DisplayItem> ();

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
		inventoryItems = new List <DisplayItem>();
	}

	void Start ()
	{
		
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
	public static void AddNewItemList(List<DisplayItem> items)
	{
		//instance.backPack.InsertItemList(items);
		foreach (DisplayItem item in items)
			AddNewItem(item);
	}

	/// <summary>
	/// Just in case, a function which conveniently process a single item is also provided.
	/// </summary>
	/// <param name="item">Item.</param>
	public static void AddNewItem(DisplayItem item)
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
		if (Object.ReferenceEquals (instance.equippedItem.GetType (), typeof (PermanentItem)))
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
	private static DisplayItem ReadItemFromFile (ItemType type, int index)
	{
		if (type == ItemType.Permanent)
			return PermanentItem.GetItemByID(index);
		else
			return UsableItem.GetItemByID(index);
	}

	public static void AddItemByIndex (ItemType type, int index)
	{
		DisplayItem toAdd = ReadItemFromFile(type, index);
		AddNewItem(toAdd);
	}

	public static InventoryState GetInventoryState ()
	{
		return instance.inventoryState;
	}

	public static List<DisplayItem> GetItemList ()
	{
		return instance.inventoryItems;
	}

	public static void Initialize ()
	{
		
	}
}

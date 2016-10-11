using UnityEngine;
using System.Collections;

public class Item
{
	public int Index
	{
		get
		{
			return index;
		}
	}

	public string Name
	{
		get
		{
			return name;
		}
	}

	public string ModelName
	{
		get
		{
			return modelName;
		}
	}

	public string Introduction
	{
		get
		{
			return introduction;
		}
	}

	public bool IsPermanent
	{
		get
		{
			return isPermanent;
		}
	}

	public string Belonger
	{
		get
		{
			return belonger;
		}
	}

	private int index;
	private string name;
	private string modelName;
	private string introduction;
	private bool isPermanent;
	private string belonger; 
	public virtual void EquipItem()
	{
		
	}

	protected virtual void UseItem()
	{
		
	}

	public Item ()
	{
		modelName = "Sphere";
		name = "Mother Fucker Test";
		introduction = "Well this is only a test script please don't expect that much hhhhhhhhhhhhh!!!";
	}

	public Item (int idx, string n, string m, string i, bool isP)
	{
		index = idx;
		name = n;
		modelName = m;
		introduction = i;
		isPermanent = isP;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{

	public new string name;
	public int quantity;
	public int xp;
	public string description;

	public Sprite artwork;

	public float size;

	public void Print()
	{
		Debug.Log(name + ": " + description + " size: " + size);
	}

	public Item Copy()
	{
		Item newItem = CreateInstance<Item>();
		newItem.name = name;
		newItem.quantity = quantity;
		newItem.description = description;
		newItem.artwork = artwork;
		newItem.size = size;

		return newItem;
	}

}
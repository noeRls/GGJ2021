﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemInfo
{
    public int price;
    public string name;
    public string imagePath;
    public string description;
    public GameObject prefab;
    public ItemCategory category;
    public KeyCode activationKey;
}

public enum ItemCategory
{
    DRUG,
    TRAP
}

public class ItemList : MonoBehaviour
{
    public List<ItemInfo> items;
}

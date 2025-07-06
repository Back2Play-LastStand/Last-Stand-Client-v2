using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOItem", menuName = "Item/SOItem")]
public class SOItem : ScriptableObject
{
    public string itemName;
    public int level;

    [SerializeField]
    public class STAT
    {
        public string name;
        public int value;
    }

    public List<STAT> stats = new();

    public int maxStack;
    public int price;

    public Sprite icon;
    public Transform prefab;

    [Multiline]
    public string description;
}

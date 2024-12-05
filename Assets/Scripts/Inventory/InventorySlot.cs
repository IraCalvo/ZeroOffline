using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public IItem Item { get; set; }
    public int StackSize { get; set; }
}

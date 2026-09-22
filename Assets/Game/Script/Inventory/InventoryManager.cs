using UnityEngine;
using System.Collections.Generic;
public class InventoryManager : MonoBehaviour
{
    // Variable list untuk menyimpan semua item yang ada di inventory  
    private List<ItemData> _item = new List<ItemData>();
    // Property untuk mengakses list _item  
    public List<ItemData> Items => _item;
 
    // Function untuk menambahkan itemdata ke dalam list
    public void AddItems(ItemData item)
    {
        Items.Add(item);
    }
 
    // Function untuk mengecek itemdata di dalam list
    public bool CheckItem(string id)
    {
        // Mencari apakah ada itemdata di dalam list 
        // yang id nya sama dengan id yang ditentukan di parameter.
        // Jika ketemu akan bernilai true, jika tidak akan bernilai false.
        bool isExsists = Items.Exists(itemData => string.Equals(itemData.ID, id));
        return isExsists;
    }
 
    // Function untuk menghapus itemdata dari list
    public void RemoveItem(ItemData item)
    {
        Items.Remove(item);
    }
}
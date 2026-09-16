using UnityEngine;
// Serialize class item data supaya variable di dalamnya
// nilai nya bisa ditentukan melalui inspector
[System.Serializable]
public class ItemData : MonoBehaviour
{
    public string ID;
    public string Name;

    public ItemData(string id, string name)
    {
        ID = id;
        Name = name;
    }
}

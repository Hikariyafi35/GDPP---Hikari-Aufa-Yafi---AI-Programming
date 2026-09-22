using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField]
    private ItemData _itemData;
    public string Name => _itemData.Name;

    // Membuat event untuk memberi tahu module lain 
    // jika item telah diambil
    public UnityEvent OnItemPicked;
    
    [ContextMenu("Interact Item")]
    public void Interact()
    {
        Pickup();

    }

    public void Pickup()
    {
        // Memanggil event ketika item diambil
        OnItemPicked?.Invoke();
        // Menghapus item ketika item diambil
        Destroy(gameObject);
    }

    public void Interact(PlayerCharacter character)
    {
        Pickup(character);
    }

    public virtual void Pickup(PlayerCharacter character)
    {
        // Membuat variable salinan data dari variable _data
        ItemData newData = new ItemData(_itemData.ID, _itemData.Name);
        // Menambahkan salinan data ke list di inventory
        // menggunakan reference PlayerCharacter
        character.Inventory.AddItems(newData);
        // Memanggil event ketika item diambil
        OnItemPicked?.Invoke();
        // Menghapus item ketika item diambil
        Destroy(gameObject);
    }
}

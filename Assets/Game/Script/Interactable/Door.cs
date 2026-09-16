using UnityEngine;

public class Door : MonoBehaviour , IInteractable
{
    [SerializeField]
    private string _name;
    // Wajib membuat property Name
    // Property Name diisikan nilai variable _name
    public string Name => _name;

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
}

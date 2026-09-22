using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    // Variable untuk reference ke module PlayerCharacterMovement
    [SerializeField]
    private PlayerCharacterMovement _movement;
    // Variable untuk reference ke module PlayerCharacterStamina
    [SerializeField]
    private PlayerCharacterStamina _stamina;
    // Variable untuk reference ke module InventoryManager
    [SerializeField]
    private InventoryManager _inventory;

    // Variable untuk reference ke module InteractDetector
    [SerializeField]
    private InteractDetector _interactDetector;
    // Variable untuk reference ke module CameraManager
    [SerializeField]
    private CameraManager _camera;
    // Property untuk menentukan apakah player sedang hiding
    public bool IsHiding { get; private set; }
    // Variable untuk reference ke module InputManager
    [SerializeField]
    private InputManager _input;
        // Variable untuk reference ke module Flashlight
    [SerializeField]
    private Flashlight _flashlight;


    // Property untuk mengakses variable _movement
    public PlayerCharacterMovement Movement => _movement;
    // Property untuk mengakses variable _stamina
    public PlayerCharacterStamina Stamina => _stamina;
    // Property untuk mengakses variable _inventory
    public InventoryManager Inventory => _inventory;
    // Property untuk mengakses variable _interactDetector
    public InteractDetector InteractDetector => _interactDetector;
        // Property untuk mengakses variable _flashlight
    public Flashlight Flashlight => _flashlight;
    // Property untuk mengakses variable _camera
    public CameraManager Camera => _camera;
    // Property untuk mengakses variable _input
    public InputManager Input => _input;

    private void Awake()
    {
        // Ketika game dijalankan,
        // cursor mouse akan disembunyikan
        Cursor.visible = false;
        // cursor mouse akan dikunci di tengah layar
        Cursor.lockState = CursorLockMode.Locked;
    }
        public void SetIsHiding(bool isHiding)
    {
        IsHiding = isHiding;
    }
}

using UnityEngine;

public class Flashlight : MonoBehaviour
{
    // Variable untuk reference ke component Light
    [SerializeField]
    private Light _light;
    // Variable untuk reference ke PlayerCharacter
    // yang memiliki flashlight
    [SerializeField]
    private PlayerCharacter _owner;
    // Variable untuk menentukan jumlah batre di awal game
    [SerializeField]
    private float _initialBatteryLevel = 100;
    // Variable untuk menentukan jumlah pengurangan batre setiap detik
    [SerializeField]
    private float _batteryDrainRate = 1;

    
    // Variable untuk menentukan jumlah batre flashlight
    private float _batteryLevel;

    // Property untuk menentukan apakah flashlight
    // masih memiliki batre atau tidak
    public bool HasBattery => _batteryLevel > 0;

    
    private void Awake()
    {
        if (_light != null)
        {
            _light.enabled = false;
        }
        // Mengisi level batre dengan level batre di awal game
        _batteryLevel = _initialBatteryLevel;
    }
        private void Update()
    {
        // Mengupdate rotasi flashlight terus menerus selama game berjalan
        UpdateFlashlightRotation();
        // Mengupdate batre terus menerus selama game berjalan
        UpdateBatteryLevel();
    }

    // Property untuk menentukan apakah player 
    // memiliki flashlight di inventory atau tidak
    public bool HasFlashlight => _owner != null && _owner.Inventory != null && _owner.Inventory.CheckItem("Flashlight_001");

    // Function untuk toggle flashlight
    public void UseFlashlight()
    {
        // Mengecek apakah player memiliki flashlight di inventory
        // Memastikan juga ada reference component light
        if (HasFlashlight == true && _light != null)
        {
            if (HasBattery == true)
            {
                // Jika masih ada batre toggle flashlight
                // Jika flashlight menyala maka akan mati
                // Jika flashlight mati maka akan menyala
                _light.enabled = !_light.enabled;
            }
            else
            {
                // Jika sudah tidak ada batre
                // Matikan flashlight
                _light.enabled = false;
            }
        }
    }

    private void UpdateFlashlightRotation()
    {
        // Mengubah rotasi flashlight dengan rotasi camera
        _light.transform.rotation = Camera.main.transform.rotation;
    }
    private void UpdateBatteryLevel()
    {
        // Memastikan component light ada 
        // dan sedang aktif (flashlight menyala)
        if (_light != null && _light.enabled == true)
        {
            // Mengecek apakah flashlight masih punya batre
            if (HasBattery == true)
            {
                // Jika batre masih ada kurangi level setiap detik dengan jumlah
                // yang sudah ditentukan di variable
                _batteryLevel = _batteryLevel - _batteryDrainRate * Time.deltaTime;
            }
            else
            {
                // Jika batre habis isi level batre dengan nol 
                // dan matikan component light
                _batteryLevel = 0;
                _light.enabled = false;
            }
        }
    }
    public void RefillBatteryLevel()
    {
        // Mengisi ulang batre dengan nilai awal
        _batteryLevel = _initialBatteryLevel;
    }
}

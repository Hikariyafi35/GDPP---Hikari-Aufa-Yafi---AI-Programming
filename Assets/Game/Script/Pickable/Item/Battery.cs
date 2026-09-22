using UnityEngine;

public class Battery : Item
{
    public override void Pickup(PlayerCharacter character)
    {
        // Memanggil function Pickup di class Parent/Base (Item)
        // untuk mengambil item
        base.Pickup(character);
        // Melakukan refill batre flashlight
        character.Flashlight.RefillBatteryLevel();
    }
}

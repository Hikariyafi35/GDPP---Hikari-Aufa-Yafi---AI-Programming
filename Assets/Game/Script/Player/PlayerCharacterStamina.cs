using UnityEngine;

public class PlayerCharacterStamina : MonoBehaviour
{
    [SerializeField]
    private float _maxStamina = 100;
    [SerializeField]
    private float _sprintStaminaCost = 20;
    [SerializeField]
    private float _staminaRegenValue = 20;
    [SerializeField]
    private float _currentStamina;

    [SerializeField]
    private PlayerCharacterMovement _characterMovement;

    private void Awake()
    {
        _currentStamina = _maxStamina;
    }
    void Update()
    {
        CalculateStamina(); 
    }
    public void CalculateStamina()
    {
        if (_characterMovement.IsSprint)
        {
            if (_currentStamina > 0)
            {
                _currentStamina = _currentStamina - _sprintStaminaCost * Time.deltaTime;
            }
            else
            {
                _characterMovement.SetSprint(false);
            }
        }
        else
        {
            _currentStamina = _currentStamina + _staminaRegenValue * Time.deltaTime;
        }
         // Membatasi stamina player, minimum: 0; maksimum: maximum stamina  
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);
    }
}

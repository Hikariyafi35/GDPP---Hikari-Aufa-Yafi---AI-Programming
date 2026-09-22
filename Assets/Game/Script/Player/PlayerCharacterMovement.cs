using UnityEngine;

public class PlayerCharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    private Vector3 _movementDirection;
    [SerializeField]
    private float _currentSpeed = 1f;
    [Header("Gravity")]
    [SerializeField]
    private float _gravityScale = 1;
    private float _velocityY;
    private Vector3 _velocityXZ;
    private bool _isGrounded;
    [Header("Sprint")]
    private bool _isSprint;
    [SerializeField]
    private float _walkSpeed = 1;
    [SerializeField]
    private float _sprintSpeed = 2;
    [SerializeField]
    private float _acceleration = 0.5f;
    public bool IsSprint => _isSprint;
    // Membuat property untuk menentukan apakah movement sedang aktif
    public bool Enabled { get; private set; } = true;
    [SerializeField] 
    private CharacterController _characterController;

    public void SetMoveDirection(Vector2 inputDirection)
    {
        // Mengisikan arah input sumbu x ke arah gerakan character sumbu x 
        // Mengisikan arah input sumbu y ke arah gerakan character sumbu z 
        _movementDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
    }
    private void Update()
    {
        CheckIsGrounded();
        CalculateAcceleration(); 
        ResetVelocityY(); 
        Move();
    }
    private void CalculateVelocityXZ()
    {
        // Mendapatkan transform camera untuk mendapatkan rotasi camera 
        Transform cameraTransform = Camera.main.transform;
        // Menghitung arah gerakkan sumbu x  
        Vector3 xDirection = _movementDirection.x * cameraTransform.right;
        // Menghitung arah gerakkan sumbu z  
        Vector3 zDirection = _movementDirection.z * cameraTransform.forward;
        // Menggabung arah gerakkan sumbu x dan sumbu z ke dalam satu vector 
        Vector3 direction = xDirection + zDirection;
        // Arah gerakkan y dibuat nol,  
        // karena tidak ada gerakan ke arah atas dan bawah 
        direction.y = 0;
        // Mengisikan arah input sumbu x ke arah gerakan character sumbu x 
        // Mengisikan arah input sumbu y ke arah gerakan character sumbu z 
        if (_movementDirection.magnitude >= 0.01)
        {
            _velocityXZ = direction.normalized * _currentSpeed * Time.deltaTime;
        }
        else
        {
            _velocityXZ = Vector3.zero;
        }
    }
    private void CalculateVelocityY()
    {
        //menghitung kecepatan gerakan arah Y
        _velocityY = _velocityY + Physics.gravity.y * _gravityScale * Time.deltaTime;
    }
    private void ResetVelocityY()
    {
        if(_isGrounded == true && _velocityY < 0)
        {
            _velocityY = -2;
        }
    }
    public void Move()
    {
        if (Enabled == true)
        {
            CalculateVelocityXZ();
            CalculateVelocityY();
            Vector3 velocity = new Vector3(_velocityXZ.x, _velocityY, _velocityXZ.z);
            _characterController.Move(velocity);
        }
    }
    public void SetSprint(bool isSprint)
    {
        _isSprint = isSprint;
        Debug.Log("SetSprint dipanggil: " + _isSprint);
    }
    public void CalculateAcceleration()
    {
        // Mengecek apakah player character bergerak atau tidak 
        if (_movementDirection.magnitude > 0.01)
        {
            // Mengecek apakah player character sprint atau tidak 
            if (_isSprint)
            {
                // Jika sedang sprint maka kecepatan akan bertambah  
                // sebesar nilai acceleration setiap detik 
                _currentSpeed = _currentSpeed + _acceleration * Time.deltaTime;
            }
            else
            {
                // Jika berhenti sprint maka kecepatan akan berkurang  
                // sebesar nilai acceleration setiap detik 
                _currentSpeed = _currentSpeed - _acceleration * Time.deltaTime;
            }
             // Membatasi kecepatan, minimum: walk speed maksimum: sprint speed  
            _currentSpeed = Mathf.Clamp(_currentSpeed, _walkSpeed, _sprintSpeed); 
        }
        else
        {
            _currentSpeed = 0;
        }
    }
    private void CheckIsGrounded()
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        _isGrounded = Physics.CheckSphere(transform.position, 0.5f, groundLayer);
    }
    // Mengubah status aktif movement
    public void SetEnabled(bool isEnabled)
    {
        Enabled = isEnabled;
    }
}

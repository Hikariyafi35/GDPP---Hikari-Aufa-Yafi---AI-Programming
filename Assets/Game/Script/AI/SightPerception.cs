using UnityEngine;

public class SightPerception : MonoBehaviour
{
    // Variable untuk reference ke transform
    // untuk menentukan posisi mata AI
    [SerializeField]
    private Transform _eyePosition;
    // Variable untuk reference ke transform target
    // untuk mengetahui posisi target
    [SerializeField]
    private Transform _target;
    // Variable untuk menentukan jarak pengelihatan AI
    [SerializeField]
    private float _viewDistance = 10f;
    // Variable untuk menentukan sudut pandangan AI
    [SerializeField]
    private float _viewAngle = 70f;
    // Variable untuk layer dari object yang dapat dilihat AI
    [SerializeField]
    private LayerMask _targetLayer;

    // Property untuk menyimpan status
    // apakah AI bisa melihat target 
    public bool CanSeePlayer { get; private set; }
    // Property untuk menyimpan posisi terakhir 
    // target ketika terlihat
    public Vector3 LastSeenPosition { get; private set; }
    private void Update()
    {
        // Memanggil function untuk mengecek apakah
        // target terlihat oleh AI, kemudian memasukkan
        // hasilnya ke dalam property CanSeePlayer
        CanSeePlayer = CheckSight();
    }

    public bool CheckSight()
    {
        // Mengecek jika tidak ada target
        // maka function akan berhenti dan mengembalikan
        // nilai false (target tidak terdeteksi)
        if (_target == null)
        {
            return false;
        }
    
        // Melakukan pengecekan jarak dari mata AI 
        // ke posisi target
        float distance = Vector3.Distance(_eyePosition.position, _target.position);
        // Mengecek apakah target ada 
        // di dalam jarak pandang AI
        if (distance > _viewDistance)
        {
            // Jika iya, maka function akan berhenti dan 
            // mengembalikan nilai false (target tidak terdeteksi)
            return false;
        }
    
        // Mendapatkan arah ke target dari posisi mata AI
        Vector3 dirToTarget = _target.position - _eyePosition.position;
        // Menentukan sudut dari arah depan mata ke arah target
        float angle = Vector3.Angle(_eyePosition.forward, dirToTarget);
        // Mengecek apakah target ada di dalam 
        // sudut pandang AI
        if (angle > _viewAngle * 0.5f)
        {
            // Jika iya, maka function akan berhenti dan 
            // mengembalikan nilai false (target tidak terdeteksi)
            return false;
        }
    
        // Mengecek dengan detector berbentuk garis dari posisi mata AI
        // ke arah depan, dengan jarak pandang AI.
        // Object yang dideteksi menggunakan layer yang ditentukan pada 
        // variable _targetLayer.
        // Hasil pengecekan akan dimasukkan ke dalam variable isSeeTarget
        bool isSeeTarget = Physics.Raycast(_eyePosition.position,
                                            dirToTarget.normalized, 
                                            out RaycastHit hit, 
                                            _viewDistance, 
                                            _targetLayer);
        // Jika ada object terlihat
        if (isSeeTarget == true)
        {
            // Memastikan jika object yang terlihat adalah target
            if (hit.transform == _target)
            {
                // Jika iya, maka simpan posisi terakhir dari target
                // ke property LastSeenPosition.
                LastSeenPosition = _target.position;
                // Function akan berhenti dan mengembalikan
                // nilai true (target terdeteksi)
                return true;
            }
        }
        // Kembalikan nilai false (target tidak terdeteksi)
        // jika target tidak terdeteksi
        return false;
    }
}

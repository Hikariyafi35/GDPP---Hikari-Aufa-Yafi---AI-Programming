using System.Collections;
using UnityEngine;

public class HidingCloset : MonoBehaviour, IInteractable
{
    // Variable menentukan nama object
    [SerializeField]
    private string _name;
    // Variable untuk reference transform posisi sembunyu
    [SerializeField]
    private Transform _hidePosition;
    // Variable untuk reference transform posisi keluar lemari
    [SerializeField]
    private Transform _unhidePosition;
    // Variable untuk menentukan durasi masuk dan keluar lemari
    [SerializeField]
    private float _duration = 1;
    // Variable untuk reference object door lemari
    [SerializeField]
    private Door _door;

    // Variable untuk reference PlayerCharacter yang sedang sembunyi
    private PlayerCharacter _hidingPlayer;
    private Coroutine _hideCoroutine;
    // Variable untuk menyimpan coroutine animasi unhide
    private Coroutine _unhideCoroutine;

    // Property untuk mengakses variable _name
    public string Name => _name;

    // Function yang akan dipanggil ketika player interaksi 
    // dengan hiding closet
    public void Interact(PlayerCharacter character)
    {
        // Memastikan ada reference posisi hide, posisi unhide,
        // dan door
        if (_hidePosition != null && _unhidePosition != null && _door != null)
        {
            // Memasukan reference player character yang berinteraksi
            // sebagai karakter yang sembunyi
            _hidingPlayer = character;
            // Mengecek apakah ada coroutine hide yang sedang berjalan
            if (_hideCoroutine != null)
            {
                // Jika iya, maka hentikan coroutine
                StopCoroutine(_hideCoroutine);
            }
            // Jalankan coroutine hide untuk menganimasikan proses sembunyi
            _hideCoroutine = StartCoroutine(Hide());
        }
    }
    public IEnumerator Hide()
    {
        // Membuat status player menjadi hiding
        _hidingPlayer.SetIsHiding(true);
        // Menonaktifkan input camera
        _hidingPlayer.Camera.SetCameraInputEnabled(false);
        // Menonaktifkan movement player
        _hidingPlayer.Movement.SetEnabled(false);
        // Menonaktifkan interaksi player
        _hidingPlayer.InteractDetector.SetEnabled(false);
        // Reset camera, supaya menghadap ke tengah dan ke depan
        _hidingPlayer.Camera.ResetCameraRotation();

        // Membuka pintu lemari
        _door.Open();
        // Menunggu selama masih menjalankan animasi buka pintu
        yield return new WaitWhile(() => _door.IsAnimating);

        // Membuat variable untuk menghitung waktu animasi
        float time = 0f;
        // Membuat variable untuk menyimpan posisi awal player
        Vector3 startPosition = _hidingPlayer.transform.position;
        // Membuat variable untuk menyimpan rotasi awal camera
        float startRotation = _hidingPlayer.Camera.PanAxis;
        // Melakukan proses pengulangan (animasi) 
        // selama waktu animasi kurang dari durasi animasi 
        while (time < _duration)
        {
            // Menambahkan waktu animasi dengan satu setiap detik
            time = time + Time.deltaTime;
            // Melakukan interpolasi posisi awal ke posisi target(hide position) 
            // Menentukan alpha dengan rumus time/duration
            // alpha bernilai 0 s.d 1, alpha merupakan nilai yang dianimasikan
            // 0 => posisi awal, 1 => posisi target 
            _hidingPlayer.transform.position = Vector3.Lerp(startPosition,
                                            _hidePosition.position,
                                            time / _duration);
            // Melakukan interpolasi sudut rotasi awal 
            // ke sudut rotasi target(hide position) 
            // Menentukan alpha dengan rumus time/duration
            // alpha bernilai 0 s.d 1, alpha merupakan nilai yang dianimasikan
            // 0 => sudut rotasi awal, 1 => sudut rotasi target 
            float panAxis = Mathf.Lerp(startRotation,
                                    _hidePosition.eulerAngles.y,
                                    time / _duration);
            // Mengubah rotasi pan camera dengan sudut rotasi yang dihitung
            // menggunakan interpolasi  
            _hidingPlayer.Camera.SetPanAxisValue(panAxis);
            // Animasi dijalankan setiap frame 
            yield return null;
        }
        // Memaksa posisi player ke posisi hide position
        // setelah selesai animasi
        _hidingPlayer.transform.position = _hidePosition.position;
        // Memaksa rotasi player ke sudut rotasi hide position
        // setelah selesai animasi
        _hidingPlayer.transform.rotation = _hidePosition.rotation;

        // Menutup pintu lemari
        _door.Close();

        // Menunggu selama masih menjalankan animasi tutup pintu
        yield return new WaitWhile(() => _door.IsAnimating);
        // listen function StopHiding dari event input interact
        _hidingPlayer.Input.OnInteractInput.AddListener(StopHiding);
    }
    public IEnumerator Unhide()
    {
        // Membuka pintu lemari
        _door.Open();
        // Menunggu selama masih menjalankan animasi buka pintu
        yield return new WaitWhile(() => _door.IsAnimating);

        // Membuat variable untuk menghitung waktu animasi
        float time = 0f;
        // Membuat variable untuk menyimpan posisi awal player
        Vector3 startPosition = _hidingPlayer.transform.position;
        // Membuat variable untuk menyimpan rotasi awal camera
        float startRotation = _hidingPlayer.Camera.PanAxis;
        // Melakukan proses pengulangan (animasi) 
        // selama waktu animasi kurang dari durasi animasi 
        while (time < _duration)
        {
            // Menambahkan waktu animasi dengan satu setiap detik
            time = time + Time.deltaTime;
            // Melakukan interpolasi posisi awal ke posisi target(unhide position) 
            // Menentukan alpha dengan rumus time/duration
            // alpha bernilai 0 s.d 1, alpha merupakan nilai yang dianimasikan
            // 0 => posisi awal, 1 => posisi target 
            _hidingPlayer.transform.position = Vector3.Lerp(startPosition,
                                                        _unhidePosition.position,
                                                        time / _duration);
            // Melakukan interpolasi sudut rotasi awal 
            // ke sudut rotasi target(unhide position) 
            // Menentukan alpha dengan rumus time/duration
            // alpha bernilai 0 s.d 1, alpha merupakan nilai yang dianimasikan
            // 0 => sudut rotasi awal, 1 => sudut rotasi target 
            float panAxis = Mathf.Lerp(startRotation,
                                    _unhidePosition.rotation.y,
                                    time / _duration);
            // Mengubah rotasi pan camera dengan sudut rotasi yang dihitung
            // menggunakan interpolasi  
            _hidingPlayer.Camera.SetPanAxisValue(panAxis);
            yield return null;
        }
        // Memaksa posisi player ke posisi unhide position
        // setelah selesai animasi
        _hidingPlayer.transform.position = _unhidePosition.position;
        // Memaksa rotasi player ke sudut rotasi unhide position
        // setelah selesai animasi
        _hidingPlayer.transform.rotation = _unhidePosition.rotation;

        // Menutup pintu lemari
        _door.Close();

        // Mengaktifkan kembali input camera
        _hidingPlayer.Camera.SetCameraInputEnabled(true);
        // Mengaktifkan kembali movement player
        _hidingPlayer.Movement.SetEnabled(true);
        // Mengaktifkan kembali interaksi player
        _hidingPlayer.InteractDetector.SetEnabled(true);
        // Membuat status player menjadi tidak hiding
        _hidingPlayer.SetIsHiding(false);
        // Mengosongkan kembali reference ke player yang hiding
        _hidingPlayer = null;

        // Menunggu selama masih menjalankan animasi tutup pintu
        yield return new WaitWhile(() => _door.IsAnimating);
        _hidingPlayer.Input.OnInteractInput.RemoveListener(StopHiding);
    }
        public void StopHiding()
    {
        // Mengecek apakah ada coroutine unhide yang sedang berjalan
        if (_unhideCoroutine != null)
        {
            // Jika iya, maka hentikan coroutine
            StopCoroutine(_unhideCoroutine);
        }
        // Jalankan coroutine unhide untuk menganimasikan proses keluar lemari
        StartCoroutine(Unhide());
    }
}
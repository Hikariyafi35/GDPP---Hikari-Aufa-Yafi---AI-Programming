using System.Collections;
using UnityEngine;

public class RotatingDoor : Door
{
    // Variable untuk menentukan sudut rotasi ketika pintu dibuka
    [SerializeField]
    private float _openAngle;
    [SerializeField]
    private float _closedAngle;

    public override void Open()
    {
        // Mengecek apakah ada coroutine animasi door yang sedang jalan
        if (_animatingDoorCoroutine != null)
        {
            // Jika ada, maka hentikan coroutine
            StopCoroutine(_animatingDoorCoroutine);
        }

        // Menjalankan coroutine untuk menganimasikan putaran pintu
        // Parameter diisi dengan sudut pintu terbuka sebagai target
        _animatingDoorCoroutine = StartCoroutine(RotateDoor(_openAngle));
        
        // Kode untuk melakukan sesuatu sebelum pintu terbuka
        // Memanggil function Open dari class base/parent (Door)
        // Sehingga kode di dalam function Open yang ada di parent 
        // akan tetap dijalankan 
        base.Open();
        // Kode untuk melakukan sesuatu sesudah pintu terbuka
    }
    // Override function close untuk mengubah perilaku ketika menutup pintu
    public override void Close()
    {
        // Mengecek apakah ada coroutine animasi door yang sedang jalan
        if (_animatingDoorCoroutine != null)
        {
            // Jika ada, maka hentikan coroutine
            StopCoroutine(_animatingDoorCoroutine);
        }
        // Menjalankan coroutine untuk menganimasikan putaran pintu
        // Parameter diisi dengan sudut pintu tertutup sebagai target
        _animatingDoorCoroutine = StartCoroutine(RotateDoor(_closedAngle));

        // Kode untuk melakukan sesuatu sebelum pintu tertutup
        // Memanggil function Close dari class base/parent (Door)
        // Sehingga kode di dalam function Close yang ada di parent 
        // akan tetap dijalankan 
        base.Close();
        // Kode untuk melakukan sesuatu sesudah pintu tertutup
    }
    private IEnumerator RotateDoor(float targetAngle)
    {
        // Mengubah status bahwa animasi memutar pintu sedang berjalan
        _isAnimating = true;
        // Menentukan sudut awal => rotasi pintu saat ini di sumbu y
        float startAngle = _doorTransform.localEulerAngles.y;
        // Menyediakan variable untuk menghitung waktu animasi yang sedang berjalan
        float time = 0;
        // Melakukan proses pengulangan (animasi) 
        // selama waktu animasi kurang dari durasi animasi 
        while (time < _duration)
        {
            // Menambahkan time dengan satu setiap detik 
            time = time + Time.deltaTime;
            // Melakukan interpolasi sudut awal ke sudut target 
            // Menentukan alpha dengan rumus time/duration
            // alpha bernilai 0 s.d 1, alpha merupakan nilai yang dianimasikan
            // 0 => sudut rotasi awal, 1 => sudut rotasi akhir 
            float angle = Mathf.LerpAngle(startAngle,
                                        targetAngle,
                                        time / _duration);
            // Mengubah rotasi sumbu Y pintu dengan sudut yang sudah dihitung
            _doorTransform.localRotation = Quaternion.Euler(0f, angle, 0f);
            // Animasi dijalankan setiap frame 
            yield return null;
        }
        // Setelah animasi selesai, memastika rotasi pintu di sumbu Y
        // mencapai sudut target
        _doorTransform.localRotation = Quaternion.Euler(0f, targetAngle, 0);
        // Mengubah status bahwa animasi memutar pintu sudah selesai
        _isAnimating = false;
    }

}

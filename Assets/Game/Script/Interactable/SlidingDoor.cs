using System.Collections;
using UnityEngine;

public class SlidingDoor : Door
{
    // Variable untuk menentukan posisi ketika pintu dibuka
    [SerializeField]
    private Vector3 _openPosition;
    // Variable untuk menentukan posisi ketika pintu ditutup
    [SerializeField]
    private Vector3 _closedPosition;

    // Override function open untuk mengubah perilaku ketika membuka pintu
    public override void Open()
    {
        // Mengecek apakah ada coroutine animasi door yang sedang jalan
        if (_animatingDoorCoroutine != null)
        {
            // Jika ada, maka hentikan coroutine
            StopCoroutine(_animatingDoorCoroutine);
        }
        // Menjalankan coroutine untuk menganimasikan posisi pintu
        // Parameter diisi dengan posisi pintu terbuka sebagai target
        _animatingDoorCoroutine = StartCoroutine(SlideDoor(_openPosition));
        // Memanggil function Open dari class base/parent (Door)
        // Sehingga kode di dalam function Open yang ada 
        // di parent akan tetap dijalankan 
        base.Open();
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
        // Menjalankan coroutine untuk menganimasikan posisi pintu
        // Parameter diisi dengan posisi pintu tertutup sebagai target
        _animatingDoorCoroutine = StartCoroutine(SlideDoor(_closedPosition));
        // Memanggil function Close dari class base/parent (Door)
        // Sehingga kode di dalam function Close yang ada 
        // di parent akan tetap dijalankan 
        base.Close();
    }

    // Membuat function IEnumerator untuk menganimasikan posisi pintu
    // Menyediakan parameter untuk menentukan target posisi pintu
    private IEnumerator SlideDoor(Vector3 targetPosition)
    {
        // Mengubah status bahwa animasi memutar pintu sedang berjalan
        _isAnimating = true;
        // Menentukan sudut awal => rotasi pintu saat ini di sumbu y
        Vector3 startPosition = _doorTransform.localPosition;
        // Menyediakan variable untuk menghitung waktu animasi yang sedang berjalan
        float time = 0f;

        // Melakukan proses pengulangan (animasi) 
        // selama waktu animasi kurang dari durasi animasi 
        while (time < _duration)
        {
            // Menambahkan time dengan satu setiap detik 
            time = time + Time.deltaTime;
            // Melakukan interpolasi posisi awal ke posisi target 
            // Menentukan alpha dengan rumus time/duration
            // alpha bernilai 0 s.d 1, alpha merupakan nilai yang dianimasikan
            // 0 => posisi awal, 1 => posisi akhir 
            Vector3 position = Vector3.Lerp(startPosition, 
                                            targetPosition, 
                                            time / _duration);
            // Mengubah posisi pintu dengan posisi yang sudah dihitung
            _doorTransform.localPosition = position;
            // Animasi dijalankan setiap frame 
            yield return null;
        }

        // Setelah animasi selesai, memastika rotasi pintu di sumbu Y
        // mencapai sudut target
        _doorTransform.localPosition = targetPosition;
        // Mengubah status bahwa animasi memutar pintu sudah selesai
        _isAnimating = false;
    }
}

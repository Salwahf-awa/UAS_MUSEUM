using UnityEngine;
using System.Collections; // Wajib ada untuk menjalankan jeda waktu (Coroutine)

public class AreaTrigger : MonoBehaviour
{
    [Header("UI Text Menyambut")]
    public GameObject welcomeTextUI;

    [Header("Berapa Detik Teks Muncul")]
    public float durasiTampil = 5.0f; 

    private bool sudahPernahDitabrak = false;

    private void OnTriggerEnter(Collider other)
    {
        // Mengecek apakah yang menabrak adalah player dan belum pernah memicu trigger ini sebelumnya
        if (!sudahPernahDitabrak && (other.CompareTag("Player") || other.GetComponent<CharacterController>() != null))
        {
            sudahPernahDitabrak = true; // Kunci agar teks tidak muncul berulang-ulang kalau player bolak-balik lewat pintu

            if (welcomeTextUI != null)
            {
                welcomeTextUI.SetActive(true);
                Debug.Log("Player masuk area museum dan menabrak trigger!");
                
                // Menjalankan fungsi jeda waktu untuk mematikan teks setelah beberapa detik
                StartCoroutine(MatikanTeksSetelahDurasi());
            }
        }
    }

    private IEnumerator MatikanTeksSetelahDurasi()
    {
        // Menunggu selama durasi yang ditentukan (misalnya 5 detik)
        yield return new WaitForSeconds(durasiTampil);
        
        if (welcomeTextUI != null)
        {
            welcomeTextUI.SetActive(false); // Mematikan teks kembali
            Debug.Log("Teks misi dinonaktifkan secara otomatis.");
        }
    }
}
using UnityEngine;
using TMPro;

public class ExitTrigger : MonoBehaviour
{
    [Header("UI Teks Counter")]
    public TextMeshProUGUI textCounterMisi; // Kolom untuk Teks_Counter_Misi

    [Header("Objek Pintu Keluar")]
    public GameObject objekPintuKeluar;     // Kolom untuk Pintu_keluar

    private MissionManager mManager;

    private void Start()
    {
        mManager = FindObjectOfType<MissionManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Kita cek lewat method bawaan MissionManager (jika ada) atau ganti kondisi ini
            // Agar aman dari error, kita gunakan fungsi cek status selesai yang universal:
            if (mManager != null && textCounterMisi != null && textCounterMisi.text.Contains("6 / 6"))
            {
                // BERHASIL: Buka pintu keluar jika teks UI sudah menunjukkan 6 / 6
                if (objekPintuKeluar != null)
                {
                    objekPintuKeluar.SetActive(false);
                }

                textCounterMisi.text = "SELAMAT! Misi Selesai.\nSilakan Keluar Museum.";
                textCounterMisi.color = Color.green;
            }
            else
            {
                // GAGAL: Pintu masih terkunci
                if (textCounterMisi != null)
                {
                    // Menyimpan teks asli sementara agar angka counter tidak hilang permanen
                    string teksSekarang = textCounterMisi.text;
                    
                    textCounterMisi.text = "PINTU TERKUNCI!\nSelesaikan Misi (6 Karya Seni) Dulu.";
                    textCounterMisi.color = Color.red;
                    
                    // Mengembalikan teks counter setelah 3 detik agar player tahu kurang berapa lagi
                    StartCoroutine(KembalikanTeksCounter(teksSekarang, 3f));
                }
            }
        }
    }

    System.Collections.IEnumerator KembalikanTeksCounter(string teksAsli, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (textCounterMisi != null && !textCounterMisi.text.Contains("SELAMAT"))
        {
            textCounterMisi.text = teksAsli;
            textCounterMisi.color = Color.yellow; // Kembalikan ke warna kuning semula
        }
    }
}
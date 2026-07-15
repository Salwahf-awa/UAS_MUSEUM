using UnityEngine;
using TMPro;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;

    [Header("UI Elements")]
    public TextMeshProUGUI textCounterMisi; 
    public GameObject endStatePanel;

    [Header("Mission Settings")]
    public int totalArtefak = 6; 
    private int artefakDinspeksi = 0;

    [Header("Audio Feedback")]
    public AudioSource sfxSource;
    public AudioClip sfxDing;

    public void PlayCheckSFX()
    {
        if (sfxSource != null && sfxDing != null)
        {
            sfxSource.PlayOneShot(sfxDing);
        }
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (endStatePanel != null) endStatePanel.SetActive(false);
        
        // KUNCI: Matikan teks counter di awal game agar tidak kelihatan saat di luar gedung
        if (textCounterMisi != null)
        {
            textCounterMisi.gameObject.SetActive(false); 
        }
        
        UpdateCounterTextOnly(); 
    }

    // Dipanggil saat Player pertama kali memeriksa objek lukisan
    public void PeriksaObjek()
    {
        artefakDinspeksi++;
        
        if (textCounterMisi != null)
        {
            textCounterMisi.gameObject.SetActive(true);
        }
        
        if (sfxSource != null && sfxDing != null)
        {
            sfxSource.PlayOneShot(sfxDing);
        }
        
        UpdateCounterUI();
    }

    // Menerima parameter dari PlayerInteraction agar tidak error
    public void PeriksaObjek(object objek)
    {
        PeriksaObjek();
    }

    public void ArtefakBerhasilDiinspeksi()
    {
        PeriksaObjek();
    }

    public bool ApakahMisiSelesai()
    {
        return artefakDinspeksi >= totalArtefak;
    }

    public bool IsMisiSelesai()
    {
        return ApakahMisiSelesai();
    }

    // Memperbarui UI Teks Counter (Kuning)
    public void UpdateCounterUI()
    {
        if (textCounterMisi != null)
        {
            // Pastikan hanya aktif jika memang sudah ada lukisan yang diperiksa
            if (artefakDinspeksi > 0)
            {
                textCounterMisi.gameObject.SetActive(true);
            }
            else
            {
                textCounterMisi.gameObject.SetActive(false);
            }
            
            textCounterMisi.color = Color.yellow; 
            textCounterMisi.text = "Karya Seni Diperiksa: " + artefakDinspeksi + " / " + totalArtefak;
        }
    }

    // Hanya merubah isi tulisan di awal tanpa memaksanya muncul di layar
    private void UpdateCounterTextOnly()
    {
        if (textCounterMisi != null)
        {
            textCounterMisi.color = Color.yellow; 
            textCounterMisi.text = "Karya Seni Diperiksa: " + artefakDinspeksi + " / " + totalArtefak;
        }
    }

    // Menimpa teks dengan Peringatan Pintu Merah saat mendekati pintu keluar
    public void TampilkanPeringatanPintu(string pesan)
    {
        if (textCounterMisi != null)
        {
            textCounterMisi.gameObject.SetActive(true); 
            textCounterMisi.color = Color.red; 
            textCounterMisi.text = pesan;
        }
    }

    public void TriggerEndState()
    {
        if (endStatePanel != null)
        {
            endStatePanel.SetActive(true);
            Time.timeScale = 0f; 
        }
    }
}
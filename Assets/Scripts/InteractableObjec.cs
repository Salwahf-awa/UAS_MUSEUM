using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private GameObject petunjukCanvas; // Canvas_Info_Firaun ([E] Lihat Informasi)
    [SerializeField] private GameObject detailCanvas;   // Canvas_Detail_Besar_Firaun (Detail Lengkap)

    private bool isInspecting = false;

    private void Start()
    {
        // Awal game, semua canvas sembunyi
        if (petunjukCanvas != null) petunjukCanvas.SetActive(false);
        if (detailCanvas != null) detailCanvas.SetActive(false);
    }

    // Fungsi saat player mendekat/melihat objek
    public void SetPetunjukActive(bool isActive)
    {
        // Petunjuk HANYA muncul jika player TIDAK sedang membuka detail informasi besar
        if (petunjukCanvas != null)
        {
            petunjukCanvas.SetActive(isActive && !isInspecting);
        }
    }

    // Fungsi ketika tombol E diklik (Gantian muncul/hilang)
    public void ToggleDetailCanvas()
    {
        if (detailCanvas != null && petunjukCanvas != null)
        {
            isInspecting = !isInspecting; // Balik status inspect (true/false)

            // Jalankan logika gantian:
            detailCanvas.SetActive(isInspecting);      // Papan besar muncul jika true
            petunjukCanvas.SetActive(!isInspecting);   // Petunjuk kecil HILANG jika true
        }
    }

    // Fungsi otomatis matikan semua kalau player jalan menjauh
    public void HideAllUI()
    {
        isInspecting = false;
        if (petunjukCanvas != null) petunjukCanvas.SetActive(false);
        if (detailCanvas != null) detailCanvas.SetActive(false);
    }
}
using UnityEngine;
using TMPro;

public class DoorMissionController : MonoBehaviour
{
    [Header("Referensi Misi & Pintu")]
    public MissionManager missionManager; 
    public GameObject objekPintuFisik; // Masukkan objek daun pintunya di sini

    [Header("Pengaturan UI Peringatan")]
    public GameObject teksKawasanUI; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Cek apakah 6 pajangan sudah diperiksa
            if (missionManager != null && missionManager.IsMisiSelesai())
            {
                Debug.Log("Misi selesai! Pintu terbuka.");
                
                // Menyembunyikan pintu fisik agar player bisa lewat keluar
                if (objekPintuFisik != null)
                {
                    objekPintuFisik.SetActive(false); 
                }
                
                // Tampilkan pesan sukses di layar
                if (teksKawasanUI != null)
                {
                    teksKawasanUI.SetActive(true);
                    TextMeshProUGUI komponenTeks = teksKawasanUI.GetComponent<TextMeshProUGUI>();
                    if (komponenTeks != null)
                    {
                        komponenTeks.color = Color.green; // Ubah teks jadi hijau tanda sukses
                        komponenTeks.text = "Museum Tour Complete! Pintu Terbuka.";
                    }
                }
            }
            else
            {
                // Jika BELUM selesai, munculkan teks merah terkunci
                if (teksKawasanUI != null)
                {
                    teksKawasanUI.SetActive(true);
                    TextMeshProUGUI komponenTeks = teksKawasanUI.GetComponent<TextMeshProUGUI>();
                    if (komponenTeks != null)
                    {
                        komponenTeks.color = Color.red;
                        komponenTeks.text = "Selesaikan misi tour terlebih dahulu sebelum keluar!";
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (teksKawasanUI != null)
            {
                teksKawasanUI.SetActive(false);
            }
        }
    }
}
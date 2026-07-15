using UnityEngine;
using TMPro;

public class DoorMissionController : MonoBehaviour
{
    [Header("Referensi Misi & Pintu")]
    public MissionManager missionManager; 
    public GameObject objekPintuFisik; 

    [Header("Pengaturan UI Peringatan")]
    public GameObject teksKawasanUI; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (missionManager != null && missionManager.IsMisiSelesai())
            {
                Debug.Log("Misi selesai! Pintu terbuka.");
                
                if (objekPintuFisik != null)
                {
                    objekPintuFisik.SetActive(false); 
                }
                
                if (teksKawasanUI != null)
                {
                    teksKawasanUI.SetActive(true);
                    TextMeshProUGUI komponenTeks = teksKawasanUI.GetComponent<TextMeshProUGUI>();
                    if (komponenTeks != null)
                    {
                        komponenTeks.color = Color.green;
                        komponenTeks.text = "Museum Tour Complete! Pintu Terbuka.";
                    }
                }
            }
            else
            {
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
            // PENTING: Sembunyikan teks kawasan UI (merah/hijau) saat player menjauh dari pintu
            if (teksKawasanUI != null)
            {
                teksKawasanUI.SetActive(false);
            }

            // Kembalikan ke counter kuning HANYA jika misi BELUM selesai
            if (missionManager != null && !missionManager.IsMisiSelesai())
            {
                missionManager.UpdateCounterUI();
            }
        }
    }
}
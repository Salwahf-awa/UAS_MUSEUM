using UnityEngine;
using TMPro;
using System.Collections;

public class ExitTrigger : MonoBehaviour
{
    [Header("UI Teks Misi")]
    public GameObject welcomeTextUI; // Menghubungkan ke Teks_Misi_Awal di layar

    private TextMeshProUGUI teksMisi;
    private Coroutine hilangkanTeksCoroutine;

    private void Start()
    {
        if (welcomeTextUI != null)
        {
            teksMisi = welcomeTextUI.GetComponent<TextMeshProUGUI>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Mengecek apakah yang menabrak pintu keluar adalah Player
        if (other.CompareTag("Player"))
        {
            if (MissionManager.instance != null)
            {
                // Mengecek ke MissionManager apakah semua lukisan sudah diperiksa
                bool apakahSudahSelesai = MissionManager.instance.IsMisiSelesai();

                if (apakahSudahSelesai)
                {
                    TampilkanTeks("MISI SELESAI!\nTerima kasih telah berkunjung ke museum.", false);
                    Debug.Log("Player berhasil keluar! Game Tamat.");
                }
                else
                {
                    TampilkanTeks("<color=red>Selesaikan misi tour terlebih dahulu sebelum keluar!</color>", true);
                }
            }
        }
    }

    void TampilkanTeks(string pesan, bool autoHide)
    {
        if (teksMisi != null && welcomeTextUI != null)
        {
            if (hilangkanTeksCoroutine != null) StopCoroutine(hilangkanTeksCoroutine);

            teksMisi.text = pesan;
            welcomeTextUI.SetActive(true);

            if (autoHide)
            {
                hilangkanTeksCoroutine = StartCoroutine(HideTextAfterDelay(3f));
            }
        }
    }

    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (welcomeTextUI != null) welcomeTextUI.SetActive(false);
    }
}
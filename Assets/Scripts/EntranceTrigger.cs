using UnityEngine;
using TMPro;
using System.Collections;

public class EntranceTrigger : MonoBehaviour
{
    [Header("UI Teks Misi")]
    public GameObject welcomeTextUI;  
    public GameObject counterTextUI;  
    public float durasiTeks = 5f;     

    [Header("Pengaturan Pintu")]
    public Transform objekPintu;       // Tarik objek 'Door' ke sini
    public float targetRotasiY = 90f;   // Sudut pintu terbuka (90 derajat)
    public float kecepatanBukaTutup = 2f; // Kecepatan gerakan pintu

    private Quaternion rotasiTertutup;
    private Quaternion rotasiTerbuka;
    private Coroutine coroutinePintu;
    private bool teksMisiSudahMuncul = false;

    private void Start()
    {
        if (objekPintu != null)
        {
            rotasiTertutup = objekPintu.rotation;
            rotasiTerbuka = rotasiTertutup * Quaternion.Euler(0, targetRotasiY, 0);
        }
    }

    // 1. KETIKA PLAYER MENDEKATI PINTU (MASUK AREA TRIGGER)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BeralihGerakanPintu(rotasiTerbuka);

            if (!teksMisiSudahMuncul)
            {
                teksMisiSudahMuncul = true;

                if (welcomeTextUI != null)
                {
                    welcomeTextUI.SetActive(true);
                    TextMeshProUGUI teksMisi = welcomeTextUI.GetComponent<TextMeshProUGUI>();
                    if (teksMisi != null)
                    {
                        teksMisi.text = "MISI MUSEUM:\nPeriksalah minimal 6 objek karya seni di galeri!";
                    }
                    StartCoroutine(HilangkanTeksMisiAwal(durasiTeks));
                }

                if (counterTextUI != null)
                {
                    counterTextUI.SetActive(true);
                }
            }
        }
    }

    // 2. KETIKA PLAYER SUDAH MASUK KE DALAM MUSEUM (KELUAR AREA TRIGGER)
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Jalankan gerakan menutup pintu kembali otomatis
            BeralihGerakanPintu(rotasiTertutup);
            
            // PERBAIKAN: Paksa teks misinya langsung hilang di sini saat player masuk
            if (welcomeTextUI != null)
            {
                welcomeTextUI.SetActive(false);
            }
            
            // Setelah pintu ditutup dan teks dimatikan, baru script ini dihancurkan
            Destroy(this); 
        }
    }

    private void BeralihGerakanPintu(Quaternion rotasiTarget)
    {
        if (coroutinePintu != null)
        {
            StopCoroutine(coroutinePintu);
        }
        if (objekPintu != null)
        {
            coroutinePintu = StartCoroutine(GerakkanPintu(rotasiTarget));
        }
    }

    IEnumerator GerakkanPintu(Quaternion target)
    {
        float waktu = 0;
        Quaternion rotasiSekarang = objekPintu.rotation;

        while (waktu < 1f)
        {
            waktu += Time.deltaTime * kecepatanBukaTutup;
            objekPintu.rotation = Quaternion.Slerp(rotasiSekarang, target, waktu);
            yield return null;
        }
    }

    IEnumerator HilangkanTeksMisiAwal(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (welcomeTextUI != null)
        {
            welcomeTextUI.SetActive(false);
        }
    }
}
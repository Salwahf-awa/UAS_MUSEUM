using UnityEngine;
using TMPro;

public class ExitTrigger : MonoBehaviour
{
    private MissionManager missionManager;

    void Start()
    {
        missionManager = MissionManager.instance;

        if (missionManager == null)
        {
            missionManager = Object.FindAnyObjectByType<MissionManager>();
            if (missionManager == null)
            {
                missionManager = Object.FindFirstObjectByType<MissionManager>();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (missionManager != null && missionManager.IsMisiSelesai())
            {
                missionManager.TriggerEndState();
            }
            else if (missionManager != null)
            {
                missionManager.TampilkanPeringatanPintu("Selesaikan misi tour terlebih dahulu sebelum keluar!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (missionManager != null)
            {
                // Jika misi SUDAH selesai, sembunyikan text counter secara permanen
                if (missionManager.IsMisiSelesai())
                {
                    if (missionManager.textCounterMisi != null)
                    {
                        missionManager.textCounterMisi.gameObject.SetActive(false);
                    }
                }
                // Jika misi BELUM selesai, kembalikan ke counter kuning
                else
                {
                    missionManager.UpdateCounterUI();
                }
            }
        }
    }
}
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;

    [Header("UI Teks Counter")]
    public TextMeshProUGUI textCounterMisi;

    private int jumlahObjekDiperiksa = 0;
    private int totalTargetObjek = 6;
    
    // Untuk mencatat objek agar tidak dihitung berkali-kali jika didekati ulang
    private HashSet<string> objekSudahDilihat = new HashSet<string>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        UpdateTeksUI();
    }

    public void PeriksaObjek(string namaObjek)
    {
        if (!objekSudahDilihat.Contains(namaObjek))
        {
            objekSudahDilihat.Add(namaObjek);
            jumlahObjekDiperiksa++;
            UpdateTeksUI();

            if (jumlahObjekDiperiksa >= totalTargetObjek)
            {
                MisiSelesai();
            }
        }
    }

    void UpdateTeksUI()
    {
        if (textCounterMisi != null)
        {
            textCounterMisi.text = "Karya Seni Diperiksa: " + jumlahObjekDiperiksa + " / " + totalTargetObjek;
        }
    }

    void MisiSelesai()
    {
        Debug.Log("Selamat! Seluruh karya seni sudah diperiksa.");
        // Tempat kamu bisa menambahkan aksi kalau misi selesai, misal muncul teks "MISI SELESAI"
    }

    // --- TAMBAHAN FUNGSI UNTUK CEK STATUS MISI DARI EXIT TRIGGER ---
    public bool IsMisiSelesai()
    {
        return jumlahObjekDiperiksa >= totalTargetObjek;
    }
}
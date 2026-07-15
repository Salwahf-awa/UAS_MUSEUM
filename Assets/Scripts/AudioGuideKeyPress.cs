using UnityEngine;
using TMPro;

public class AudioGuideKeyPress : MonoBehaviour
{
    [Header("Audio & Subtitle")]
    public AudioSource audioSource;
    [TextArea(2, 5)]
    public string subtitleText = "Ini adalah narasi objek...";
    public GameObject subtitleUI;

    [Header("Pengaturan Tombol")]
    public KeyCode tombolInteraksi = KeyCode.Q;
    
    [Header("Petunjuk Prompt (opsional)")]
    public GameObject promptUI; // contoh: teks kecil "[Q] Dengarkan Audio Guide"

    private bool playerInRange = false;
    private bool isPlaying = false;

    void Start()
    {
        if (subtitleUI != null) subtitleUI.SetActive(false);
        if (promptUI != null) promptUI.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(tombolInteraksi))
        {
            ToggleAudioGuide();
        }
    }

void ToggleAudioGuide()
{
    isPlaying = !isPlaying;
    Debug.Log("isPlaying sekarang: " + isPlaying + " | promptUI: " + (promptUI != null ? promptUI.name : "NULL"));

    if (isPlaying)
    {
        if (audioSource != null) audioSource.Play();
        if (subtitleUI != null)
        {
            subtitleUI.SetActive(true);
            TextMeshProUGUI teks = subtitleUI.GetComponent<TextMeshProUGUI>();
            if (teks != null) teks.text = subtitleText;
        }
        if (promptUI != null) promptUI.SetActive(false);
    }
    else
    {
        if (audioSource != null) audioSource.Stop();
        if (subtitleUI != null) subtitleUI.SetActive(false);
        if (promptUI != null) promptUI.SetActive(true);
    }
}

void OnTriggerEnter(Collider other)
{
    Debug.Log("Trigger kena: " + other.name);
    if (other.CompareTag("Player"))
    {
        playerInRange = true;
        if (!isPlaying && promptUI != null) promptUI.SetActive(true);
    }
}

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (promptUI != null) promptUI.SetActive(false);

            // Otomatis stop audio & subtitle kalau Player menjauh
            if (isPlaying)
            {
                isPlaying = false;
                if (audioSource != null) audioSource.Stop();
                if (subtitleUI != null) subtitleUI.SetActive(false);
            }
        }
    }
}
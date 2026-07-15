using UnityEngine;
using TMPro;

public class AudioZoneTrigger : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Pengaturan UI Teks")]
    public GameObject teksKawasanUI; 
    [TextArea(2, 5)]
    public string pesanKawasan = "Ini kawasan blablabla..."; 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }

            if (teksKawasanUI != null)
            {
                teksKawasanUI.SetActive(true);
                TextMeshProUGUI komponenTeks = teksKawasanUI.GetComponent<TextMeshProUGUI>();
                if (komponenTeks != null)
                {
                    komponenTeks.text = pesanKawasan;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioSource != null)
            {
                audioSource.Stop();
            }

            if (teksKawasanUI != null)
            {
                teksKawasanUI.SetActive(false);
            }
        }
    }
}
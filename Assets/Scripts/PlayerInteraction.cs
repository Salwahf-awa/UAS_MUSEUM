using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private Transform cameraTransform; 
    [SerializeField] private float interactDistance = 5f; 
    [SerializeField] private LayerMask interactableLayer; 

    private InteractableObject lastDetectedObject; 

    void Update()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
        {
            InteractableObject obj = hit.collider.GetComponent<InteractableObject>();
            
            if (obj != null)
            {
                lastDetectedObject = obj;

                // 1. JIKA MENDEKAT: Otomatis munculkan petunjuk kecil [E] saja
                obj.SetPetunjukActive(true);

                // 2. JIKA TEKAN TOMBOL E: Papan informasi besar baru keluar/hilang
                if (Input.GetKeyDown(KeyCode.E))
                {
                    obj.ToggleDetailCanvas();

                    // --- TAMBAHAN KODE COUNTER MISI ---
                    // Mengirimkan nama objek lukisan/patung ini ke MissionManager
                    if (MissionManager.instance != null)
                    {
                        MissionManager.instance.PeriksaObjek(obj.gameObject.name);
                    }
                    // ----------------------------------
                }
            }
        }
        else
        {
            // 3. JIKA MENJAUH: Semua teks otomatis hilang bersih
            if (lastDetectedObject != null)
            {
                lastDetectedObject.HideAllUI();
                lastDetectedObject = null;
            }
        }
    }
}
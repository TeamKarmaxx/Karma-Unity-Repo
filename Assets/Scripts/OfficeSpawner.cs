using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class OfficeSpawner : MonoBehaviour
{
    public GameObject officePrefab;
    public GameObject[] hazardPrefabs;

    private GameObject spawnedOffice;

    public void SpawnOffice(Vector3 pos, Quaternion rot)
    {
        if (spawnedOffice != null) return;

        if (officePrefab != null)
        {
            spawnedOffice = Instantiate(officePrefab, pos, rot);
            spawnedOffice.AddComponent<ARAnchor>();
        }

        // TODO: FireHazardManager banane ke baad uncomment karenge
        // FireHazardManager fhm = FindObjectOfType<FireHazardManager>();
        // if (fhm != null)
        //     fhm.SpawnHazards(spawnedOffice.transform, hazardPrefabs);

        // TODO: ARPlacementManager ka officePlaced flag set karenge
        ARPlacementManager apm = FindAnyObjectByType<ARPlacementManager>();
        if (apm != null)
            apm.officePlaced = true;

        // TODO: TrainingUIManager banane ke baad uncomment karenge
        // FindObjectOfType<TrainingUIManager>().ShowPlaceOfficeButton(false);
        // FindObjectOfType<TrainingUIManager>().ShowFireWarning(true);
    }
}
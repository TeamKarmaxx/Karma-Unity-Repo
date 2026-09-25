using System.Collections.Generic;
using UnityEngine;

public class FireHazardManager : MonoBehaviour
{
    public List<FireHazard> activeHazards = new List<FireHazard>();
    public FireHazard currentHazard;

    public void SpawnHazards(Transform officeRoot, GameObject[] hazardPrefabs)
    {
        activeHazards.Clear();

        if (hazardPrefabs == null || hazardPrefabs.Length == 0)
        {
            Debug.LogWarning("FireHazardManager: No hazard prefabs assigned!");
            return;
        }

        GameObject h = Instantiate(hazardPrefabs[0], officeRoot);
        h.transform.localPosition = new Vector3(1.5f, 0.8f, 1.5f);
        FireHazard fh = h.GetComponent<FireHazard>();

        if (fh != null)
        {
            activeHazards.Add(fh);
            currentHazard = fh;
        }
    }

    public FireType GetCurrentFireType()
    {
        return currentHazard != null ? currentHazard.fireType : FireType.Paper;
    }

    public FireHazard GetCurrentHazard() => currentHazard;
}
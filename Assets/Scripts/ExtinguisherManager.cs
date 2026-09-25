using UnityEngine;

public enum ExtinguisherType { Water, DryChemical, CO2 }

public class ExtinguisherManager : MonoBehaviour
{
    public GameObject waterExtinguisher;
    public GameObject dryChemicalExtinguisher;
    public GameObject co2Extinguisher;

    public ExtinguisherType selectedType;
    public bool correctSelected = false;
    public GameObject activeExtinguisher;

    public void SelectExtinguisher(ExtinguisherType type)
    {
        selectedType = type;

        FireHazardManager fhm = FindObjectOfType<FireHazardManager>();
        if (fhm == null) return;

        FireType fire = fhm.GetCurrentFireType();

        // Fire type ke hisaab se extinguisher check
        bool correct = (fire == FireType.Paper && type == ExtinguisherType.DryChemical)
                    || (fire == FireType.Electrical && type == ExtinguisherType.CO2)
                    || (fire == FireType.ServerRoom && type == ExtinguisherType.CO2);

        correctSelected = correct;

        // TODO: TrainingUIManager banane ke baad uncomment karenge
        // FindObjectOfType<TrainingUIManager>().ShowExtinguisherFeedback(correct);

        if (correct)
        {
            SpawnInHand(type);
        }
    }

    void SpawnInHand(ExtinguisherType type)
    {
        GameObject prefab = type switch
        {
            ExtinguisherType.Water => waterExtinguisher,
            ExtinguisherType.DryChemical => dryChemicalExtinguisher,
            ExtinguisherType.CO2 => co2Extinguisher,
            _ => null
        };

        if (prefab == null)
        {
            Debug.LogWarning("ExtinguisherManager: Prefab not assigned for " + type);
            return;
        }

        if (activeExtinguisher != null) Destroy(activeExtinguisher);

        Transform cam = Camera.main.transform;
        activeExtinguisher = Instantiate(prefab, cam);
        activeExtinguisher.transform.localPosition = new Vector3(0.3f, -0.3f, 0.6f);
        activeExtinguisher.transform.localRotation = Quaternion.Euler(0, -20, 0);
        activeExtinguisher.transform.localScale = Vector3.one * 0.3f;

        // TODO: TrainingUIManager banane ke baad uncomment karenge
        // FindObjectOfType<TrainingUIManager>().ShowSprayButton(true);
    }
}
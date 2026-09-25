using UnityEngine;

public enum FireType { Paper, Electrical, ServerRoom }

public class FireHazard : MonoBehaviour
{
    public FireType fireType;
    public ParticleSystem flameParticles;
    public ParticleSystem smokeParticles;
    public GameObject warningIcon;
    public GameObject targetHighlight;
    public float fireLevel = 100f;
    public float decreaseRate = 20f;

    private bool isBeingSprayed = false;

    void Update()
    {
        if (isBeingSprayed && fireLevel > 0)
        {
            fireLevel -= decreaseRate * Time.deltaTime;
            fireLevel = Mathf.Clamp(fireLevel, 0, 100);
            UpdateVisuals();

            if (fireLevel <= 0) Extinguish();
        }
    }

    public void SetSpraying(bool spraying) => isBeingSprayed = spraying;

    void UpdateVisuals()
    {
        if (flameParticles != null)
        {
            var em = flameParticles.emission;
            em.rateOverTime = Mathf.Lerp(0, 80, fireLevel / 100f);
        }

        if (smokeParticles != null)
        {
            var sm = smokeParticles.emission;
            sm.rateOverTime = Mathf.Lerp(0, 30, fireLevel / 100f);
        }

        // TODO: TrainingUIManager banane ke baad uncomment karenge
        // FindObjectOfType<TrainingUIManager>().UpdateFireProgress(fireLevel);
    }

    void Extinguish()
    {
        if (flameParticles != null) flameParticles.Stop();
        if (smokeParticles != null) smokeParticles.Stop();
        if (warningIcon) warningIcon.SetActive(false);
        if (targetHighlight) targetHighlight.SetActive(false);

        // TODO: TrainingUIManager aur TrainingManager banane ke baad uncomment karenge
        // FindObjectOfType<TrainingUIManager>().ShowFireExtinguished();
        // FindObjectOfType<TrainingManager>().OnFireExtinguished();
    }

    public void HighlightTarget(bool on)
    {
        if (targetHighlight) targetHighlight.SetActive(on);
    }
}
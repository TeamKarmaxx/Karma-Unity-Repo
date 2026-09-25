using UnityEngine;

public class FireSprayController : MonoBehaviour
{
    public ParticleSystem sprayParticles;
    public FireAimController aimController;
    private bool spraying = false;

    public void StartSpray()
    {
        ExtinguisherManager em = FindObjectOfType<ExtinguisherManager>();
        if (em == null || !em.correctSelected) return;

        spraying = true;
        if (sprayParticles != null) sprayParticles.Play();
    }

    public void StopSpray()
    {
        spraying = false;
        if (sprayParticles != null) sprayParticles.Stop();

        if (aimController != null)
        {
            FireHazard h = aimController.GetAimedHazard();
            if (h != null) h.SetSpraying(false);
        }
    }

    void Update()
    {
        if (!spraying) return;

        if (aimController == null) return;

        FireHazard h = aimController.GetAimedHazard();
        if (h != null && h.fireLevel > 0) h.SetSpraying(true);
        else if (h != null) h.SetSpraying(false);
    }
}
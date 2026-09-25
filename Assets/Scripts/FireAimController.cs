using UnityEngine;
using UnityEngine.UI;

public class FireAimController : MonoBehaviour
{
    public float aimRange = 20f;
    public LayerMask fireLayer;
    public GameObject reticle;

    private FireHazard aimedHazard;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, aimRange, fireLayer))
        {
            FireHazard fh = hit.collider.GetComponentInParent<FireHazard>();
            if (fh != null && fh.fireLevel > 0)
            {
                aimedHazard = fh;
                fh.HighlightTarget(true);
                SetReticleColor(Color.green);
                return;
            }
        }

        if (aimedHazard != null) aimedHazard.HighlightTarget(false);
        aimedHazard = null;
        SetReticleColor(Color.white);
    }

    void SetReticleColor(Color c)
    {
        if (reticle == null) return;

        Image img = reticle.GetComponent<Image>();
        if (img != null) img.color = c;
    }

    public FireHazard GetAimedHazard() => aimedHazard;
}
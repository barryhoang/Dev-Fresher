using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickSpawnVfx : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ParticleSystem hoverParticle;
    [SerializeField] private Button button;

    private void Start()
    {
        if (hoverParticle != null)
        {
            hoverParticle.Stop();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverParticle != null)
        {
            hoverParticle.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverParticle != null)
        {
            hoverParticle.Stop();
        }
    }
}

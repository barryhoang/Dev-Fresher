using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickSpawnVfx : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ParticleSystem hoverParticle; // Kéo và thả Particle System từ thư mục Assets vào trường này trong Inspector
    [SerializeField] private Button button;

    private void Start()
    {
        
        // Make sure hoverParticle is not null and is not playing from the start
        if (hoverParticle != null)
        {
            hoverParticle.Stop();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Start particle effect when mouse hovers over the button
        if (hoverParticle != null)
        {
            hoverParticle.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Stop particle effect when mouse exits the button
        if (hoverParticle != null)
        {
            hoverParticle.Stop();
        }
    }
}

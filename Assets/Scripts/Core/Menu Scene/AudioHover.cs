using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioHover : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioSource buttonAudio;
    [SerializeField] private AudioClip _audio;

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonAudio.PlayOneShot(_audio);
    }
}

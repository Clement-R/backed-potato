using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonInteractionEvents : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler
{
    // Set those in the inspector or via AddListener exactly the same as onClick of a button
    public UnityEvent onPointerEnter;
    public UnityEvent onPointerUp;

    public AudioClip hoverClip;
    public AudioClip clickClip;

    public AudioSource audioSource;

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(hoverClip);
        onPointerEnter.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        audioSource.PlayOneShot(clickClip);
        onPointerUp.Invoke();
    }

}
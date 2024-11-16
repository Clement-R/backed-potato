using UnityEngine;

public class FileAnimationEvents : MonoBehaviour
{
    public GameObject spotLight;

    private void Start()
    {
        spotLight.SetActive(false);
    }
    public void OnAnimationEvent()
    {
        spotLight.SetActive(true);
    }
}

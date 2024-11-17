using UnityEngine;

public class FileAnimationEvents : MonoBehaviour
{
    public GameObject spotLight;
    public GameObject music;
    public AudioClip bangSound;
    private AudioSource _audioSource;

    private void Start()
    {
        spotLight.SetActive(false);
        _audioSource = GetComponent<AudioSource>();
    }
    public void LightFlicker()
    {
        spotLight.SetActive(true);
    }

    public void SlideSound()
    {
        _audioSource.Play();
    }

    public void BangSound()
    {
        _audioSource.PlayOneShot(bangSound);
    }

    public void PlayMusic()
    {
        music.SetActive(true);
    }
}

using UnityEngine;

public class FileAnimationEvents : MonoBehaviour
{
    public GameObject spotLight;
    public GameObject music;
    public AudioClip bangSound;
    private AudioSource _audioSource;
    private TextManager m_textManager;

    private void Start()
    {
        spotLight.SetActive(false);
        _audioSource = GetComponent<AudioSource>();
        m_textManager = FindAnyObjectByType<TextManager>();
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

    public void UpdateText() {
        m_textManager.InitText();
    }
}

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioLooper : MonoBehaviour
{
    [SerializeField] private double loopStartTime;
    [SerializeField] private double loopEndTime;

    private int _loopStartSamples;
    private int _loopEndSamples;
    private int loopLengthSamples;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.time = (float)loopStartTime;
    }

    void Update()
    {
        if (_audioSource.time >= (float)loopEndTime)
        {
            _audioSource.time = (float)loopStartTime;
        }
    }
}

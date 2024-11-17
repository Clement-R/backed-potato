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
        _loopStartSamples = (int)(loopStartTime * _audioSource.clip.frequency);
        _loopEndSamples = (int)(loopEndTime * _audioSource.clip.frequency);
        loopLengthSamples = _loopEndSamples - _loopStartSamples;
    }

    // Update is called once per frame
    void Update()
    {
        if(_audioSource.timeSamples >= _loopEndSamples)
        {
            _audioSource.timeSamples -= loopLengthSamples;
        }
    }
}

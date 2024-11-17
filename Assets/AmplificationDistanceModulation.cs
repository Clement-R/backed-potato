using UnityEngine;
using UnityEngine.InputSystem;

public class AmplificationDistanceModulation : MonoBehaviour
{
    private AudioSource _audioSource;
    private Camera _mainCamera;
    private Vector2 _audioSourceScreenSpacePosition;
    private float _distance;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _mainCamera = Camera.main;
        _audioSourceScreenSpacePosition = _mainCamera.WorldToScreenPoint(_audioSource.transform.position);
    }

    private void Update()
    {
        var mousePosition = Mouse.current.position.ReadValue();
        var diff = mousePosition - _audioSourceScreenSpacePosition;
        var diffNormalized = new Vector2(diff.x / Screen.currentResolution.width, diff.y / Screen.currentResolution.height);
        var distanceNormalized = diffNormalized.magnitude;
        _audioSource.volume = 1f - distanceNormalized;
    }
}

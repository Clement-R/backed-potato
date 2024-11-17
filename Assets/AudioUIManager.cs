using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class AudioUIManager : MonoBehaviour
{
    private AudioSource sceneAudioSource;
    public AudioClip hoverClip;
    public AudioClip clickClip;

    private Button[] buttons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneAudioSource = GetComponent<AudioSource>();
        buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach(var button in buttons)
        {
            var interactions = button.gameObject.AddComponent<ButtonInteractionEvents>();
            interactions.audioSource = sceneAudioSource;
            interactions.hoverClip = hoverClip;
            interactions.clickClip = clickClip;

        }
    }
}

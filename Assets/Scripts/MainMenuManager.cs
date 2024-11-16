using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField m_seedInputField;

    [SerializeField]
    private Button m_shooterStartButton;

    [SerializeField]
    private Button m_informerStartButton;

    [SerializeField]
    private CanvasGroup m_errorMessageCanvas;

    [SerializeField]
    private string m_shooterSceneName;

    [SerializeField]
    private string m_informerSceneName;

    void Start()
    {
        m_seedInputField.onValueChanged.AddListener(SeedValueChanged);
        m_shooterStartButton.onClick.AddListener(StartAsShooter);
        m_informerStartButton.onClick.AddListener(StartAsInformer);

        m_errorMessageCanvas.alpha = 0f;
        m_errorMessageCanvas.blocksRaycasts = false;
        m_errorMessageCanvas.interactable = false;
    }

    private void Update()
    {

    }

    private void SeedValueChanged(string p_value)
    {
        m_seedInputField.text = Regex.Replace(p_value, @"[^0-9]", "");
    }

    private void StartAsShooter()
    {
        if (!string.IsNullOrEmpty(m_seedInputField.text))
        {
            LaunchGame(m_shooterSceneName);
        }
        else
        {
            ShowErrorMessage();
        }
    }

    private void StartAsInformer()
    {
        if (!string.IsNullOrEmpty(m_seedInputField.text))
        {
            Debug.Log("Start as informer with seed : " + m_seedInputField.text);
            LaunchGame(m_informerSceneName);
        }
        else
        {
            ShowErrorMessage();
        }
    }

    private void LaunchGame(string p_sceneName)
    {
        // Set seed
        GameManager.Instance.SetSeed(Int32.Parse(m_seedInputField.text));

        // Load scene
        SceneManager.LoadScene(p_sceneName);
    }

    private void ShowErrorMessage()
    {
        m_errorMessageCanvas.blocksRaycasts = true;
        m_errorMessageCanvas.interactable = true;

        m_errorMessageCanvas
            .DOFade(1f, 1f)
            .OnComplete(() => { HideErrorMessage(); });
    }

    private void HideErrorMessage()
    {
        m_errorMessageCanvas
           .DOFade(0f, 0.5f)
           .OnComplete(() =>
           {
               m_errorMessageCanvas.blocksRaycasts = false;
               m_errorMessageCanvas.interactable = false;
           });

    }
}

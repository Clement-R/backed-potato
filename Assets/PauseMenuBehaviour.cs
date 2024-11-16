using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuBehaviour : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup m_group;
    [SerializeField]
    private Button m_exitButton;
    [SerializeField]
    private Button m_closeButton;

    [SerializeField]
    private float m_fadeDuration = 1f;

    private InputAction m_exitAction;

    private void Start()
    {
        m_exitAction = InputSystem.actions.FindAction("Cancel");
        m_exitAction.performed += ShowPauseMenu;

        m_exitButton.onClick.AddListener(GoToMainMenu);
        m_closeButton.onClick.AddListener(HideMenu);

        m_group.alpha = 0f;
        SetMenuUsable(false);
    }

    private void ShowPauseMenu(InputAction.CallbackContext context)
    {
        m_group
        .DOFade(1f, m_fadeDuration)
        .OnComplete(
            () => { SetMenuUsable(true); }
        );
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void HideMenu()
    {
        m_group
        .DOFade(0f, m_fadeDuration)
        .OnComplete(
            () => { SetMenuUsable(false); }
        );
    }

    private void SetMenuUsable(bool p_usable)
    {
        m_group.blocksRaycasts = p_usable;
        m_group.interactable = p_usable;
    }
}

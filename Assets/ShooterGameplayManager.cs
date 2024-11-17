using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using DG.Tweening;

using UnityEngine.InputSystem;

using Random = UnityEngine.Random;

public class ShooterGameplayManager : MonoBehaviour
{
    [Header("Game")]
    private float lel;

    [Header("UI")]
    [SerializeField]
    private float m_fadeDuration = 1f;

    [SerializeField]
    private TMP_InputField m_nameInput;
    [SerializeField]
    private Button m_startButton;
    [SerializeField]
    private CanvasGroup m_menuCanvasGroup;

    [SerializeField]
    private RectTransform m_crosshair;
    [SerializeField]
    private Canvas m_crosshairCanvas;

    private bool m_huntStarted = false;
    private float m_huntStartTime = float.NaN;

    void Awake()
    {
        m_startButton.onClick.AddListener(StartHunt);
    }

    void Start()
    {
        m_huntStarted = true;
        m_huntStartTime = Time.time;
        ShowMenu();
    }

    private void Update()
    {
        // Timer before start
        if (!m_huntStarted)
        {
            return;
        }

        if (m_huntStartTime + 3 > Time.time)
        {
            return;
        }

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            m_crosshairCanvas.transform as RectTransform,
            Mouse.current.position.ReadValue(),
            m_crosshairCanvas.worldCamera,
            out pos
        );

        m_crosshair.position = m_crosshairCanvas.transform.TransformPoint(pos);

        // Try to shoot
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            Debug.DrawLine(
                Camera.main.transform.position,
                Camera.main.transform.position + rayOrigin.direction * 15f,
                Color.red,
                10f
            );

            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                GameObject hitGo = hitInfo.collider.gameObject;
                if (hitGo.TryGetComponent<Potato>(out var potato))
                {
                    if (potato.isTarget)
                    {
                        //TODO: win condition
                        Debug.Log("Win, next file");
                    }
                    else
                    {
                        //TODO: lose condition*
                        Debug.Log("Lose, potato");
                    }

                    Debug.Log(hitGo.name);
                }
            }
        }
    }

    private void StartHunt()
    {
        if (string.IsNullOrEmpty(m_nameInput.text))
        {
            return;
        }

        var hash = m_nameInput.text.GetPotatoHash();
        Random.InitState(hash);

        HideMenu();

        m_huntStarted = true;
        m_huntStartTime = Time.time;
    }

    private void ShowMenu()
    {
        SetMenuUsable(false);

        m_menuCanvasGroup
        .DOFade(1f, m_fadeDuration)
        .OnComplete(
            () => { SetMenuUsable(true); }
        );
    }

    private void HideMenu()
    {
        m_menuCanvasGroup
        .DOFade(0f, m_fadeDuration)
        .OnComplete(
            () => { SetMenuUsable(false); }
        );
    }

    private void SetMenuUsable(bool p_usable)
    {
        m_menuCanvasGroup.blocksRaycasts = p_usable;
        m_menuCanvasGroup.interactable = p_usable;
    }
}

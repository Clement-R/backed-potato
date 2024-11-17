using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using DG.Tweening;

using UnityEngine.InputSystem;

using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class ShooterGameplayManager : MonoBehaviour
{
    [SerializeField]
    private PotatoGenerator m_potatoGenerator;

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

    [Header("Result UI")]
    [SerializeField]
    private float m_resultFadeDuration = 1f;
    [SerializeField]
    private CanvasGroup m_resultCanvasGroup;
    [SerializeField]
    private CanvasGroup m_textCanvasGroup;
    [SerializeField]
    private TMP_Text m_resultText;
    [SerializeField]
    private string m_winText = "Target smashed";
    [SerializeField]
    private Color m_winColor;
    [SerializeField]
    private string m_loseText = "Wrong potato";
    [SerializeField]
    private Color m_loseColor;
    [SerializeField]
    private string m_nextTargetText = "Next target";
    [SerializeField]
    private string m_restartText = "Restart mission";
    [SerializeField]
    private Button m_resultButton;
    [SerializeField]
    private TMP_Text m_resultButtonText;
    [SerializeField]
    private AudioSource m_audioSource;
    [SerializeField]
    private AudioClip m_shootSound;
    [SerializeField]
    private GameObject m_shootParticles;

    private bool m_huntStarted = false;
    private float m_huntStartTime = float.NaN;
    private int m_currentHash = 0;
    [SerializeField]
    private float m_shootCooldown = 0.5f;
    private float m_lastShoot = 0f;

    void Awake()
    {
        m_startButton.onClick.AddListener(ValidateTargetName);
    }

    void Start()
    {
        ShowMenu();

        m_resultCanvasGroup.alpha = 0f;
        m_resultCanvasGroup.blocksRaycasts = false;
        m_resultCanvasGroup.interactable = false;
    }

    private void Update()
    {
        // Timer before start
        if (!m_huntStarted)
        {
            return;
        }

        if (m_huntStartTime + .25 > Time.time)
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
        if (Time.time <= m_lastShoot + m_shootCooldown)
        {
            return;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            m_lastShoot = Time.time;
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
                // Play sound
                m_audioSource.PlayOneShot(m_shootSound);
                // Play vfx
                Instantiate(m_shootParticles, hitInfo.point, Quaternion.identity);

                GameObject hitGo = hitInfo.collider.gameObject;
                if (hitGo.TryGetComponent<Potato>(out var potato))
                {
                    EndHunt(potato.isTarget);
                }
            }
        }
    }

    private void EndHunt(bool p_missionSuccess)
    {
        m_resultCanvasGroup.blocksRaycasts = true;
        m_resultCanvasGroup.interactable = true;

        m_resultButton.gameObject.SetActive(false);

        if (p_missionSuccess)
        {
            m_resultButton.onClick.RemoveAllListeners();
            m_resultButton.onClick.AddListener(NextLevel);

            m_resultText.text = m_winText;
            m_resultText.color = m_winColor;
            m_resultButtonText.text = m_nextTargetText;
        }
        else
        {
            m_resultButton.onClick.RemoveAllListeners();
            m_resultButton.onClick.AddListener(RestartLevel);

            m_resultText.text = m_loseText;
            m_resultText.color = m_loseColor;
            m_resultButtonText.text = m_restartText;
        }

        var seq = DOTween.Sequence();
        seq.Append(
            m_resultCanvasGroup.DOFade(1f, m_resultFadeDuration)
        );
        seq.Append(
            m_textCanvasGroup.DOFade(1f, m_resultFadeDuration)
        );
        seq.AppendInterval(2f);
        seq.OnComplete(
            () => { m_resultButton.gameObject.SetActive(true); }
        );

        seq.Play();
    }

    private void RestartLevel()
    {
        Random.InitState(m_currentHash);
        InitLevel();
        m_potatoGenerator.RestartLevel();
        Debug.Log(m_currentHash);
        StartHunt();
    }

    private void NextLevel()
    {
        ShowMenu();
        GameManager.Instance.level += 1;

        m_resultCanvasGroup.alpha = 0f;
        m_resultCanvasGroup.blocksRaycasts = false;
        m_resultCanvasGroup.interactable = false;
    }

    private void ValidateTargetName()
    {
        if (string.IsNullOrEmpty(m_nameInput.text))
        {
            return;
        }

        var hash = m_nameInput.text.GetPotatoHash();
        m_currentHash = hash;
        StartHunt();
    }

    private void StartHunt()
    {
        Random.InitState(m_currentHash);

        InitLevel();

        m_potatoGenerator.RestartLevel();

        HideMenu();

        m_huntStarted = true;
        m_huntStartTime = Time.time;

        m_resultCanvasGroup.alpha = 0f;
        m_resultCanvasGroup.blocksRaycasts = false;
        m_resultCanvasGroup.interactable = false;
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

    private void InitLevel()
    {
        var level = GameManager.Instance.level;

        float nbPotatoes = (float)(1.089 * Math.Pow(level, 2) - 1.117 * level + 2.592);
        m_potatoGenerator.nbPotatoes = (int)Math.Min(nbPotatoes, 100);
    }
}

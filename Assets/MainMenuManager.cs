using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        m_shooterStartButton.onClick.AddListener(StartAsShooter);
        m_informerStartButton.onClick.AddListener(StartAsInformer);
    }

    private void Update()
    {

    }

    private void StartAsShooter()
    {
        if (!string.IsNullOrEmpty(m_seedInputField.text))
        {
            Debug.Log("Start as shooter with seed : " + m_seedInputField.text);
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
        }
        else
        {
            m_errorMessageCanvas.alpha = 1f;
            m_errorMessageCanvas.blocksRaycasts = true;
        }
    }

    private void ShowErrorMessage()
    {
        m_errorMessageCanvas.alpha = 1f;
        m_errorMessageCanvas.blocksRaycasts = true;
        m_errorMessageCanvas.interactable = true;
    }

    private void HideErrorMessage()
    {
        m_errorMessageCanvas.alpha = 0f;
        m_errorMessageCanvas.blocksRaycasts = false;
        m_errorMessageCanvas.interactable = false;
    }
}

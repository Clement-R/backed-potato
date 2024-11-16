using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private Button m_shooterStartButton;

    [SerializeField]
    private Button m_informerStartButton;

    [SerializeField]
    private string m_shooterSceneName;

    [SerializeField]
    private string m_informerSceneName;

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
        SceneManager.LoadScene(m_shooterSceneName);
    }

    private void StartAsInformer()
    {
        SceneManager.LoadScene(m_informerSceneName);
    }
}

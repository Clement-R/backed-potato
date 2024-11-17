using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using DG.Tweening;

public class InformerScreenBehaviour : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_targetNameText;

    [SerializeField]
    private Potato m_targetPotato;

    private string m_targetName;

    [SerializeField]
    private Button m_nextTargetButton;

    [Header("Target Confirm Popup")]
    [SerializeField]
    private CanvasGroup m_popupCanvasGroup;
    [SerializeField]
    private Button m_returnButton;
    [SerializeField]
    private Button m_confirmButton;
    [SerializeField]
    private Animator m_fileAnimator;
    void Start()
    {
        m_targetPotato.enabled = false;
        NextTarget();

        SetPopupVisible(false);

        m_returnButton.onClick.AddListener(() => { SetPopupVisible(false); });

        m_nextTargetButton.onClick.AddListener(() => { SetPopupVisible(true); });

        m_confirmButton.onClick.AddListener(NextTarget);
    }

    private void SetPopupVisible(bool p_visible)
    {
        m_popupCanvasGroup.alpha = p_visible ? 1f : 0f;
        m_popupCanvasGroup.blocksRaycasts = p_visible;
        m_popupCanvasGroup.interactable = p_visible;
    }

    private void NextTarget()
    {
        GameManager.Instance.level += 1;
        StartCoroutine(WaitForNextTarget());
    }

    private IEnumerator WaitForNextTarget()
    {
        m_nextTargetButton.gameObject.SetActive(false);
        SetPopupVisible(false);
        m_fileAnimator.SetBool("Visible", false);
        yield return new WaitForSeconds(2.5f);
        yield return GenerateTargetName();
        m_fileAnimator.SetBool("Visible", true);
        yield return new WaitForSeconds(2.5f);
        m_nextTargetButton.gameObject.SetActive(true);
    }

    private IEnumerator GenerateTargetName()
    {
        var url = "https://api.phage.directory/utility/api/animals?num=1";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    var randomAnimal = webRequest.downloadHandler.text;
                    randomAnimal = Regex.Replace(randomAnimal, @"[^a-zA-Z-]", "");
                    m_targetNameText.text = randomAnimal;
                    m_targetName = randomAnimal;

                    var hash = m_targetName.GetPotatoHash();
                    m_targetPotato.symetric = GameManager.Instance.IsPotatoesSymetric;
                    m_targetPotato.SetAsInformerProfile(hash);
                    m_targetPotato.enabled = true;

                    break;
            }
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Target
{
    public string name;
}

[System.Serializable]
public class Targets
{
    public Target[] targets;
}

public class InformerScreenBehaviour : MonoBehaviour
{
    [SerializeField]
    public TextAsset namesJson;

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

    private List<string> m_targetNames = new List<string>();

    void Start()
    {
        m_targetPotato.enabled = false;
        NextTarget();

        SetPopupVisible(false);

        m_returnButton.onClick.AddListener(() => { SetPopupVisible(false); });

        m_nextTargetButton.onClick.AddListener(() => { SetPopupVisible(true); });

        m_confirmButton.onClick.AddListener(NextTarget);

        Targets targetsInJson = JsonUtility.FromJson<Targets>(namesJson.text);
        m_targetNames = targetsInJson.targets.Select(e => e.name).ToList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateTargetName();
        }
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
        GenerateTargetName();
        m_fileAnimator.SetBool("Visible", true);
        yield return new WaitForSeconds(2.5f);
        m_nextTargetButton.gameObject.SetActive(true);
    }

    private void GenerateTargetName()
    {
        var randomAnimal = m_targetNames[Random.Range(0, m_targetNames.Count)];
        randomAnimal = Regex.Replace(randomAnimal, @"[^a-zA-Z-]", "");
        m_targetNameText.text = randomAnimal;
        m_targetName = randomAnimal;

        var hash = m_targetName.GetPotatoHash();
        m_targetPotato.symetric = GameManager.Instance.IsPotatoesSymetric;
        m_targetPotato.SetAsInformerProfile(hash);
        m_targetPotato.enabled = true;
    }
}

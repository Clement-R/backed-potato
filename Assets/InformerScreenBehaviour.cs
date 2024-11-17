using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Text.RegularExpressions;

public class InformerScreenBehaviour : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_targetNameText;

    [SerializeField]
    private Potato m_targetPotato;

    private string m_targetName;

    void Start()
    {
        m_targetPotato.enabled = false;
        StartCoroutine(GenerateTargetName());
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
                    m_targetPotato.SetAsInformerProfile(hash);
                    m_targetPotato.enabled = true;

                    break;
            }
        }
    }
}

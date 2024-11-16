using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;

    public static GameManager Instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = (GameManager)FindFirstObjectByType(typeof(GameManager));

                if (!m_instance)
                {
                    Debug.LogError("There needs to be one active OptionsManager script on a GameObject in your scene.");
                }
            }

            return m_instance;
        }
    }

    private int m_seed;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetSeed(int p_seed)
    {
        m_seed = p_seed;
        Random.InitState(m_seed);
    }
}

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;


public class PotatoGenerator : MonoBehaviour
{
    public GameObject potatoPrefab;

    [HideInInspector]
    public int nbPotatoes;

    [SerializeField]
    private Transform m_navmeshCenter;

    [SerializeField]
    private float m_navmeshRange;

    private List<GameObject> m_potatoes = new List<GameObject>();

    private void OnDrawGizmos()
    {
        if (m_navmeshCenter == null)
        {
            return;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(m_navmeshCenter.transform.position, m_navmeshRange);
    }

    public void SpawnPotatoes()
    {
        Debug.Log("PotatoGenerator Start, nbPotatoes: " + nbPotatoes + "Level: " + GameManager.Instance.level);
        
        bool symetry = GameManager.Instance.IsPotatoesSymetric;

        for (int i = 0; i < nbPotatoes; i++)
        {
            var position = new Vector3();
            var potato = Instantiate(potatoPrefab, position, Quaternion.identity);
            var potatoScript = potato.GetComponent<Potato>();
            potatoScript.symetric = symetry;
            potatoScript.AddAccessories();
            RandomPointOnNavMesh(m_navmeshCenter.position, m_navmeshRange, out var out_position);
            potato.transform.position = out_position;

            m_potatoes.Add(potato);
            

            potato.GetComponent<Timer>().timeRemaining = Random.Range(10, 15);

            if (i == 0)
            {
                potatoScript.isTarget = true;
            }

            potato.name = $"Potato : {Random.Range(0, 100)}";
        }
    }

    public void RestartLevel()
    {
        m_potatoes.ForEach((potato) => { Destroy(potato.gameObject); });
        m_potatoes.Clear();

        SpawnPotatoes();
    }

    bool RandomPointOnNavMesh(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
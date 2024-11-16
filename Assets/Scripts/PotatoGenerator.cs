using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class PotatoGenerator : MonoBehaviour
{
    public GameObject potatoPrefab;
    public int nbPotatoes = 10;

    [SerializeField]
    private Transform m_navmeshCenter;

    [SerializeField]
    private float m_navmeshRange;

    private void OnDrawGizmos()
    {
        if (m_navmeshCenter == null)
        {
            return;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(m_navmeshCenter.transform.position, m_navmeshRange);
    }

    void Start()
    {
        bool symetry = GameManager.Instance.IsPotatoesSymetric;
        for (int i = 0; i < nbPotatoes; i++)
        {
            RandomPoint(m_navmeshCenter.position, m_navmeshRange, out var position);

            var potato = Instantiate(potatoPrefab, position, Quaternion.identity);
            potato.name = $"Potato : {Random.Range(0, 100)}";

            var potatoScript = potato.GetComponent<Potato>();
            potatoScript.symetric = symetry;
            potato.GetComponent<Timer>().timeRemaining = Random.Range(10, 15);

            if (i == 0)
            {
                potatoScript.isTarget = true;
            }
        }
    }

<<<<<<< HEAD
    void Update()
    {
        
=======
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
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
>>>>>>> 335b7aad17d8907ae7f5c79d72ad74afdccc4ce4
    }
}
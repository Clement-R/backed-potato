using System.Runtime.CompilerServices;
using UnityEngine;

public class PotatoGenerator : MonoBehaviour
{
    public GameObject potatoPrefab;
    public int nbPotatoes = 10;
    public float minX = -5;
    public float maxX = 5;
    public float minY = -5;
    public float maxY = 5;

    void Start()
    {
        bool symetry = GameManager.Instance.IsPotatoesSymetric;
        for (int i = 0; i < nbPotatoes; i++)
        {
            Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);

            var potato = Instantiate(potatoPrefab, position, Quaternion.identity);
            var potatoScript = potato.GetComponent<Potato>();
            potatoScript.symetric = symetry;
        }
    }
}
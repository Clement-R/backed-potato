using UnityEngine;

public class PotatoGenerator : MonoBehaviour
{
    public GameObject potatoPrefab;
    public int nbPotatoes = 10;
    public float minX = -3;
    public float maxX = 3;
    public float minY = -3;
    public float maxY = 3;

    void Start()
    {
        for (int i = 0; i < nbPotatoes; i++)
        {
            Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);

            var potato = Instantiate(potatoPrefab, position, Quaternion.identity);
            var potatoScript = potato.GetComponent<Potato>();
            potatoScript.symetric = false;
        }
    }
}
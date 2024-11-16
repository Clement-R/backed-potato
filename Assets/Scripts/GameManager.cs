using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int seed = 0;
        Random.InitState(seed);

        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.linearVelocity = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

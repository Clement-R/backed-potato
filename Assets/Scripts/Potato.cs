using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Potato : MonoBehaviour
{
    // Liste publique visible dans l'inspecteur
    [SerializeField]
    private List<Sprite> body = new List<Sprite>();
    [SerializeField]
    private List<Sprite> hats = new List<Sprite>();
    [SerializeField]
    private List<Sprite> eyes = new List<Sprite>();
    [SerializeField]
    private List<Sprite> hands = new List<Sprite>();

    [SerializeField]
    private List<Sprite> accessories = new List<Sprite>();
    [SerializeField]
    private List<Sprite> feet = new List<Sprite>();


    // Références vers les emplacements des accessoires
    public SpriteRenderer bodySlot;
    public SpriteRenderer hatSlot;
    public SpriteRenderer eyesSlot;
    public SpriteRenderer bodyAccessorySlot;
    public SpriteRenderer leftHandSlot;
    public SpriteRenderer rightHandSlot;
    public SpriteRenderer leftFootSlot;
    public SpriteRenderer rightFootSlot;

    [HideInInspector]
    public bool symetric = true;

    // [HideInInspector]
    public bool isTarget = false;

    private NavMeshAgent agent;
    
    private Animator animator;

    private void Awake()
    {
        // Récupère le composant NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        animator = GetComponent<Animator>();
    }

    // Déplace l'objet vers une destination donnée
    public void MoveTo(Vector3 destination)
    {
        if (agent != null)
        {
            agent.SetDestination(destination);
        }
    }
    public void AddAccessories()
    {

        // Ajoute un corps
        bodySlot.sprite = body[Random.Range(0, body.Count)];

        // Ajoute des yeux
        eyesSlot.sprite = eyes[Random.Range(0, eyes.Count)];

        // Ajoute un chapeau
        hatSlot.sprite = hats[Random.Range(0, hats.Count)];


        // Ajoute des mains
        int nb = Random.Range(0, hands.Count);
        leftHandSlot.sprite = hands[nb];
        if (!symetric)
        {
            nb = Random.Range(0, hands.Count);
        }
        rightHandSlot.sprite = hands[nb];

        // Ajoute un accessoire
        bodyAccessorySlot.sprite = accessories[Random.Range(0, accessories.Count)];

        // Ajoute des pieds
        nb = Random.Range(0, feet.Count);
        leftFootSlot.sprite = feet[nb];
        if (!symetric)
        {
            nb = Random.Range(0, hands.Count);
        }
        rightFootSlot.sprite = feet[nb];
    }

    private void Start()
    {
        AddAccessories();
        
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        // Déplace la patate à une position aléatoire toutes les X secondes
        if (Input.GetKeyDown(KeyCode.Space)) // Appuie sur la barre espace pour tester
        {
            MoveToRandomPosition();
        }
    }
    
    // Déplace la patate vers une position aléatoire
    private void MoveToRandomPosition()
    {
        // Rayon de recherche autour de la position actuelle
        float searchRadius = 10f;

        // Génère une direction aléatoire dans un rayon donné
        Vector3 randomDirection = Random.insideUnitSphere * searchRadius;
        randomDirection += transform.position;

        // Trouve un point navigable proche de la direction générée
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, searchRadius, NavMesh.AllAreas))
        {
            // Déplace la patate vers le point trouvé
            MoveTo(hit.position);
        }
    }
}
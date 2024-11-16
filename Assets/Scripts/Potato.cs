using UnityEngine;
using System.Collections.Generic;

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

    [HideInInspector]
    public bool isTarget = false;

    public void AddAccessories() {
      
        // Ajoute un corps
        bodySlot.sprite = body[Random.Range(0, body.Count)];

        // Ajoute des yeux
        eyesSlot.sprite = eyes[Random.Range(0, eyes.Count)];

        // Ajoute un chapeau
        hatSlot.sprite = hats[Random.Range(0, hats.Count)];
  

        // Ajoute des mains
        int nb = Random.Range(0, hands.Count);
        leftHandSlot.sprite = hands[nb];
        if (!symetric) {
            nb = Random.Range(0, hands.Count);
        }
        rightHandSlot.sprite = hands[nb];
        
        // Ajoute un accessoire
        bodyAccessorySlot.sprite = accessories[Random.Range(0, accessories.Count)];
        
        // Ajoute des pieds
        nb = Random.Range(0, feet.Count);
        leftFootSlot.sprite = feet[nb];
        if (!symetric) {
            nb = Random.Range(0, hands.Count);
        }
        rightFootSlot.sprite = feet[nb];
    }

    private void Start() {
        AddAccessories();
    }
}
using UnityEngine;
using System.Collections.Generic;

public class Potato : MonoBehaviour
{
    // Liste publique visible dans l'inspecteur
    [SerializeField]
    private List<Sprite> hats = new List<Sprite>();
    [SerializeField]
    private List<Sprite> hands = new List<Sprite>();
    [SerializeField]
    private List<Sprite> accessories = new List<Sprite>();
    [SerializeField]
    private List<Sprite> feet = new List<Sprite>();

    // Références vers les emplacements des accessoires
    public SpriteRenderer hatSlot;
    public SpriteRenderer handsSlot;
    public SpriteRenderer accessorySlot;
    public SpriteRenderer feetSlot;


    public void AddAccessories() {
        // Ajoute un chapeau
        hatSlot.sprite = hats[Random.Range(0, hats.Count)];
        
        // Ajoute des mains
        handsSlot.sprite = hands[Random.Range(0, hands.Count)];
        
        // Ajoute un accessoire
        accessorySlot.sprite = accessories[Random.Range(0, accessories.Count)];
        
        // Ajoute des pieds
        feetSlot.sprite = feet[Random.Range(0, feet.Count)];
    }
}

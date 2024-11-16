using UnityEngine;

public class Potato : MonoBehaviour
{
    // Références vers les emplacements des accessoires
    public Transform hatSlot;
    public Transform handSlot;
    public Transform accessorySlot;
    public Transform footSlot;

    public void AddAccessory(string type, Sprite accessorySprite)
    {
        Transform slot = null;

        // Détermine le slot correspondant au type d'accessoire
        switch (type.ToLower())
        {
            case "hat":
                slot = hatSlot;
                break;
            case "hand":
                slot = handSlot;
                break;
            case "accessory":
                slot = accessorySlot;
                break;
            case "foot":
                slot = footSlot;
                break;
            default:
                Debug.LogWarning("Type d'accessoire inconnu : " + type);
                return;
        }

        // Ajoute ou remplace le Sprite dans le slot correspondant
        if (slot != null)
        {
            SpriteRenderer sr = slot.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = accessorySprite; // Assigne l'image
            }
            else
            {
                Debug.LogError("Le slot " + slot.name + " n'a pas de SpriteRenderer !");
            }
        }
    }

    public void RemoveAccessory(string type)
    {
        Transform slot = null;

        // Détermine le slot correspondant au type d'accessoire
        switch (type.ToLower())
        {
            case "hat":
                slot = hatSlot;
                break;
            case "hand":
                slot = handSlot;
                break;
            case "accessory":
                slot = accessorySlot;
                break;
            case "foot":
                slot = footSlot;
                break;
            default:
                Debug.LogWarning("Type d'accessoire inconnu : " + type);
                return;
        }

        // Supprime l'accessoire du SpriteRenderer
        if (slot != null)
        {
            SpriteRenderer sr = slot.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = null; // Enlève le sprite
            }
            else
            {
                Debug.LogError("Le slot " + slot.name + " n'a pas de SpriteRenderer !");
            }
        }
    }
}

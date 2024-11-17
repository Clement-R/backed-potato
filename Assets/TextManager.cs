using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using System;

public class TextManager : MonoBehaviour
{
    public TMPro.TMP_Text note1;
    public TMPro.TMP_Text note2;
    public TMPro.TMP_Text note3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private string[] notes =
    {
        "\n\nAgent.\nVotre mission est delicate.\n" +
        "Vous devez communiquer efficacement avec l'operateur du drone afin d'eliminer la cible.\n" +
        "Le temps vous est compte.\nLa securite de votre nation en depend.",

        "\n\n\nAgent, ce nouveau suspect est incontrolable.\n" +
        "Il est responsable de plusieurs tueries de masse,\n" +
        "et veut quitter le pays. Nous devons l'arreter.\n" +
        "La nation compte sur vous.",

        "\n\n\nAgent, cet homme est un agent double.\n" +
        "Il a vendu des informations sensibles a des pays ennemis.\n" +
        "Nous devons le tuer avant qu'il ne s'extrade a l'etranger.\n" +
        "L'agence compte sur vous.",

        "\n\nTuez le suspect.\n" +
        "Nous comptons sur vous.",

        "\n\nTuez la cible.\n",

        "\n\nTuez le !\n",

        "\n\nTUER !\n",

        "\n\nT.U.E.R !!!!!!!!!\n",

        "\n\n\nTUER !! TUER !! TUER !!\n" +
        "TUER !! TUER !! TUER !!\n" +
        "TUER !! TUER !! TUER !!\n" +
        "TUER !! TUER !! TUER !!\n" +
        "TUER !! TUER !! TUER !!\n" +
        "TUER !! TUER !! TUER !!\n",
    };


    public void InitText()
    {
        int note_number = Math.Min(GameManager.Instance.level - 1, notes.Length - 1);
        note1.text = notes[note_number];
    }


}

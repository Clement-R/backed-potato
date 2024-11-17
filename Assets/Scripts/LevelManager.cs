using Unity.AI.Navigation;
using UnityEditor.SceneManagement;
using UnityEngine;
using System;
using UnityEngine.AI;


public class LevelManager : MonoBehaviour {
    
    PotatoGenerator potatoGenerator;
    

    void Start() {
        int lvl = GameManager.Instance.level;
        Debug.Log("Level in LevelManager : " + lvl);
        if (lvl <= 3) {
            GameManager.Instance.IsPotatoesSymetric = true;
        }
        else {
            GameManager.Instance.IsPotatoesSymetric = false;
        }
        potatoGenerator = FindAnyObjectByType<PotatoGenerator>();
        float nbPotatoes = (float)(1.089 * Math.Pow(lvl, 2) - 1.117 * lvl + 2.592);
        potatoGenerator.nbPotatoes = (int) Math.Min(nbPotatoes, 100);
        potatoGenerator.InitPotatoGenerator();
    }
}
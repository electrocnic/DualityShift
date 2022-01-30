using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
    [SerializeField] private string nextLevelName;

    public static int currentLevel = 0;
    // [SerializeField] private Transform levelExitTarget;
    private GameObject player;
    
    private void OnEnable() {
        //m_Monsters = FindObjectsOfType<>()
    }

    void Start() {
        player = Resources.FindObjectsOfTypeAll<CharacterController2d>()[0].gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: idea: if all monsters dead: win
        //if (reachedLevelExit()) {
        //    goToNextLevel();
        //}
    }

    private void goToNextLevel() {
        currentLevel++;
        SceneManager.LoadScene(nextLevelName);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (!player) {
            return;
        }

        if (player == col.gameObject) {
            goToNextLevel();
        }
    }
}

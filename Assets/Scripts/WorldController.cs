using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

/**
 * Keymap:
 *
 * R: Restart (after death)
 * WASD: move (W = Jump)
 * W, Space: Jump
 * Left Shift: Consume Potion if available
 */
public class WorldController : MonoBehaviour {
    [SerializeField] CharacterController2d controller;

    [SerializeField] private Transform pfBirb;
    [SerializeField] private Transform pfShroom;
    [SerializeField] private int maxEnemyCount = 5;
    [SerializeField] private float enemySpawnRateInSeconds = 10f;
    [SerializeField] private float enemyMinSpawnDistanceToPlayer = 4f;
    [SerializeField] private float enemyMaxSpawnDistanceToPlayer = 10f;
    [SerializeField] GameObject world1;
    [SerializeField] GameObject world2;
    [SerializeField] TextMeshProUGUI levelStatsTextField;
    public static bool isDead = false;

    private GameObject player;
    private float lastShot = 0f;

    // Start is called before the first frame update
    void Start() {
        player = Resources.FindObjectsOfTypeAll<CharacterController2d>()[0].gameObject;
        pfBirb.GetComponent<BirbAI>().target = controller.transform;
        pfShroom.GetComponent<MushroomAI>().target = controller.transform;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            isDead = false;
            levelStatsTextField.SetText("");
            Application.LoadLevel(Application.loadedLevel);
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Equals)) {
            controller.setPotionFillStatus(MathF.Min(1.0f, controller.getPotionFillStatus() + 0.3f));
        }

        if (Input.GetKeyDown(KeyCode.Alpha7)) {
            Invincible = !Invincible;
        }
    }

    public bool Invincible { get; set; } = false;

    // Update is called once per frame
    void FixedUpdate() {
        if (isDead) {
            displayDeathMessage();
        }
        
        if (!controller) {
            return;
        }

        float t = Time.time;
        if (t - lastShot > enemySpawnRateInSeconds) {
            float randX = Random.Range(0f, 1f) - 0.5f;
            float randY = Random.Range(0f, 1f);
            float minDist = (randX > 0) ? enemyMinSpawnDistanceToPlayer : -enemyMinSpawnDistanceToPlayer;
            float maxDist = (randX > 0) ? enemyMaxSpawnDistanceToPlayer : -enemyMaxSpawnDistanceToPlayer;
            var playerPosition = (Vector2) controller.transform.position;
            float spawnAtX = MathF.Min(minDist,
                MathF.Max(maxDist, (randX + (maxDist - minDist) * 0.01f) * 100f + minDist));
            var randEnemyType = Random.Range(0f, 1f);

            if (!(world1.activeInHierarchy ^ world2.activeInHierarchy)) {
                return;
            }
            if ((randEnemyType < 0.5 && world1.activeInHierarchy || randEnemyType < 0.3) &&
                Resources.FindObjectsOfTypeAll<BirbAI>().Length < maxEnemyCount) {
                // spawn flyings more often in world1 and less often in world 2
                // newPos - playerPos > minSpawnDistance  ==  randomVec > minSpawnDistance, because randomVec+playerPos = newPos
                // newPos - enemyMaxSpawnDistanceToPlayer < playerPos, randomVec < maxSpawnDistance
                Vector2 spwanPostion = playerPosition + new Vector2(
                    spawnAtX,
                    MathF.Min(enemyMinSpawnDistanceToPlayer,
                        MathF.Max(enemyMaxSpawnDistanceToPlayer,
                            (randY + (enemyMaxSpawnDistanceToPlayer - enemyMinSpawnDistanceToPlayer) * 0.01f) * 100f +
                            enemyMinSpawnDistanceToPlayer))
                );
                var birb = Instantiate(pfBirb, spwanPostion, Quaternion.identity);
                birb.GetComponent<Damageable>().objectId = "birb";
                lastShot = t;
            }
            else if ((randEnemyType >= 0.3 && world2.activeInHierarchy) &&
                     Resources.FindObjectsOfTypeAll<MushroomAI>().Length < maxEnemyCount) {
                // spawn ground enemies only in world 2 (and only if not flying was spawned).
                Vector2 spwanPostion = new Vector2(playerPosition.x + spawnAtX, 0);
                var shroom = Instantiate(pfShroom, spwanPostion, Quaternion.identity);
                shroom.GetComponent<Damageable>().objectId = "shroom";
                lastShot = t;
            }
        }
    }

    public void displayDeathMessage() {
        levelStatsTextField.SetText("You died!\nKilled Birbs: "
                                    + Scoring.getKilledBirbs()
                                    + "\nKilled shrooms: " + Scoring.getKilledShrooms()
                                    + "\nDeaths in this level: " + Scoring.getDeaths());
    }
}
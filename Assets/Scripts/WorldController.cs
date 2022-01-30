using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
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

    [SerializeField] public Transform pfBullet;
    [SerializeField] private Transform pfBirb;
    [SerializeField] private Transform pfShroom;
    [SerializeField] private int maxEnemyCount = 5;
    [SerializeField] private float enemySpawnRateInSeconds = 4.4f;
    [SerializeField] private float enemyMinSpawnDistanceToPlayer = 4f;
    [SerializeField] private float enemyMaxSpawnDistanceToPlayer = 10f;
    [SerializeField] GameObject world1;
    [SerializeField] GameObject world2;
    private float lastShot = 0f;
    // Start is called before the first frame update
    void Start() {
        pfBirb.GetComponent<BirbAI>().target = controller.transform;
        pfBirb.GetComponent<BirbAI>().pfBullet = pfBullet;
        pfShroom.GetComponent<MushroomAI>().target = controller.transform;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
            controller.setPotionFillStatus(MathF.Min(1.0f,controller.getPotionFillStatus() + 0.3f));
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!controller) {
            return;
        }
        float t = Time.time;
        if (t - lastShot > enemySpawnRateInSeconds) {
            float randX = Random.Range(0f, 1f) - 0.5f;
            float randY = Random.Range(0f, 1f);
            float minDist = (randX > 0) ? enemyMinSpawnDistanceToPlayer : -enemyMinSpawnDistanceToPlayer;
            float maxDist = (randX > 0) ? enemyMaxSpawnDistanceToPlayer : -enemyMaxSpawnDistanceToPlayer;
            var playerPosition = (Vector2)controller.transform.position;
            float spawnAtX = MathF.Min(minDist, MathF.Max(maxDist, (randX + (maxDist - minDist) * 0.01f) * 100f + minDist));
            var randEnemyType = Random.Range(0f, 1f);

            if ((randEnemyType < 0.5 && world1.activeInHierarchy || randEnemyType < 0.3) && Resources.FindObjectsOfTypeAll<BirbAI>().Length < maxEnemyCount) {
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

                Instantiate(pfBirb, spwanPostion, Quaternion.identity);
            } else if ((randEnemyType >= 0.3 && world2.activeInHierarchy) && Resources.FindObjectsOfTypeAll<MushroomAI>().Length < maxEnemyCount) {
                // spawn ground enemies only in world 2 (and only if not flying was spawned).
                Vector2 spwanPostion = playerPosition + new Vector2(spawnAtX, 0);

                Instantiate(pfShroom, spwanPostion, Quaternion.identity);
            }

            lastShot = Time.time;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

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
    [SerializeField] private int maxEnemyCount = 5;
    [SerializeField] private float enemySpawnRateInSeconds = 4.4f;
    [SerializeField] private float enemyMinSpawnDistanceToPlayer = 4f;
    [SerializeField] private float enemyMaxSpawnDistanceToPlayer = 10f;
    private Random _random;
    private float lastShot = 0f;
    // Start is called before the first frame update
    void Start() {
        _random = new Random();
        pfBirb.GetComponent<BirbAI>().target = controller.transform;
        pfBirb.GetComponent<BirbAI>().pfBullet = pfBullet;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!controller) {
            return;
        }
        float t = Time.time;
        if (t - lastShot > enemySpawnRateInSeconds && Resources.FindObjectsOfTypeAll<BirbAI>().Length < maxEnemyCount) {
            var playerPosition = (Vector2)controller.transform.position;
            float randX = (float)_random.NextDouble()-0.5f;
            float randY = (float)_random.NextDouble();
            float minDist = (randX > 0) ? enemyMinSpawnDistanceToPlayer : -enemyMinSpawnDistanceToPlayer;
            float maxDist = (randX > 0) ? enemyMaxSpawnDistanceToPlayer : -enemyMaxSpawnDistanceToPlayer;
            // newPos - playerPos > minSpawnDistance  ==  randomVec > minSpawnDistance, because randomVec+playerPos = newPos
            // newPos - enemyMaxSpawnDistanceToPlayer < playerPos, randomVec < maxSpawnDistance
            Vector2 spwanPostion = new Vector2(
                MathF.Min(minDist, MathF.Max(maxDist, (randX + (maxDist-minDist)*0.01f)*100f + minDist)),
                MathF.Min(enemyMinSpawnDistanceToPlayer, MathF.Max(enemyMaxSpawnDistanceToPlayer, (randY + (enemyMaxSpawnDistanceToPlayer-enemyMinSpawnDistanceToPlayer)*0.01f) * 100f + enemyMinSpawnDistanceToPlayer))
            );

            Instantiate(pfBirb, spwanPostion, Quaternion.identity);
            lastShot = Time.time;
        }
    }
}
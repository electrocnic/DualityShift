using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour {
    [SerializeField] private float fallDelay = 0.5f;
    [SerializeField] private float disappearDelay = 2f;
    [SerializeField] private float risingSpeed = 2f;
    [SerializeField] private float riseDelay = 2f;
    [SerializeField] private float gravityScale = 2f;
    [SerializeField] private Transform player;
    private DualityModeController dualityModeController;

    private bool movingUp = false;

    private Rigidbody2D rbRigidbody2D;
    private void Start() {
        dualityModeController = Resources.FindObjectsOfTypeAll<DualityModeController>()[0];
        rbRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (!player) {
            return;
        }
        if (col.gameObject == player.gameObject && dualityModeController.WorldState == WorldSwitched.World.Light) {
            StartCoroutine(FallDown());
        }
        if (col.gameObject == player.gameObject && dualityModeController.WorldState == WorldSwitched.World.Dark) {
            StartCoroutine(MoveUp());
        }
    }

    private void FixedUpdate() {
        if (!movingUp && dualityModeController.WorldState == WorldSwitched.World.Light) {
            rbRigidbody2D.gravityScale = Math.Abs(rbRigidbody2D.gravityScale);
        }
    }

    private IEnumerator FallDown() {
        movingUp = false;
        yield return new WaitForSeconds(fallDelay);
        rbRigidbody2D.gravityScale = gravityScale;
        rbRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(disappearDelay);
        Destroy(gameObject);
    }
    
    private IEnumerator MoveUp() {
        movingUp = true;
        yield return new WaitForSeconds(riseDelay);
        rbRigidbody2D.gravityScale = 0;
        rbRigidbody2D.velocity = Vector2.up * risingSpeed;
        rbRigidbody2D.bodyType = RigidbodyType2D.Kinematic;
    }
}

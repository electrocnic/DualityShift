using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour {
    [SerializeField] private float fallDelay = 0.5f;
    [SerializeField] private float disappearDelay = 2f;
    private DualityModeController dualityModeController;

    private Rigidbody2D rbRigidbody2D;
    private void Start() {
        dualityModeController = Resources.FindObjectsOfTypeAll<DualityModeController>()[0];
        rbRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col) {
        StartCoroutine(FallDown());
    }

    private void FixedUpdate() {
        switch (dualityModeController.WorldState)
        {
            case WorldSwitched.World.Light:
                rbRigidbody2D.gravityScale = Math.Abs(rbRigidbody2D.gravityScale);
                break;
            case WorldSwitched.World.Dark:
                rbRigidbody2D.gravityScale = -Math.Abs(rbRigidbody2D.gravityScale);
                break;
        }
    }

    private IEnumerator FallDown() {
        yield return new WaitForSeconds(fallDelay);
        rbRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(disappearDelay);
        Destroy(gameObject);
    }
}

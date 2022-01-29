using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour {
    [SerializeField] private float fallDelay = 0.5f;
    [SerializeField] private float disappearDelay = 0.5f;

    private void OnCollisionEnter2D(Collision2D col) {
        StartCoroutine(FallDown());
    }

    private IEnumerator FallDown() {
        yield return new WaitForSeconds(fallDelay);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(disappearDelay);
        Destroy(gameObject);
    }
}

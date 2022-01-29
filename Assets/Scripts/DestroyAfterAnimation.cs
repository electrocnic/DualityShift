using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    private void Start() {
        GetComponent<Animator>().fireEvents = true;
        
    }
}

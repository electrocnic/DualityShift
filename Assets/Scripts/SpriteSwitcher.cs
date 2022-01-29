using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Assertions;

public class SpriteSwitcher : MonoBehaviour {
    private Animator SpriteAnimator;
    [SerializeField] AnimatorController spriteRainbowCharacter;
    [SerializeField] AnimatorController spriteDarkCharacter;
    private DualityModeController dualityModeController;

    private void Start() {
        SpriteAnimator = GetComponent<Animator>();
        dualityModeController = Resources.FindObjectsOfTypeAll<DualityModeController>()[0];
        dualityModeController.OnWorldSwitched += DualityModeControllerOnOnWorldSwitched;
        DualityModeControllerOnOnWorldSwitched(null, new WorldSwitched(dualityModeController.WorldState));
    }

    private void DualityModeControllerOnOnWorldSwitched(object sender, EventArgs eventArgs) {
        if (!gameObject) {
            return;
        }

        WorldSwitched ws = (WorldSwitched) eventArgs;
        switch (ws.NewWorld) {
            case WorldSwitched.World.Light:
                SpriteAnimator.runtimeAnimatorController = spriteRainbowCharacter;
                break;
            case WorldSwitched.World.Dark:
                SpriteAnimator.runtimeAnimatorController = spriteDarkCharacter;
                break;
        }
    }

    private void OnDestroy() {
        dualityModeController.OnWorldSwitched -= DualityModeControllerOnOnWorldSwitched;
    }
}
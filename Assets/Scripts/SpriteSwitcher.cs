using System;
using UnityEngine;

public class SpriteSwitcher : MonoBehaviour {
    private Animator SpriteAnimator;
    [SerializeField] RuntimeAnimatorController spriteRainbowCharacter;
    [SerializeField] RuntimeAnimatorController spriteDarkCharacter;
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
        if (dualityModeController) {
            dualityModeController.OnWorldSwitched -= DualityModeControllerOnOnWorldSwitched;
        }
    }
}
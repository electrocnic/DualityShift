using System;
using UnityEditor.Animations;
using UnityEngine;

public class DualityModeController : MonoBehaviour
{
    [SerializeField] CharacterController2d controller;
    [SerializeField] GameObject world1;
    [SerializeField] GameObject world2;
    [SerializeField] private float potionConsumationSpeed = 0.01f;
    [SerializeField] AnimatorController spriteRainbowCharacter;
    [SerializeField] AnimatorController spriteDarkCharacter;
    private Animator m_SpriteAnimator;

    public event EventHandler OnWorldSwitched;
    
    // Start is called before the first frame update
    void Start() {
        m_SpriteAnimator = controller.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        bool leftShiftPressed = Input.GetKeyDown(KeyCode.LeftShift);
        if (leftShiftPressed && controller.getPotionFillStatus() > 0.0f && !world2.activeInHierarchy)
        {
            WorldState = WorldSwitched.World.Dark;
            world2.SetActive(true);
            world1.SetActive(false);
            //controller.GetComponent<SpriteRenderer>().sprite = spriteDarkCharacter;
            m_SpriteAnimator.runtimeAnimatorController = spriteDarkCharacter;
            controller.GetComponent<SpriteRenderer>().flipX = true;
        } else if (controller.getPotionFillStatus() <= 0.0f || (leftShiftPressed && world2.activeInHierarchy)) {
            WorldState = WorldSwitched.World.Light;
            world1.SetActive(true);
            world2.SetActive(false);
            // controller.GetComponent<SpriteRenderer>().sprite = spriteRainbowCharacter;
            m_SpriteAnimator.runtimeAnimatorController = spriteRainbowCharacter;
            controller.GetComponent<SpriteRenderer>().flipX = false;
        }
        OnWorldSwitched?.Invoke(this, new WorldSwitched(WorldState));

        if (world2.activeInHierarchy) {
            controller.setPotionFillStatus(MathF.Max(0.0f, controller.getPotionFillStatus() - potionConsumationSpeed));
        }
    }

    public WorldSwitched.World WorldState { get; set; } = WorldSwitched.World.Light;
}

public class WorldSwitched : EventArgs {
    public WorldSwitched(World newWorld) {
        this.NewWorld = newWorld;
    }

    public enum World {
        Light,
        Dark,
    }

    public World NewWorld { get; set; }
}

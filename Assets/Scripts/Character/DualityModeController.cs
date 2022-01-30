using System;
using UnityEngine;
using UnityEngine.Serialization;

public class DualityModeController : MonoBehaviour
{
    [SerializeField] CharacterController2d controller;
    [SerializeField] GameObject world1;
    [SerializeField] GameObject world2;
    [SerializeField] private float potionConsumationSpeed = 0.01f;
    [FormerlySerializedAs("spriteRainbowCharacter")] [SerializeField] RuntimeAnimatorController animRainbowCharacter;
    [SerializeField] Sprite spriteRainbowCharacter;
    [FormerlySerializedAs("spriteDarkCharacter")] [SerializeField] RuntimeAnimatorController animDarkCharacter;
    [SerializeField] Sprite spriteDarkCharacter;
    private SpriteRenderer m_SpriteRenderer;
    private Animator m_SpriteAnimator;

    public event EventHandler OnWorldSwitched;
    
    // Start is called before the first frame update
    void Start() {
        m_SpriteRenderer = controller.GetComponent<SpriteRenderer>();
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
            m_SpriteRenderer.sprite = spriteDarkCharacter;
            m_SpriteAnimator.runtimeAnimatorController = animDarkCharacter;
            controller.GetComponent<SpriteRenderer>().flipX = true;
            
            m_SpriteAnimator.enabled = true;
        } else if (controller.getPotionFillStatus() <= 0.0f || (leftShiftPressed && world2.activeInHierarchy)) {
            WorldState = WorldSwitched.World.Light;
            world1.SetActive(true);
            world2.SetActive(false);
            m_SpriteRenderer.sprite = spriteRainbowCharacter;
            m_SpriteAnimator.runtimeAnimatorController = animRainbowCharacter;
            controller.GetComponent<SpriteRenderer>().flipX = false;
            
            m_SpriteAnimator.enabled = true;
        }
        OnWorldSwitched?.Invoke(this, new WorldSwitched(WorldState));
    }

    private void FixedUpdate() {
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

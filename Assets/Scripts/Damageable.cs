using UnityEngine;

public class Damageable : MonoBehaviour {
    [SerializeField] private float health = 100f;
    [SerializeField] private Transform pfPuff;
    [SerializeField] public string objectId;
    private WorldController m_WorldController;
    private CharacterController2d m_Player;

    private void Start() {
        m_WorldController = Resources.FindObjectsOfTypeAll<WorldController>()[0];
        m_Player = Resources.FindObjectsOfTypeAll<CharacterController2d>()[0];
    }

    public void Damage(float dmg) {
        if (!gameObject || !m_Player) {
            return;
        }

        health -= dmg;

        if (health > 0) {
            return;
        }

        if ((m_WorldController.Invincible || m_Player.invincibilityDuration > 0) &&
            ReferenceEquals(m_Player.gameObject, gameObject)) {
            return;
        }
        
        if (objectId == "birb") {
            Scoring.incrementBirbs();
        } else if (objectId == "baps") {
            Scoring.incrementBaps();
        } else if (objectId == "shroom") {
            Scoring.incrementShrooms();
        } else if (objectId == "player") {
            Scoring.incrementDeaths();
            WorldController.isDead = true;
        }

        var puffRef = Instantiate(pfPuff, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(puffRef.gameObject, 5f);
    }
}
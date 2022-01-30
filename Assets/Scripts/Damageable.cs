using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private Transform pfPuff;
    private WorldController m_WorldController;
    private GameObject m_Player;

    private void Start()
    {
        m_WorldController = Resources.FindObjectsOfTypeAll<WorldController>()[0];
        m_Player = Resources.FindObjectsOfTypeAll<CharacterController2d>()[0].gameObject;
    }

    public void Damage(float dmg)
    {
        if (!gameObject || !m_Player) {
            return;
        }
        health -= dmg;
        if (health <= 0) {
            if (m_WorldController.Invincible && ReferenceEquals(m_Player, gameObject))
            {
                return;
            }
            var puffRef = Instantiate(pfPuff, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(puffRef.gameObject, 5f);
        }
    }
}

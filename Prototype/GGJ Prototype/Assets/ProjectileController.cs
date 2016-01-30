using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour
{
    public float m_StepSize;
    public float m_HeightScale;

    float m_Step;
    float m_Distance;
    float m_DeltaY;
    float m_Direction;
    Vector3 m_LastPosition;
    Vector3 m_Start;
    Vector3 m_Target;
    bool m_Fired = false;
    GameObject m_Player;

    void Awake()
    {
        GetComponent<SphereCollider>().enabled = false;
    }

    public GameObject Fire(Vector3 target, Player player)
    {
        m_StepSize = player.m_ProjectileSpeed;
        m_HeightScale = player.m_ProjectileHeight;
        m_Start = transform.position;
        m_Target = target;
        m_Player = player.gameObject;
        m_Distance = Mathf.Abs(m_Target.x - m_Start.x);
        m_DeltaY = m_Target.y - m_Start.y;
        m_Direction = (m_Target.x - m_Start.x) / m_Distance;
        m_Fired = true;
        GetComponent<SphereCollider>().enabled = true;
        return gameObject;
    }

    void FixedUpdate()
    {
        if (m_Fired)
        {
            m_Step += m_StepSize;
            if (m_Step < m_Distance)
            {
                float y = (m_HeightScale * m_Distance) * Mathf.Sin(Mathf.Lerp(0, Mathf.PI, m_Step / m_Distance)) + (m_Step / m_Distance) * m_DeltaY;
                float z = Mathf.Lerp(m_Start.z, m_Target.z, m_Step / m_Distance);
                m_LastPosition = transform.position;
                transform.position = new Vector3(m_Start.x + m_Direction * m_Step, m_Start.y + y, z);
            }
            else if (GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().velocity = 60f * (transform.position - m_LastPosition);
                Destroy(gameObject, 10f);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject != m_Player)
        {
            if (!other.GetComponent<Player>().m_ProjectileSlot.Filled)
                other.GetComponent<Player>().m_ProjectileSlot.Fill();
            else
                other.GetComponent<Player>().Stun();

            // Destroy the projectile
            Destroy(gameObject);
        }
        // Collision with a block.
        if (other.CompareTag("Cube"))
        {
            bool parentNotFound = true;
            GameObject parent = other.transform.parent.gameObject;
            while (parent && parentNotFound)
            {
                Platform platform = parent.GetComponent<Platform>();
                if (platform)
                {
                    platform.DestroyBlock(other.transform.gameObject);
                    break;
                }
                parent = parent.transform.parent.gameObject;
            }

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}

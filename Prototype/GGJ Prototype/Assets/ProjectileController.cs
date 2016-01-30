using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour
{
    public float m_StepSize;
    public float m_HeightScale;

    float m_Step;
    float m_Distance;
    float m_Direction;
    Vector3 m_LastPosition;
    Vector3 m_Start;
    Vector3 m_Target;
    bool m_Fired = false;

    public GameObject Fire(Vector3 target)
    {
        m_Start = transform.position;
        m_Target = target;
        m_Distance = Mathf.Abs(m_Target.x - m_Start.x);
        m_Direction = (m_Target.x - m_Start.x) / m_Distance;
        m_Fired = true;
        return gameObject;
    }

    void FixedUpdate()
    {
        if (m_Fired)
        {
            m_Step += m_StepSize;
            if (m_Step < m_Distance)
            {
                float y = (m_HeightScale * m_Distance) * Mathf.Sin(Mathf.Lerp(0, Mathf.PI, m_Step / m_Distance));
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
        // Collision with a block.
        if (other.CompareTag("Cube"))
        {
            // Get corners of object.
            Vector3[] positions = {
                        other.transform.position,
                        // Corners.
                        new Vector3(other.transform.position.x + other.transform.localScale.x / 2f, other.transform.position.y, other.transform.position.z + other.transform.localScale.z / 2f),
                        new Vector3(other.transform.position.x - other.transform.localScale.x / 2f, other.transform.position.y, other.transform.position.z - other.transform.localScale.z / 2f),
                        new Vector3(other.transform.position.x - other.transform.localScale.x / 2f, other.transform.position.y, other.transform.position.z + other.transform.localScale.z / 2f),
                        new Vector3(other.transform.position.x + other.transform.localScale.x / 2f, other.transform.position.y, other.transform.position.z - other.transform.localScale.z / 2f),
                        // Line centers.
                        new Vector3(other.transform.position.x + other.transform.localScale.x / 2f, other.transform.position.y, other.transform.position.z),
                        new Vector3(other.transform.position.x - other.transform.localScale.x / 2f, other.transform.position.y, other.transform.position.z),
                        new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z + other.transform.localScale.z / 2f),
                        new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z - other.transform.localScale.z / 2f)
                    };

            bool playerHit = false;
            foreach (Vector3 position in positions)
            {
                // Check if the player is above us
                RaycastHit hit2;
                if (Physics.Raycast(position, transform.up, out hit2, float.PositiveInfinity))
                {
                    if (hit2.transform.gameObject.CompareTag("Player"))
                    {
                        hit2.transform.gameObject.GetComponent<Player>().Decay();
                        playerHit = true;
                        break;
                    }
                }
            }
            // If no player above us
            if (!playerHit)
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
            }
            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}

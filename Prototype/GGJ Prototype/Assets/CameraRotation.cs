using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour
{
    public Vector3[] m_Targets;
    [Range(0, 2f)]
    public float m_Speed;

    float m_Step = 1f;
    int m_CurrentTarget;

    Vector3 m_OriginalPosition;
    Quaternion m_OriginalRotation;
    Vector3 m_StartPosition;
    Quaternion m_StartRotation;

    void Awake()
    {
        m_OriginalPosition = transform.position;
        m_OriginalRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        // You don't work step.
        // Please fuck off.
        m_Step += Time.deltaTime * m_Speed;
        if (m_Step >= 1f)
        {
            m_CurrentTarget++;
            if (m_CurrentTarget == m_Targets.Length)
            {
                m_StartPosition = transform.position;
                m_StartRotation = transform.rotation;
            }
            if (m_CurrentTarget > m_Targets.Length)
                m_CurrentTarget = 0;

            m_Step = 0;
        }

        if (m_CurrentTarget < m_Targets.Length)
        {
            Vector3 direction = m_Targets[m_CurrentTarget] * m_Speed * Time.deltaTime;
            transform.RotateAround(Vector3.zero, direction.normalized, -direction.magnitude / m_Speed);
        }
        else
        {
            transform.position = Vector3.Lerp(m_StartPosition, m_OriginalPosition, m_Step);
            transform.rotation = Quaternion.Lerp(m_StartRotation, m_OriginalRotation, m_Step);
        }
    }
}
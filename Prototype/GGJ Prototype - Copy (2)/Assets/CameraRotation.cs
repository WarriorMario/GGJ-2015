using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour
{
    public Vector3[] m_Targets;
    [Range(0, 2f)]
    public float m_Speed;

    float m_Step;
    float m_Duration;
    int m_CurrentTarget;
    Vector3 m_StartRotation;

    void Awake()
    {
        m_Step = m_Duration;
    }

    void FixedUpdate()
    {
        m_Step += Time.deltaTime;
        if (m_Step >= m_Duration)
        {
            m_CurrentTarget++;
            if (m_CurrentTarget == m_Targets.Length)
                m_CurrentTarget = 0;

            m_Duration = m_Targets[m_CurrentTarget].magnitude * Time.deltaTime / m_Speed;
            m_StartRotation = transform.eulerAngles;
            m_Step = 0;
        }

        Vector3 direction = m_Targets[m_CurrentTarget] * m_Speed * Time.deltaTime;
        transform.RotateAround(Vector3.zero, direction.normalized, direction.magnitude);
    }
}
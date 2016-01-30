using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour
{
    public float m_StepSize;
    public float m_HeightScale;

    float m_Step;
    float m_Distance;
    float m_Direction;
    Vector3 m_Start;
    Vector3 m_Target;
    bool m_Fired = false;

    public void Fire(Vector3 target)
    {
        m_Start = transform.position;
        m_Target = target;
        m_Distance = Mathf.Abs(m_Target.x - m_Start.x);
        m_Direction = (m_Target.x - m_Start.x) / m_Distance;
        m_Fired = true;
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
                transform.position = new Vector3(m_Start.x + m_Direction * m_Step, m_Start.y + y, z);
            }
            else
            {
                transform.position = m_Target;
                Destroy(gameObject, 2f);
            }
        }
    }
}

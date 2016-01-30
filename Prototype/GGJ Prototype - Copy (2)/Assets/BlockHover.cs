using UnityEngine;
using System.Collections;

public class BlockHover : MonoBehaviour
{
    public float m_Amplitude;
    public float m_Duration;

    float m_Step;
    float m_StartY;

    void Awake()
    {
        m_StartY = transform.position.y;
        m_Step = Random.value * m_Duration;
    }

    void FixedUpdate()
    {
        m_Step += Time.deltaTime / (m_Duration / 2f);
        float y = m_Amplitude * Mathf.Sin(Mathf.Lerp(0, 2f * Mathf.PI, m_Step / (m_Duration / 2f)));
        transform.position = new Vector3(transform.position.x, m_StartY + y, transform.position.z);

        // Reset.
        if (m_Step > m_Duration / 2f)
            m_Step = 0;
    }
}

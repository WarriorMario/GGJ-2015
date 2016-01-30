using UnityEngine;
using System.Collections;

public class BlockHover : MonoBehaviour
{
    public float m_Distance;
    public float m_Duration;

    float m_Step;
    float m_StartY;

	void Awake ()
    {
        m_Step = Random.value * m_Duration;
	}
	
	void FixedUpdate ()
    {
        m_Step += Time.deltaTime / m_Duration;
        float y = m_Distance * Mathf.Sin(Mathf.Lerp(0, Mathf.PI, m_Step / m_Duration));
        transform.position = new Vector3(transform.position.x, m_StartY + y, transform.position.z);

        // Reset.
        if (m_Step > m_Duration)
            m_Step = 0;
	}
}

using UnityEngine;
using System.Collections;

public class OrbTracker : MonoBehaviour
{
    bool m_OnTarget = false;

    public float m_Speed;
    public float m_YOffset;
    GameObject m_Projectile;

    bool m_WasInitialized;

    // Use this for initialization
    public void Initialize(GameObject projectile)
    {
        m_Projectile = projectile;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Projectile != null)
        {
            m_WasInitialized = true;
            if (!m_OnTarget)
            {
                transform.position += ((m_Projectile.transform.position - new Vector3(0, m_YOffset, 0)) - transform.position).normalized * m_Speed * Time.deltaTime;
                if (((m_Projectile.transform.position - new Vector3(0, m_YOffset, 0)) - transform.position).magnitude < 0.2)
                {
                    m_OnTarget = true;
                }
            }
            else
            {
                transform.position = m_Projectile.transform.position - new Vector3(0, m_YOffset, 0);
            }
        }
        else if(m_WasInitialized)
        {
            Destroy(this.gameObject);
        }
    }
}

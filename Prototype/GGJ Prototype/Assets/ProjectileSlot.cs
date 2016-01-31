using UnityEngine;
using System.Collections;

public class ProjectileSlot : MonoBehaviour
{
    public ProjectileController m_ProjectilePrefab;
    public OrbTracker m_OrbTrackerPrefab;

    GameObject m_Projectile;
    GameObject m_ActiveOrbTracker;
    public bool Filled { get; set; }

    public void Charge(Vector3 position)
    {
        m_ActiveOrbTracker = Instantiate(m_OrbTrackerPrefab.gameObject, position, Quaternion.identity) as GameObject;
    }

    public void Fill()
    {
        if (!Filled)
        {
            m_Projectile = Instantiate(m_ProjectilePrefab.gameObject, transform.position, transform.rotation) as GameObject;
            m_Projectile.transform.SetParent(transform);

            if (m_ActiveOrbTracker != null)
                m_ActiveOrbTracker.GetComponent<OrbTracker>().Initialize(m_Projectile);

            Filled = true;
        }
    }
    public void Fire(Vector3 target)
    {
        if (Filled)
        {
            m_Projectile.transform.SetParent(null);
            m_Projectile.GetComponent<ProjectileController>().Fire(target,transform.parent.GetComponent<Player>());

            Filled = false;
        }
    }
}

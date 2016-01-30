using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public GameObject[] m_PlayerStates;

    public int m_Player;
    public float m_XSpeed;
    public float m_ZSpeed;

    public float m_PickupOffset;
    public ProjectileSlot m_ProjectileSlot;
    public GameObject m_BlockSelector;
    public GameObject m_GatherEffectPrefab;
    public float m_TimerLimit = 1.0f;

    CharacterController m_CharacterController;
    Vector3 m_FaceDirection;
    Vector3 m_MoveDirection;
    string m_HorizontalInput = "Horizontal";
    string m_VerticalInput = "Vertical";
    string m_PickupInput = "Pickup";

    GameObject m_LastHit;
    bool m_PickingUp = false;
    bool m_Charging = false;
    float m_Timer = 0.0f;

    // Use this for initialization
    void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_HorizontalInput += m_Player.ToString();
        m_VerticalInput += m_Player.ToString();
        m_PickupInput += m_Player.ToString();

        m_FaceDirection = Vector3.forward;
        m_MoveDirection = Vector3.zero;

        bool parentNotFound = true;
        GameObject parent = GetBlock().transform.parent.gameObject;
        while (parent && parentNotFound)
        {
            Platform platform = parent.GetComponent<Platform>();
            if (platform)
            {
                platform.m_Players.Add(this);
                parentNotFound = false;
                platform.transform.parent.GetComponent<PlatformManager>().Init();
                break;
            }
            parent = parent.transform.parent.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_Charging)
        {
            if (Mathf.Abs(Input.GetAxis(m_HorizontalInput)) > Mathf.Abs(Input.GetAxis(m_VerticalInput)))
            {
                m_MoveDirection = new Vector3(Input.GetAxis(m_HorizontalInput) * m_XSpeed, 0, 0);
            }
            else
            {
                m_MoveDirection = new Vector3(0, 0, Input.GetAxis(m_VerticalInput) * m_ZSpeed);
            }
        }

        // Pickup.
        if (m_LastHit != null)
        {
            if (Input.GetButtonDown(m_PickupInput))
            {
                m_MoveDirection = Vector3.zero;
                m_Charging = true;
                m_Timer = 0;
                // Spawn effect.
                GameObject obj = Instantiate(m_GatherEffectPrefab, m_LastHit.transform.position, Quaternion.identity) as GameObject;
            }

            if(m_Charging)
            {
                m_Timer += Time.deltaTime;
                if (m_Timer >= m_TimerLimit)
                {
                    m_Timer = 0;
                    m_PickingUp = true;
                    m_Charging = false;
                }
            }
            else if(m_PickingUp)
            {
                m_LastHit.GetComponent<Rigidbody>().useGravity = true;
                m_LastHit.GetComponent<BoxCollider>().enabled = false;

                bool parentNotFound = true;
                GameObject parent = m_LastHit.transform.parent.gameObject;
                while (parent && parentNotFound)
                {
                    Platform platform = parent.GetComponent<Platform>();
                    if (platform)
                    {
                        platform.DestroyBlock(m_LastHit);
                        break;
                    }
                    parent = parent.transform.parent.gameObject;
                }
                m_LastHit = null;
                m_PickingUp = false;

                if(m_BlockSelector.activeSelf)
                    m_ProjectileSlot.Fill();
            }
        }
    }

    void FixedUpdate()
    {
        if (m_MoveDirection.x != 0 || m_MoveDirection.z != 0)
        {
            m_FaceDirection = m_MoveDirection.normalized;
            transform.rotation = Quaternion.LookRotation(m_MoveDirection.normalized);
        }

        m_BlockSelector.SetActive(false);
        // Mark new.
        if (!m_ProjectileSlot.Filled)
            MarkForwardBlock();
        else
            m_LastHit = null;

        m_CharacterController.Move(m_MoveDirection);

        if (Floating())
        {
            m_CharacterController.Move(-m_MoveDirection);
        }
    }

    void MarkForwardBlock()
    {
        // Block pickup.
        Vector3 direction = m_FaceDirection.normalized * m_PickupOffset;
        Vector3 position = new Vector3(transform.position.x + direction.x, transform.position.y, transform.position.z + direction.z);
        RaycastHit hit;
        if (Physics.Raycast(position, -transform.up, out hit, float.PositiveInfinity))
        {
            m_BlockSelector.transform.position = hit.transform.position;
            m_LastHit = hit.transform.gameObject;
            m_BlockSelector.SetActive(true);
        }
    }

    public GameObject GetBlock()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, float.PositiveInfinity))
        {
            return hit.collider.gameObject;
        }
        else
        {
            // FALL
            return null;
        }
    }

    public bool Floating()
    {
        RaycastHit hit;

        // Get corners of collider.
        Bounds collider = GetComponent<Collider>().bounds;
        Vector3[] positions = {
            new Vector3(collider.center.x + collider.extents.x, collider.center.y, collider.center.z + collider.extents.z),
            new Vector3(collider.center.x - collider.extents.x, collider.center.y, collider.center.z - collider.extents.z),
            new Vector3(collider.center.x - collider.extents.x, collider.center.y, collider.center.z + collider.extents.z),
            new Vector3(collider.center.x + collider.extents.x, collider.center.y, collider.center.z - collider.extents.z)
        };

        foreach (Vector3 position in positions)
        {
            if (!Physics.Raycast(position, -transform.up, out hit, float.PositiveInfinity))
                return true;
        }
        return false;
    }

    public void Decay()
    {
        for (int state = 0; state < m_PlayerStates.Length; state++)
        {
            // Disable the first found active state.
            if (m_PlayerStates[state].activeSelf)
            {
                m_PlayerStates[state].SetActive(false);
                break;
            }
        }
    }
}

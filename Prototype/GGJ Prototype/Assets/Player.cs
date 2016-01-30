using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int m_Player;
    public float m_XSpeed;
    public float m_ZSpeed;
    public float m_ChargeTime = 1.0f;
    public float m_ProjectileSpeed = 0.1f;
    public float m_ProjectileHeight = 0.5f;
    public float m_StunDuration;

    float m_PickupOffset;
    public ProjectileSlot m_ProjectileSlot;
    public GameObject m_BlockSelector;
    public GameObject m_GatherEffectPrefab;

    CharacterController m_CharacterController;
    Vector3 m_FaceDirection;
    Vector3 m_MoveDirection;
    string m_HorizontalInput = "Horizontal";
    string m_VerticalInput = "Vertical";
    string m_PickupInput = "Pickup";

    GameObject m_ChargeEffect;
    bool m_PickingUp = false;
    bool m_Charging = false;
    float m_Timer = 0.0f;
    float m_StunTimer = 0.0f;

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

        m_PickupOffset = GetComponent<BoxCollider>().bounds.extents.x + 1.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Charging)
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

        GameObject marked = MarkForwardBlock();
        // Pickup.
        if (marked != null && !m_ProjectileSlot.Filled && m_Charging == false)
        {
            if (Input.GetButtonDown(m_PickupInput) && !m_PickingUp)
            {
                m_MoveDirection = Vector3.zero;
                m_Charging = true;
                m_Timer = 0;

                // Spawn effect.
                //m_ProjectileSlot.Fill();
            }
        }

        if (m_Charging)
        {
            m_Timer += Time.deltaTime;
            if (m_Timer >= m_ChargeTime)
            {
                m_Timer = 0;
                m_PickingUp = true;
                m_Charging = false;
            }
        }
        else if (m_PickingUp)
        {
            m_BlockSelector.SetActive(false);
            marked.GetComponent<Rigidbody>().useGravity = true;
            marked.GetComponent<BoxCollider>().enabled = false;

            bool parentNotFound = true;
            GameObject parent = marked.transform.parent.gameObject;
            while (parent && parentNotFound)
            {
                Platform platform = parent.GetComponent<Platform>();
                if (platform)
                {
                    platform.DestroyBlock(marked);
                    break;
                }
                parent = parent.transform.parent.gameObject;
            }
            m_ProjectileSlot.Fill();

            m_PickingUp = false;
        }
    }

    void FixedUpdate()
    {
        if (Floating())
        {
            gameObject.AddComponent<Rigidbody>().useGravity = true;
            GetComponent<Player>().enabled = false;
            Destroy(gameObject, 10f);
        }
        else
        {
            if (m_MoveDirection.x != 0 || m_MoveDirection.z != 0)
            {
                m_FaceDirection = m_MoveDirection.normalized;
                transform.rotation = Quaternion.LookRotation(m_MoveDirection.normalized);
            }
            m_CharacterController.Move(m_MoveDirection);

            if (Floating())
            {
                m_CharacterController.Move(-m_MoveDirection);
            }
        }
    }

    GameObject MarkForwardBlock()
    {
        // Block pickup.
        Vector3 direction = m_FaceDirection.normalized * m_PickupOffset;
        Vector3 position = new Vector3(transform.position.x + direction.x, transform.position.y, transform.position.z + direction.z);
        RaycastHit hit;
        if (Physics.Raycast(position, -transform.up, out hit, float.PositiveInfinity))
        {
            m_BlockSelector.transform.position = hit.transform.GetChild(0).position;
            m_BlockSelector.SetActive(true);
            return hit.transform.gameObject;
        }
        else
        {
            m_BlockSelector.SetActive(false);
            return null;
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
        Bounds collider = GetComponent<BoxCollider>().bounds;
        Vector3[] positions = {
            new Vector3(collider.center.x + collider.extents.x, collider.center.y, collider.center.z + collider.extents.z),
            new Vector3(collider.center.x - collider.extents.x, collider.center.y, collider.center.z - collider.extents.z),
            new Vector3(collider.center.x - collider.extents.x, collider.center.y, collider.center.z + collider.extents.z),
            new Vector3(collider.center.x + collider.extents.x, collider.center.y, collider.center.z - collider.extents.z)
        };
        
        foreach (Vector3 position in positions)
        {
            if (Physics.Raycast(position, -transform.up, out hit, float.PositiveInfinity) == false)
                return true;
            else if (hit.transform.GetComponent<Rigidbody>() != null && hit.transform.GetComponent<Rigidbody>().useGravity)
                return true;
        }
        return false;
    }

    public void Stun()
    {
        // Play animation.

        float m_StunTimer = m_StunDuration;
    }
}

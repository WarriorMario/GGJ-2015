using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    CharacterController m_CharacterController;
    Vector3 m_MoveDirection;
	// Use this for initialization
	void Awake ()
    {
        m_CharacterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        m_CharacterController.Move(m_MoveDirection);
	}
}

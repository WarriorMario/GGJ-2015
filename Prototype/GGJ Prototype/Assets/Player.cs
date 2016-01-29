using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    CharacterController m_CharacterController;
	// Use this for initialization
	void Awake ()
    {
        m_CharacterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}

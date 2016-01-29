using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonManager : MonoBehaviour {

    public ButtonFunctions m_ActiveButton;
    public int m_MinPlayerAmount;
    public int m_MaxPlayerAmount;
    public Text m_NumberSelection;
    public float m_ButtonTimer;

    private int m_SelectedAmount = 2;
    private float m_Timer = 0.0f;
    // Update is called once per frame
    void Update ()
    {
        if (!m_NumberSelection.gameObject.activeSelf)
        {
            m_ActiveButton.Active();
        }
        else
        {
            m_Timer -= Time.deltaTime;
            if (Input.GetAxis("Vertical0") > 0 && m_Timer <= 0)
            {
                m_Timer = m_ButtonTimer;
                m_SelectedAmount++;
                if(m_SelectedAmount > m_MaxPlayerAmount)
                {
                    m_SelectedAmount = m_MaxPlayerAmount;
                }
            }
            else if (Input.GetAxis("Vertical0") < 0 && m_Timer <= 0)
            {
                m_Timer = m_ButtonTimer;
                m_SelectedAmount--;
                if(m_SelectedAmount < m_MinPlayerAmount)
                {
                    m_SelectedAmount = m_MinPlayerAmount;
                }
            }
            m_NumberSelection.text = m_SelectedAmount.ToString();
            if( Input.GetButtonDown("Submit"))
            {
                InterSceneVars.s_AmountOfPlayers = m_SelectedAmount;
                SceneManager.LoadScene("Prototype 1");
            }
        }
	}
}

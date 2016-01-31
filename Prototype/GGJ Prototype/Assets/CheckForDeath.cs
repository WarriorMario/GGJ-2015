using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class CheckForDeath : MonoBehaviour
{
    public static CheckForDeath access;

    public SkullScript m_Skulls;
    List<Player> m_Players;
    int m_PlayersLeft;

    void Awake()
    {
        access = (access == null) ? this : access;
    }

	void Start ()
    {
        m_PlayersLeft = InterSceneVars.s_AmountOfPlayers;
        m_Players = new List<Player>();
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in playerObjects)
            m_Players.Add(p.GetComponent<Player>());
	}

    public void OnPlayerDeath()
    {
        m_PlayersLeft--;
        if(InterSceneVars.s_AmountOfPlayers == 2)
        {
            if(m_PlayersLeft <= 1)
                DoEnd();
        }
        else if(InterSceneVars.s_AmountOfPlayers == 4)
        {
            if (m_PlayersLeft == 2)
            {
                Player[] ps = new Player[2];
                int index = 0;
                foreach(Player p in m_Players)
                {
                    if(p != null)
                    {
                        ps[index] = p;
                        index++;
                    }
                }
                if(ps[0].m_Team == ps[1].m_Team)
                    DoEnd();
            }
            else if(m_PlayersLeft == 1)
                DoEnd();
        }
        else
        {
            Debug.Log("There is a problem with the amount of players in InterSceneVars... it should be either 2 or 4");
        }
    }

    // Game has ended.
    void DoEnd()
    {
        foreach (Player p in m_Players)
        {
            if (p.Alive)
                InterSceneVars.s_WinningTeam = p.m_Team;
        }
        m_Skulls.StartFalling();
    }
}

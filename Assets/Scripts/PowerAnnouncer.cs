using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerAnnouncer : MonoBehaviour {

    public GameObject m_AnnounceText;

	// Use this for initialization
	void Start () {
        m_AnnounceText.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AnnouncePower(PowerupManager.PowerUps power)
    {

    }
}

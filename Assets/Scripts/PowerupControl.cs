using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupControl : MonoBehaviour {

    public int m_PowerTime;

    private List<Pair<PowerupManager.PowerUps, int>> m_ActiveMods;

    private int m_SizeModsInUse;

    private void Start()
    {
        m_SizeModsInUse = 0;
        m_ActiveMods = new List<Pair<PowerupManager.PowerUps, int>>();
    }

    public void ApplyMod(PowerupManager.PowerUps mod)
    {
        switch(mod)
        {
            case PowerupManager.PowerUps.BIGBALLS:
                transform.localScale = new Vector3(3, 3, 3);
                m_ActiveMods.Add(new Pair<PowerupManager.PowerUps, int>(PowerupManager.PowerUps.BIGBALLS, 0));
                ++m_SizeModsInUse;
                break;

            case PowerupManager.PowerUps.SMALLBALLS:
                transform.localScale = new Vector3(1, 1, 1);
                m_ActiveMods.Add(new Pair<PowerupManager.PowerUps, int>(PowerupManager.PowerUps.BIGBALLS, 0));
                ++m_SizeModsInUse;
                break;
        }
    }

    public void ApplyMod(PowerupManager.PowerUps mod, int timerStartTime)
    {
        switch (mod)
        {
            case PowerupManager.PowerUps.BIGBALLS:
                transform.localScale = new Vector3(3, 3, 3);
                m_ActiveMods.Add(new Pair<PowerupManager.PowerUps, int>(PowerupManager.PowerUps.BIGBALLS, timerStartTime));
                ++m_SizeModsInUse;
                break;

            case PowerupManager.PowerUps.SMALLBALLS:
                transform.localScale = new Vector3(1, 1, 1);
                m_ActiveMods.Add(new Pair<PowerupManager.PowerUps, int>(PowerupManager.PowerUps.BIGBALLS, timerStartTime));
                ++m_SizeModsInUse;
                break;
        }
    }

    // Update is called once per frame
    void Update () {
		if(m_ActiveMods.Count > 0)
        {
            List<Pair<PowerupManager.PowerUps, int>> endedMods = new List<Pair<PowerupManager.PowerUps, int>>();
            foreach(Pair<PowerupManager.PowerUps, int> mod in m_ActiveMods)
            {
                ++mod.Second;
                if (mod.Second >= m_PowerTime) // Deactivate mod
                {
                    endedMods.Add(mod);
                    switch(mod.First)
                    {
                        case PowerupManager.PowerUps.BIGBALLS:
                        case PowerupManager.PowerUps.SMALLBALLS:
                            --m_SizeModsInUse;
                            if(m_SizeModsInUse == 0) // This makes sure we dont reset ball size when another mod should be affecting it
                                transform.localScale = new Vector3(2, 2, 2);
                            break;
                    }
                }
            }

            foreach(Pair<PowerupManager.PowerUps, int> mod in endedMods)
            {
                m_ActiveMods.Remove(mod);
            }
        }
	}
}

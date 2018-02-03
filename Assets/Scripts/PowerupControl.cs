using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupControl : MonoBehaviour {

    public int m_PowerTime;

    private List<Pair<PowerupManager.PowerUps, int>> m_ActiveMods;

    private int m_SizeModsInUse;
    private int m_BounceModsInUse;

    public PhysicMaterial m_BouncyBallMat;
    public PhysicMaterial m_UnbouncyBallMat;
    public PhysicMaterial m_NormalBallMat;

    private void Start()
    {
        m_SizeModsInUse = 0;
        m_BounceModsInUse = 0;
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
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                m_ActiveMods.Add(new Pair<PowerupManager.PowerUps, int>(PowerupManager.PowerUps.SMALLBALLS, 0));
                ++m_SizeModsInUse;
                break;

            case PowerupManager.PowerUps.CLUSTERBALLS:
                GameObject.Instantiate(gameObject).transform.position = transform.position;
                GameObject.Instantiate(gameObject).transform.position = transform.position;
                break;

            case PowerupManager.PowerUps.BOUNCYBALLS:
                GetComponent<SphereCollider>().material = m_BouncyBallMat;
                ++m_BounceModsInUse;
                break;

            case PowerupManager.PowerUps.UNBOUNCYBALLS:
                GetComponent<SphereCollider>().material = m_UnbouncyBallMat;
                ++m_BounceModsInUse;
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

                        case PowerupManager.PowerUps.BOUNCYBALLS:
                        case PowerupManager.PowerUps.UNBOUNCYBALLS:
                            --m_BounceModsInUse;
                            if (m_BounceModsInUse == 0)
                                GetComponent<SphereCollider>().material = m_NormalBallMat;
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

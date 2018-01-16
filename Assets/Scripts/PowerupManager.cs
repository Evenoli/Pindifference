using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour {

    public enum PowerUps { BIGBALLS, SMALLBALLS }

    public int m_RechargeTime;
    private int m_RechargeTimer;
    private bool m_Recharging;

    private Queue<PowerUps> m_PowerQueue;

    public GameObject m_OpponentTable;

    public PowerupDisplay m_PowerDisp;

    public string inputName;
    private bool m_axisPressed;


    // Use this for initialization
    void Start () {
        m_Recharging = false;
        m_PowerQueue = new Queue<PowerUps>();

    }
	
	// Update is called once per frame
	void Update () {
		if(m_Recharging)
        {
            ++m_RechargeTimer;
            if(m_RechargeTimer >= m_RechargeTime)
            {
                m_Recharging = false;
            }
        }
        else
        {
            if (Input.GetAxis(inputName) == 1 && !m_axisPressed)
            {
                UsePowerup();
                m_axisPressed = true;
            }

            if (Input.GetAxis(inputName) == 0)
                m_axisPressed = false;
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            AddRandomPowerToQueue();
        }
	}

    public void AddPowerToQueue(PowerUps power)
    {
        m_PowerQueue.Enqueue(power);

        if (m_PowerQueue.Count == 1)
            m_PowerDisp.SetDisplayActive(power);
    }

    public void AddRandomPowerToQueue()
    {
        int numPowers = PowerUps.GetNames(typeof(PowerUps)).Length;
        int selection = (int)(Random.value * numPowers);
        AddPowerToQueue((PowerUps)selection);

        if (m_PowerQueue.Count == 1)
            m_PowerDisp.SetDisplayActive((PowerUps)selection);
    }

    private void UsePowerup()
    {
        if (m_Recharging || m_PowerQueue.Count == 0)
            return;

        PowerUps power = m_PowerQueue.Dequeue();
        if (m_PowerQueue.Count == 0)
            m_PowerDisp.SetDisplayInactive();
        else
            m_PowerDisp.StartRechargeAnim(m_RechargeTime, m_PowerQueue.Peek());

        switch(power)
        {
            case PowerUps.SMALLBALLS:
            case PowerUps.BIGBALLS:
                List<GameObject> oppBalls = m_OpponentTable.GetComponent<BallManager>().GetBalls();
                foreach(GameObject ball in oppBalls)
                    ball.GetComponent<PowerupControl>().ApplyMod(power);

                break;
        }

        m_Recharging = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PowerupManager : MonoBehaviour {

    public enum PowerUps { BIGBALLS, SMALLBALLS, CLUSTERBALLS, BOUNCYBALLS, UNBOUNCYBALLS, SLOMO, FLIPPERSWAP }

    public int m_RechargeTime;
    private int m_RechargeTimer;
    private bool m_Recharging;

    private Queue<PowerUps> m_PowerQueue;

    public GameObject m_OpponentTable;

    public PowerupDisplay m_PowerDisp;

    public int m_FlipperSwapDuration;
    private int m_FlipperSwapTimer;
    private bool m_FlippersSwapped;

    public string inputName;
    private bool m_axisPressed;

    public FlipperControl[] m_LeftFlippers;
    public FlipperControl[] m_RightFlippers;

    public GameManager m_GameMan;

	public PowerAnnouncer m_PowerAnnouncer;
	public PowerAnnouncer m_OppPowerAnnouncer;

    // Use this for initialization
    void Start () {
        m_Recharging = false;
        m_PowerQueue = new Queue<PowerUps>();
        m_FlippersSwapped = false;
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

        if(m_FlippersSwapped)
        {
            ++m_FlipperSwapTimer;
            if(m_FlipperSwapTimer >= m_FlipperSwapDuration)
            {
                m_FlippersSwapped = false;
                FlipperControl[] oppLeftFlippers = m_OpponentTable.GetComponent<PowerupManager>().m_LeftFlippers;
                FlipperControl[] oppRightFlippers = m_OpponentTable.GetComponent<PowerupManager>().m_RightFlippers;

                foreach (FlipperControl lFlip in oppLeftFlippers)
                {
                    StringBuilder sb = new StringBuilder(lFlip.inputName);
                    sb[0] = 'L';
                    lFlip.inputName = sb.ToString();
                }

                foreach (FlipperControl rFlip in oppRightFlippers)
                {
                    StringBuilder sb = new StringBuilder(rFlip.inputName);
                    sb[0] = 'R';
                    rFlip.inputName = sb.ToString();
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            AddPowerToQueue(PowerUps.FLIPPERSWAP);
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

        

        switch (power)
        {
		case PowerUps.SMALLBALLS:
		case PowerUps.BIGBALLS:
		case PowerUps.CLUSTERBALLS:
		case PowerUps.BOUNCYBALLS:
		case PowerUps.UNBOUNCYBALLS:
			List<GameObject> oppBalls = m_OpponentTable.GetComponent<BallManager> ().GetBalls ();
			m_OppPowerAnnouncer.AnnouncePower (power);
            if (oppBalls.Count > 0)
            {
                GameObject[] ballArr = new GameObject[oppBalls.Count];
                oppBalls.CopyTo(ballArr);

                foreach (GameObject ball in ballArr)
                    ball.GetComponent<PowerupControl>().ApplyMod(power);
            }
            break;

		case PowerUps.SLOMO:
			m_GameMan.ActivateSloMo ();
			m_OppPowerAnnouncer.AnnouncePower (power);
			m_PowerAnnouncer.AnnouncePower (power);
            break;

        case PowerUps.FLIPPERSWAP:
            FlipperControl[] oppLeftFlippers = m_OpponentTable.GetComponent<PowerupManager>().m_LeftFlippers;
            FlipperControl[] oppRightFlippers = m_OpponentTable.GetComponent<PowerupManager>().m_RightFlippers;

			m_OppPowerAnnouncer.AnnouncePower (power);

            foreach(FlipperControl lFlip in oppLeftFlippers)
            {
                StringBuilder sb = new StringBuilder(lFlip.inputName);
                sb[0] = 'R';
                lFlip.inputName = sb.ToString();
            }

            foreach (FlipperControl rFlip in oppRightFlippers)
            {
                StringBuilder sb = new StringBuilder(rFlip.inputName);
                sb[0] = 'L';
                rFlip.inputName = sb.ToString();
            }

            m_FlippersSwapped = true;
            m_FlipperSwapTimer = 0;

            break;

        }

        m_Recharging = true;
        m_RechargeTimer = 0;
    }
}

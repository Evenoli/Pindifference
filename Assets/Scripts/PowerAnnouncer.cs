using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerAnnouncer : MonoBehaviour {

    public GameObject m_AnnounceText;

	public Color[] m_PossibleTextCols;

	private bool m_TextActive;

	private TextMesh m_tMesh;

	public float m_FadeRate;
	public float m_RiseRate;

	private string[] m_BigBallAnnounces = { "BIG OL' BALLS!", "BIG BALLS!", "THE BIGGEST OF BALLS!", "MY BALLS LOOK BIG IN THIS?",
		"BIGGIE BALLS", "BALLS ON SWOLL!", "OVERCOMPENSATION BALLS!", "BIG BALLS RULE OK" };

	private string[] m_SmallBallAnnounces = { "SMALL BALLS!", "LIL BALLS!", "WEE BABY PINBALLS!", "UNDERCOMPENSATION BALLS!", 
		"WHERE'D MA BALLS GO?", "HONEY, I SHRUNK THE BALLS!", "SMALL BALLS RULE OK" };

	private string[] m_ClusterBallAnnounces = { "CLUSTER BALLS!", "AN ABSOLUTE CLUSTERBALL!", "THEY'RE REPRODUCING!", "3 FOR 1!", 
		"MULTI, MULTI, MULTIBALL!", "NEED 4 BALLS 3: CLUSTERBALLS" };

	private string[] m_BouncyBallAnnounces = { "BOUNCY BALLS!", "SUPER MEGA BOUNCE BALLS!", "BOUNCY BALLS, BOOOIIII!", "HECKIN' BOUNCY BALLS!", 
		"RUBBER BALLS!", "THE TING GO BOUNCE!" };

	private string[] m_UnbouncyBallsAnnounces = { "UNBOUNCY BALLS!", "BORING, UNBOUNCY BALLS!", "WOODEN BALLS!", "MAN'S NOT BOUNCE", 
		"NO BOUNCIN ALLOWED" };
	
	private string[] m_SloMoAnnounces = {
		"SLOOOOOO MOOOOOOO...",
		"SLOW DOOOWWWWN",
		"SLOW MOTION, BOI!",
		"WOAH THERE, CHEIF!",
		"EEEEAAAASY TIGER..,",
		"TOTEM ANIMAL: TORTOISE!",
		"SLOW MOTION!"
	};

	private string[] m_FlipperSwapAnnounces = {"FLIPPERS SWAPPED!", "FLIPPER SWAP!", "RIGHT LEFT, LEFT, RIGHT?", "SWAP THEM FINGERS!", 
		"MIRROR MODE, BOI!", "FLIP THE FLIPPIN' FLIPPERS!", "FETTY SWAP (FLIPPERS)"
	};


	// Use this for initialization
	void Start () {
        m_AnnounceText.SetActive(false);
		m_TextActive = false;
		m_tMesh = m_AnnounceText.GetComponent<TextMesh> ();
    }
	
	// Update is called once per frame
	void Update () {
		if (m_TextActive) {
			m_tMesh.color -= new Color(0,0,0, m_FadeRate);
			m_AnnounceText.transform.localPosition += new Vector3 (0, m_RiseRate, 0);

			if (m_tMesh.color.a == 0) {
				m_AnnounceText.SetActive (false);
				m_TextActive = false;
			}
		}
	}

    public void AnnouncePower(PowerupManager.PowerUps power)
    {
		int colSel = (int) (Random.Range(0.0f, 0.999f) * m_PossibleTextCols.Length);
		m_tMesh.color = m_PossibleTextCols [colSel];
		m_AnnounceText.transform.localPosition = Vector3.zero;
		string announcement = "";
		int choice;
		switch (power) {
			case PowerupManager.PowerUps.BIGBALLS:
				choice = (int) (Random.Range (0.0f, 0.999f) * m_BigBallAnnounces.Length);
				announcement = m_BigBallAnnounces [choice];
				break;
		    case PowerupManager.PowerUps.SMALLBALLS:
			    choice = (int) (Random.Range (0.0f, 0.999f) * m_SmallBallAnnounces.Length);
			    announcement = m_SmallBallAnnounces [choice];
			    break;
            case PowerupManager.PowerUps.CLUSTERBALLS:
                choice = (int)(Random.Range(0.0f, 0.999f) * m_ClusterBallAnnounces.Length);
                announcement = m_ClusterBallAnnounces[choice];
                break;
            case PowerupManager.PowerUps.FLIPPERSWAP:
                choice = (int)(Random.Range(0.0f, 0.999f) * m_FlipperSwapAnnounces.Length);
                announcement = m_FlipperSwapAnnounces[choice];
                break;
            case PowerupManager.PowerUps.SLOMO:
                choice = (int)(Random.Range(0.0f, 0.999f) * m_SloMoAnnounces.Length);
                announcement = m_SloMoAnnounces[choice];
                break;
            case PowerupManager.PowerUps.BOUNCYBALLS:
                choice = (int)(Random.Range(0.0f, 0.999f) * m_BouncyBallAnnounces.Length);
                announcement = m_BouncyBallAnnounces[choice];
                break;
            case PowerupManager.PowerUps.UNBOUNCYBALLS:
                choice = (int)(Random.Range(0.0f, 0.999f) * m_UnbouncyBallsAnnounces.Length);
                announcement = m_UnbouncyBallsAnnounces[choice];
                break;

        }
        m_tMesh.text = announcement;



        m_AnnounceText.SetActive (true);
		m_TextActive = true;
    }
}

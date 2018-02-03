using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperControl : MonoBehaviour {

    public HingeJoint hinge;

    public float restPos = 0f;
    public float pressedPos = 60f;
    public float hitStrength = 10000f;
    public float flipperDamper = 150f;

    public string inputName;

    private AudioSource m_FlipperUpClip;
    private AudioSource m_FlipperDownClip;

    private bool m_AxisPressed;

    // Use this for initialization
    void Start () {
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;

        AudioSource[] clips = GetComponents<AudioSource>();
        m_FlipperUpClip = clips[0];
        m_FlipperDownClip = clips[1];

        m_AxisPressed = false;

    }
	
	// Update is called once per frame
	void Update () {
        JointSpring spring = new JointSpring();
        spring.spring = hitStrength;
        spring.damper = flipperDamper;

        if(Input.GetAxis(inputName) == 1)
        {
            spring.targetPosition = pressedPos;
            if(!m_AxisPressed)
            {
                m_AxisPressed = true;
                m_FlipperUpClip.Play();
            }
        }
        else
        {
            spring.targetPosition = restPos;
            if(m_AxisPressed)
            {
                m_AxisPressed = false;
                m_FlipperDownClip.Play();
            }
        }

        hinge.spring = spring;
    }

}

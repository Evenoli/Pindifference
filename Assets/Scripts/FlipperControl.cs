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

    // Use this for initialization
    void Start () {
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;
	}
	
	// Update is called once per frame
	void Update () {
        JointSpring spring = new JointSpring();
        spring.spring = hitStrength;
        spring.damper = flipperDamper;

        if(Input.GetAxis(inputName) == 1)
        {
            spring.targetPosition = pressedPos;
        }
        else
        {
            spring.targetPosition = restPos;
        }

        hinge.spring = spring;
    }

}

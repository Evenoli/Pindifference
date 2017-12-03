using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherControl : MonoBehaviour {

    public string inputName;
    public int m_springPower;
    public int m_springDamper;
    public SpringJoint m_springJoint;
    public float m_MaxPullback;
    public float m_pullbackSpeed;

    private float m_startZ;

    // Use this for initialization
    void Start () {
        m_startZ = transform.localPosition.z;
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis(inputName) == 1)
        {
            m_springJoint.spring = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - m_pullbackSpeed);
        }
        else
        {
            m_springJoint.spring = m_springPower;
        }

    }
}

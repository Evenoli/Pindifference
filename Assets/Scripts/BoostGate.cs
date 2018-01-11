using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostGate : MonoBehaviour {

    public float m_BoostSpeed;


    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.tag == "Ball")
        {
            Vector3 vel = obj.GetComponent<Rigidbody>().velocity;
            if(vel.y < m_BoostSpeed)
                obj.GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(0, m_BoostSpeed, 0));

        }
    }
}

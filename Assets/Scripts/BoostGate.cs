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
            obj.GetComponent<Rigidbody>().velocity += transform.TransformDirection(new Vector3(0, m_BoostSpeed, 0));

        }
    }
}

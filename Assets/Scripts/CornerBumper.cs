using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerBumper : MonoBehaviour {

    public float bumperPowerScale;
    public float maxMagnitude;
    public float minBumperPower;

    public GameObject m_BumperBody;

    public Color flashCol;
    private Color initialCol;
    public float flashResetRate;
    private bool isFlashing;
    private float flashLerpFactor;
    private Material mat;
    private Material bodyMat;


    // Use this for initialization
    void Start()
    {
        isFlashing = false;
        mat = gameObject.GetComponent<Renderer>().material;
        bodyMat = m_BumperBody.GetComponent<Renderer>().material;
        initialCol = mat.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlashing)
        {
            flashLerpFactor += flashResetRate;
            mat.color = Color.Lerp(flashCol, initialCol, flashLerpFactor);
            bodyMat.color = Color.Lerp(flashCol, initialCol, flashLerpFactor);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.collider.gameObject;
        if (obj.tag == "Ball")
        {
            Rigidbody ballRigidBdy = obj.GetComponent<Rigidbody>();
            ContactPoint cp = collision.contacts[0];
            Vector3 reverseVelocity = collision.relativeVelocity;
            Vector3 bounce = Vector3.Reflect(reverseVelocity, cp.normal);
            Vector3 bounceDirection = cp.normal * Mathf.Max(1, bounce.magnitude/2);

            ballRigidBdy.velocity = Vector3.ClampMagnitude((bounce * bumperPowerScale + bounceDirection * minBumperPower), maxMagnitude);
            Flash();
        }
    }

    public void Flash()
    {
        mat.color = flashCol;
        bodyMat.color = flashCol;

        isFlashing = true;
        flashLerpFactor = 0;
    }
}

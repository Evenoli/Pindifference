using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSound : MonoBehaviour {

    private AudioSource m_AudioSource;
    private Rigidbody m_RB;

    private float m_speed;
    public float m_nominalSpeed;

    public float m_MinPitch;
    public float m_MaxPitch;

    private bool m_BeingDestroyed;

	// Use this for initialization
	void Start () {
        m_RB = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
        m_BeingDestroyed = false;

    }

    private void FixedUpdate()
    {
        m_speed = m_RB.velocity.magnitude;
        m_AudioSource.pitch = Mathf.Clamp(m_speed / m_nominalSpeed, m_MinPitch, m_MaxPitch);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!m_AudioSource.isPlaying && m_speed >= 0.1f && collision.gameObject.tag == "Floor" && m_AudioSource.isActiveAndEnabled)
            m_AudioSource.Play();
        else if (m_AudioSource.isPlaying && m_speed < 0.1f && collision.gameObject.tag == "Floor")
            m_AudioSource.Pause();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_AudioSource.isPlaying && collision.gameObject.tag == "Floor")
            m_AudioSource.Pause();
    }

    void OnDestroy()
    {
        m_AudioSource.Pause();
    }
}

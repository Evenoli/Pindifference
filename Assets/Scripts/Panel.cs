using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour {

    private bool m_up;
    public float m_dropSpeed;
    private bool m_moving;
    public float m_dropHeight;

    private float m_minHeight;
    private float m_maxHeight;

    private BoxCollider m_collider;

    public Color flashCol;
    private Color initialCol;
    public float flashResetRate;
    private bool isFlashing;
    private float flashLerpFactor;
    private Material mat;

    private AudioSource m_PanelSound;

    private PointManager m_pointMan;

    // Use this for initialization
    void Start () {
        m_up = true;
        m_moving = false;
        isFlashing = false;

        m_maxHeight = transform.localPosition.y;
        m_minHeight = m_maxHeight - m_dropHeight;

        m_collider = GetComponent<BoxCollider>();
        mat = gameObject.GetComponent<Renderer>().material;
        initialCol = mat.color;
        m_pointMan = transform.root.gameObject.GetComponent<PointManager>();

        m_PanelSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlashing)
        {
            flashLerpFactor += flashResetRate;
            mat.color = Color.Lerp(flashCol, initialCol, flashLerpFactor);
        }

        if(m_moving)
        {
            float y= transform.localPosition.y;

            if (!m_up)
            {
                y -= m_dropSpeed;
                if (y <= m_minHeight)
                {
                    y = m_minHeight;
                    m_collider.enabled = false;
                    m_moving = false;
                }
            }
            else
            {
                y += m_dropSpeed;
                if (y >= m_maxHeight)
                {
                    y = m_maxHeight;
                    m_collider.enabled = true;
                    m_moving = false;
                }
            }

            transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.collider.gameObject;
        if (obj.tag == "Ball")
        {
            m_up = false;
            m_moving = true;
            Flash();
            m_PanelSound.Play();
            m_pointMan.AddToScore(PointManager.InteractionType.PANEL);
        }
    }

    public bool isDown()
    {
        return !m_up;
    }

    public void Reset()
    {
        if (!m_up)
        {
            m_up = true;
            m_moving = true;
        }
    }

    public void Flash()
    {
        mat.color = flashCol;
        isFlashing = true;
        flashLerpFactor = 0;
    }
}

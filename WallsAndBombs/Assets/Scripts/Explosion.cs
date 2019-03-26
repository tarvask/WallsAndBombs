using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private Transform m_Transform = null;

    [SerializeField]
    private Renderer m_Renderer = null;

    float m_Duration = 0.5f;
    float m_Size = 1f;

    public float Duration { set { m_Duration = value; } }
    public float Size { set { m_Size = value; } }

    float m_Timer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        m_Timer = 0f;
    }

    // decided to do it without Tweens
    void Update()
    {
        m_Timer += Time.deltaTime;
        float currentProgress = m_Timer / m_Duration;
        ChangeSize(currentProgress);
        ChangeOpacity(currentProgress);


        if (m_Timer > m_Duration)
        {
            Destroy(gameObject);
        }
    }

    void ChangeSize(float progress)
    {
        // some easing can be added
        float currentSize = m_Size * progress;
        m_Transform.localScale = Vector3.one * currentSize;
    }

    void ChangeOpacity(float progress)
    {
        // some easing can be added
        float currentOpacity = 1 - progress;
        Color currentColor = m_Renderer.material.color;
        currentColor.a = currentOpacity;
        m_Renderer.material.color = currentColor;
    }
}

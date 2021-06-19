using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D m_rb;

    public Rigidbody2D rb
    {
        get {
            return this.m_rb;
        }
        set {
            this.m_rb = value;
        }
    }

    public void Reset()
    {
        if (this.rb == null)
        {
            this.rb = GetComponent<Rigidbody2D>();
            if (this.rb == null)
                Debug.Log("Couldn't find Rigidbody2D on ball component.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PachinkoScore : MonoBehaviour
{
    private TextMeshProUGUI m_scoreText;
    private float m_score = 0f;


    public TextMeshProUGUI scoreText
    {
        get {
            return this.m_scoreText;
        }
    }

    public float score
    {
        get {
            return this.m_score;
        }
        set {
            this.m_score = value;
        }
    }

    public void Reset()
    {
        this.score = 0f;
    }

    public void initComponents()
    {
        this.m_scoreText = this.GetComponent<TextMeshProUGUI>();
        if (this.m_scoreText == null)
            throw new System.Exception("Failed to find TextMeshProUGUI object for score under PachinkoScore.");
    }

    // Start is called before the first frame update
    void Start()
    {
        this.initComponents();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.m_scoreText == null)
            this.initComponents();
        this.m_scoreText.text = $"Score: {this.score}";
    }
}

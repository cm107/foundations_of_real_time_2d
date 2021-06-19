using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PachinkoPlayArea : MonoBehaviour
{
    private BallSpawner m_ballSpawner;
    private PachinkoGoalHandler m_goalHandler;
    private PachinkoScore m_gameScore;

    public BallSpawner ballSpawner
    {
        get {
            return this.m_ballSpawner;
        }
    }

    public PachinkoGoalHandler goalHandler
    {
        get {
            return this.m_goalHandler;
        }
    }

    public PachinkoScore gameScore
    {
        get {
            return this.m_gameScore;
        }
    }

    public void Reset()
    {
        this.ballSpawner.Reset();
        this.goalHandler.Reset();
        this.gameScore.Reset();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.m_ballSpawner = GetComponentInChildren<BallSpawner>();
        if (this.m_ballSpawner == null)
            throw new System.Exception("Failed to find BallSpawner under PachinkoPlayArea");
        this.m_goalHandler = GetComponentInChildren<PachinkoGoalHandler>();
        if (this.m_goalHandler == null)
            throw new System.Exception("Failed to find PachinkoGoalHandler under PachinkoPlayArea");
        this.m_gameScore = GetComponentInChildren<PachinkoScore>();
        if (this.m_gameScore == null)
            throw new System.Exception("Failed to find PachinkoScore under PachinkoPlayArea.");
    }

    // Update is called once per frame
    void Update()
    {
        this.goalHandler.processGoalTriggers(
            ballSpawner: this.ballSpawner,
            gameScore: this.gameScore
        );
    }
}

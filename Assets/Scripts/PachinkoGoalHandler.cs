using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PachinkoGoalHandler : MonoBehaviour
{
    private PachinkoGoal[] m_goals;
    public float standardGoalProportion = 6f;
    public float goldGoalProportion = 0.5f;
    public float bombGoalProportion = 0.5f;

    public PachinkoGoal[] goals {
        get {
            return this.m_goals;
        }
        set {
            this.m_goals = value;
        }
    }

    private float standardGoalProbability
    {
        get {
            return this.standardGoalProportion / (this.standardGoalProportion + this.goldGoalProportion + this.bombGoalProportion);
        }
    }

    private float goldGoalProbability
    {
        get {
            return this.goldGoalProportion / (this.standardGoalProportion + this.goldGoalProportion + this.bombGoalProportion);
        }
    }

    private float bombGoalProbability
    {
        get {
            return this.bombGoalProportion / (this.standardGoalProportion + this.goldGoalProportion + this.bombGoalProportion);
        }
    }

    private PachinkoGoal.GOAL_TYPE randomGoal
    {
        get {
            float standardThresh = this.standardGoalProbability;
            float goldThresh = standardThresh + this.goldGoalProbability;
            float bombThresh = goldThresh + this.bombGoalProbability;
            float randomSeed = Random.Range(0f, 1f);
            if (randomSeed <= standardThresh)
                return PachinkoGoal.GOAL_TYPE.STANDARD;
            else if (randomSeed <= goldThresh)
                return PachinkoGoal.GOAL_TYPE.GOLD;
            else
                return PachinkoGoal.GOAL_TYPE.BOMB;
        }
    }

    public void randomizeGoals()
    {
        foreach (PachinkoGoal goal in this.goals)
        {
            goal.goalType = this.randomGoal;
        }
    }

    public void processGoalTriggers(BallSpawner ballSpawner, PachinkoScore gameScore)
    {
        List<Ball> ballsToRemove = new List<Ball>();
        bool isTriggered = false;
        foreach (Ball ball in ballSpawner.ballList)
        {
            foreach (PachinkoGoal goal in this.goals)
            {
                if (goal.isInsideTriggerRegion(ball.gameObject))
                {
                    gameScore.score += goal.triggerReward;
                    ballsToRemove.Add(ball);
                    isTriggered = true;
                    break;
                }
            }
        }
        foreach (Ball ball in ballsToRemove)
        {
            CollisionDetection.removeTouchedInstance(ball.gameObject);
            ballSpawner.removeBall(ball);
        }
        if (isTriggered)
            this.randomizeGoals();
    }

    public void Reset()
    {
        this.randomizeGoals();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.goals = GetComponentsInChildren<PachinkoGoal>();
        if (this.goals == null)
            throw new System.Exception("Failed to find any PachinkoGoal components under PachinkoGoalHandler.");
        if (this.goals.Length == 0)
            throw new System.Exception("No goals under PachinkoGoalHandler");
        this.randomizeGoals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PachinkoGoal : MonoBehaviour
{
    public enum GOAL_TYPE {
        STANDARD = 0,
        GOLD = 1,
        BOMB = 2
    }

    private GoalTriggerRegion triggerRegion;
    private SpriteRenderer spriteRenderer;
    private GOAL_TYPE m_goalType = GOAL_TYPE.STANDARD;
    private float m_triggerReward = 1.0f;

    public float triggerReward {
        get {
            return this.m_triggerReward;
        }
    }

    public GOAL_TYPE goalType {
        get {
            return this.m_goalType;
        }
        set {
            if (this.spriteRenderer == null)
                this.initComponents();
            this.m_goalType = value;
            if (this.m_goalType == GOAL_TYPE.STANDARD)
            {
                this.m_triggerReward = 1.0f;
                this.spriteRenderer.color = Color.white;
            }
            else if (this.m_goalType == GOAL_TYPE.GOLD)
            {
                this.m_triggerReward = 10f;
                this.spriteRenderer.color = Color.yellow;
            }
            else if (this.m_goalType == GOAL_TYPE.BOMB)
            {
                this.m_triggerReward = -5f;
                this.spriteRenderer.color = Color.red;
            }
            else
            {
                throw new System.Exception($"Invalid goalType: {value}");
            }
        }
    }

    static public GOAL_TYPE randomGoalType
    {
        get {
            return (GOAL_TYPE)Random.Range(0, System.Enum.GetValues(typeof(GOAL_TYPE)).Length);
        }
    }

    public bool isInsideTriggerRegion(GameObject obj)
    {
        return CollisionDetection.IsTouching(obj, this.triggerRegion.gameObject);
    }

    public void initComponents()
    {
        this.triggerRegion = GetComponentInChildren<GoalTriggerRegion>();
        if (this.triggerRegion == null)
            throw new System.Exception("Couldn't find GoalTriggerRegion under PachinkoGoal.");
        this.spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (this.spriteRenderer == null)
            throw new System.Exception("Couldn't find SpriteRenderer under PachinkoGoal.");
    }

    // Start is called before the first frame update
    void Start()
    {
        this.initComponents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

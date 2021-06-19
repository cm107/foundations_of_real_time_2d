using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public Ball ballPrefab;
    private Vector3? ballSpawnCursor = null;
    private List<Ball> m_ballList;
    private Ball lockedBall = null;

    public List<Ball> ballList
    {
        get {
            return this.m_ballList;
        }
        set {
            this.m_ballList = value;
        }
    }

    public void Reset()
    {
        this.clearBalls();
        this.ballSpawnCursor = this.transform.position;
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

    public Vector2? spawnCursor
    {
        get {
            Vector2? result = null;
            if (this.ballSpawnCursor != null)
                result = new Vector2(
                    x: this.ballSpawnCursor.Value.x,
                    y: this.ballSpawnCursor.Value.y
                );
            return result;
        }
        set {
            float clippedX = value.Value.x;
            if (clippedX > 9.6f) clippedX = 9.6f;
            if (clippedX < -9.6f) clippedX = -9.6f;
            float clippedY = value.Value.y;
            // if (clippedY != 0f) clippedY = 0f;
            this.ballSpawnCursor = new Vector3(
                x: clippedX,
                y: clippedY,
                z: this.ballSpawnCursor.Value.z
            );
            if (this.lockedBall != null)
                this.lockedBall.transform.position = this.ballSpawnCursor.Value;
        }
    }

    public void removeBall(Ball ball)
    {
        this.ballList.Remove(ball);
        Destroy(ball.gameObject);
    }

    public void clearBalls()
    {
        if (this.ballList != null)
        {
            for (int i = this.ballList.Count - 1; i >= 0; i--)
            {
                if (this.ballList[i] != null)
                {
                    this.removeBall(this.ballList[i]);
                }
            }
        }
        this.ballList = new List<Ball>();
    }

    private void lockBall(Ball ball)
    {
        if (this.lockedBall != null)
            throw new System.Exception("Can't lock more than one ball.");
        if (ball == null)
            throw new System.Exception("Can't lock a ball that is null.");
        if (ball.rb == null)
            ball.Reset();
        ball.rb.isKinematic = true;
        this.lockedBall = ball;
    }

    private void unlockBall()
    {
        if (this.lockedBall != null)
        {
            this.lockedBall.rb.isKinematic = false;
            this.lockedBall = null;
        }
    }

    public bool isBallLocked
    {
        get {
            return this.lockedBall != null;
        }
    }

    public void SpawnNewBall(bool locked=false)
    {
        Ball ball = Instantiate<Ball>(this.ballPrefab);
        if (ball == null)
            throw new System.Exception("Failed to instantiate new ball.");
        if (this.ballSpawnCursor == null)
            throw new System.Exception("Can't spawn ball until spawn cursor is defined.");
        ball.transform.position = this.ballSpawnCursor.Value;
        ball.transform.parent = this.transform;
        this.ballList.Add(ball);
        if (locked)
            this.lockBall(ball);
    }

    public void ReleaseBall()
    {
        this.unlockBall();
    }
}

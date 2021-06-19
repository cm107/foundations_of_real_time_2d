using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PachinkoPlayer : MonoBehaviour
{
    public PachinkoPlayArea area;
    public float cursorMovementSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (this.area == null)
            throw new System.Exception("Player needs to be assigned to a play area.");
    }

    private void moveBallCursor(float amount)
    {
        if (this.area.ballSpawner.spawnCursor != null)
        {
            float deltaX = amount * this.cursorMovementSpeed * Time.fixedDeltaTime;
            this.area.ballSpawner.spawnCursor = new Vector2(
                x: this.area.ballSpawner.spawnCursor.Value.x + deltaX,
                y: this.area.ballSpawner.spawnCursor.Value.y
            );
        }
    }

    private void dropBall()
    {
        this.area.ballSpawner.ReleaseBall();
        this.area.ballSpawner.SpawnNewBall(locked: true);
    }

    private void ballCursorListener()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            this.moveBallCursor(-1f);
        else if (Input.GetKey(KeyCode.RightArrow))
            this.moveBallCursor(1f);
        if (Input.GetKeyDown(KeyCode.Space))
            this.dropBall();
    }

    private void otherListener()
    {
        if (Input.GetKeyDown(KeyCode.R))
            this.area.goalHandler.randomizeGoals();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.area.ballSpawner.isBallLocked)
            this.area.ballSpawner.SpawnNewBall(locked: true);
        this.ballCursorListener();
        this.otherListener();
    }
}

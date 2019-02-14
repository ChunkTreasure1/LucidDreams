using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovementManager : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] [Range(0, 1)] private float SoundSensitivity = 0.5f;

    [SerializeField] private float MaxHearingDistanceRun = 15f;
    [SerializeField] private float MaxHearingDistanceWalk = 10f;
    [SerializeField] private float MaxHearingDistanceCrouch = 5f;

    [Header("Enemy")]
    [SerializeField] private NavMeshAgent[] Enemies;
    [SerializeField] private AIController[] Controllers;

    [Header("Guarding")]
    [SerializeField] private float WaitForIdle = 2f;
    [SerializeField] private GameObject[] GuardPoints;

    private float CurrTimerValue = 0f;
    private bool ShouldDoGuardRound = false;
    private bool TimerStarted = false;

    private int CurrentPointIndex;
    private GameObject CurrentPoint;
    private GameObject LastPoint;

    private AIController Controller;
    private NavMeshAgent Enemy;
    public bool IsUpstairs = true;

    void Awake()
    {
        CurrentPointIndex = 0;
        CurrentPoint = GuardPoints[CurrentPointIndex];
        Enemy = Enemies[0];
        Controller = Controllers[0];
    }

    void Start()
    {
        Enemy.updateRotation = false;
    }

    //Sets the destination which the AI should move to
    public void SetDestination(Vector3 pos, float soundLevel, MovingMode movingMode)
    {
        if (Vector3.Distance(Enemy.transform.position, pos) <= MaxHearingDistanceRun && movingMode == MovingMode.mM_Run)
        {
            Enemy.SetDestination(pos);
        }
        else if (Vector3.Distance(Enemy.transform.position, pos) <= MaxHearingDistanceWalk && movingMode == MovingMode.mM_Walk)
        {
            Enemy.SetDestination(pos);
        }
        else if (Vector3.Distance(Enemy.transform.position, pos) <= MaxHearingDistanceCrouch && movingMode == MovingMode.mM_Crouched)
        {
            Enemy.SetDestination(pos);
        }
        else if (movingMode == MovingMode.mM_Null && soundLevel > 5)
        {
            Enemy.SetDestination(pos);
        }
    }

    private void Update()
    {
        if (!IsUpstairs)
        {
            Controller = Controllers[0];
            Enemy = Enemies[0];
        }
        else
        {
            Controller = Controllers[1];
            Enemy = Enemies[1];
        }

        if (Enemy.isActiveAndEnabled)
        {
            if (ShouldDoGuardRound)
            {
                DoGuardRound();
            }
        }

        if (Enemy.isActiveAndEnabled)
        {
            //If the AI has reached it's end point
            if (Enemy.remainingDistance <= Enemy.stoppingDistance && !TimerStarted)
            {
                TimerStarted = true;
                //Start a timer to make the AI wait there
                StartCoroutine(Timer(WaitForIdle));
            }

            if (Enemy.remainingDistance > Enemy.stoppingDistance)
            {
                Controller.Move(Enemy.desiredVelocity, false, false);
            }
            else
            {
                Controller.Move(Vector3.zero, false, false);
            }
        }
    }

    //Timer for checking how long the enemy has been standing still
    public IEnumerator Timer(float time)
    {
        CurrTimerValue = time;
        while (CurrTimerValue > 0)
        {
            yield return new WaitForSeconds(1f);
            CurrTimerValue--;
        }
        ShouldDoGuardRound = true;
    }

    //Starts the guard round after a certain amount of time
    private void DoGuardRound()
    {
        //If the distance to the point is less or equal to the stopping distance
        if (Enemy.remainingDistance <= Enemy.stoppingDistance)
        {
            //Check if it should increase the current point index or set it to zero
            if (CurrentPointIndex < GuardPoints.Length - 1)
            {
                CurrentPointIndex++;
            }
            else
            {
                CurrentPointIndex = 0;
            }

            //Set the current point
            CurrentPoint = GuardPoints[CurrentPointIndex];
        }

        //If the last point isn't the current one
        if (LastPoint != CurrentPoint)
        {
            //Set the last point to the current and set the destination
            LastPoint = CurrentPoint;
            Enemy.SetDestination(GuardPoints[CurrentPointIndex].transform.position);
        }
    } 

    public GameObject GetEnemy()
    {
        return Enemy.gameObject;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Trigger_01 : MonoBehaviour
{
    [SerializeField] private PlayerController Controller;
    [SerializeField] private GameObject Can;
    [SerializeField] private AIMovementManager MovementManager;

    [SerializeField] private GameObject Enemy;
    [SerializeField] private Transform StoppingPoint;
    [SerializeField] private StoryManager Story;

    [SerializeField] private Animator Animator;

    private bool DestinationSet = false;
    private bool DestinationTwoSet = false;
    private float CurrTimerValue = 0;
    private bool CanOpen = false;

    private void Start()
    {
        Enemy.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Controller.GetComponent<Inventory>().SetMessageText("Press E to open", true);
            CanOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Controller.GetComponent<Inventory>().SetMessageText("", false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanOpen)
        {
            CanOpen = false;
            Controller.GetComponent<Inventory>().SetMessageText("", false);

            Animator.SetBool("IsOpen", true);

            Controller.CanMove = false;
            Controller.gameObject.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            Controller.gameObject.transform.position = new Vector3(12.64f, 5.463f, -10.432f);
            Can.GetComponent<Rigidbody>().AddForce(new Vector3(10, 0, 0));

            Enemy.SetActive(true);
            MovementManager.SetDestination(Can.transform.position, 0, MovingMode.mM_Null);
            DestinationSet = true;
        }

        if (DestinationSet)
        {
            if (Enemy.GetComponent<NavMeshAgent>().remainingDistance < Enemy.GetComponent<NavMeshAgent>().stoppingDistance)
            {
                StartCoroutine(Timer(3f));
                DestinationSet = false;
            }
        }
        else if (DestinationTwoSet)
        {
            if (Enemy.GetComponent<NavMeshAgent>().remainingDistance < Enemy.GetComponent<NavMeshAgent>().stoppingDistance) 
            {
                Enemy.SetActive(false);
                Controller.CanMove = true;
                Destroy(this);
            }
        }
    }

    public IEnumerator Timer(float time)
    {
        CurrTimerValue = time;
        while (CurrTimerValue > 0)
        {
            yield return new WaitForSeconds(1f);
            CurrTimerValue--;
        }

        MovementManager.SetDestination(StoppingPoint.position, 0, MovingMode.mM_Null);
        Story.SetText("Seems like that... thing reacts to sounds, I must be careful", true);
        DestinationTwoSet = true;
    }
}

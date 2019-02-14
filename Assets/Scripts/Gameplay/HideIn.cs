using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HideIn : MonoBehaviour
{
    public Transform Player;
    public Transform GoOutPosition;
    public Animator Animator;
    public Animator FadeAnimator;

    private bool Ishiding = false;
    private bool OpenAnimationDone = false;

    // Update is called once per frame
    void Update()
    {
        // To hide
        if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(transform.position, Player.position) <= 2)
        {
            Ishiding = !Ishiding;

            Animator.SetBool("EnterOrExit", true);
        }
        // Text that tells you to hide
        if(Vector3.Distance(transform.position, Player.position) <= 2 && !Ishiding)
        {
            Player.GetComponent<Inventory>().SetMessageText("Press E to hide", true);
        }
        else if(Ishiding)
        {
            Player.GetComponent<Inventory>().SetMessageText("", false);
        }

        if (Animator.GetBool("EnterOrExit") && OpenAnimationDone)
        {
            FadeAnimator.SetBool("ShouldFade", true);

            if (FadeAnimator.gameObject.GetComponent<InventoryUI>().FadeAnimationDone)
            {
                FadeAnimator.SetBool("ShouldFade", false);
                Animator.SetBool("EnterOrExit", false);

                if (Ishiding)
                {
                    Player.position = transform.position;
                }
                else
                {
                    Player.position = GoOutPosition.transform.position;
                }

                OpenAnimationDone = false;
                FadeAnimator.gameObject.GetComponent<InventoryUI>().FadeAnimationDone = false;
            }
        }
    }

    public void AnimationFinished()
    {
        OpenAnimationDone = true;
    }
}

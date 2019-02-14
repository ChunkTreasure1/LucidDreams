using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SoundSender : MonoBehaviour
{
    public AIMovementManager MovementManager;
    public bool OnlyPlayer;

    public void SendSound(float soundLevel, MovingMode movingMode)
    {
        //If the box isn't checked, send the sound
        if (!OnlyPlayer && MovementManager.GetEnemy().activeSelf)
        {
            MovementManager.SetDestination(this.transform.position, soundLevel, movingMode);
        }
    }
}
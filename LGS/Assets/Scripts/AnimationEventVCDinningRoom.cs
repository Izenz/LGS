using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AnimationEventVCDinningRoom : MonoBehaviour
{
    public Transform[] gargoyles;
    public Transform[] ghosts;
    public Transform player;
   
    public CinemachineVirtualCamera cmv;


    public void SetLookAtGhosts(int index)
    {
        cmv.m_LookAt = ghosts[index];
    }

    public void SetLookAtGargoyles(int index)
    {
        cmv.m_LookAt = gargoyles[index];
    }

    public void SetLookAtPlayer()
    {
        cmv.m_LookAt = player;
    }
}

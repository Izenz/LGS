using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]

public class Observer : MonoBehaviour
{
    public Transform player;

    bool isPlayerInRange;

    public GameEnding gameEnding;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            isPlayerInRange = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform == player)
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if(isPlayerInRange)
        {
            /*
             * El vector up se le suma porque John Lemon tiene la ra�z en los pies... hay que sumarle un metro para que est� por el torso.
             */
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);

            RaycastHit raycastHit;
            //Si chocamos contra algo
            //out hace que se pase por referencia.
            if(Physics.Raycast(ray, out raycastHit))
            {
                if(raycastHit.collider.transform == player)
                {

                }
            }
        }
    }
}

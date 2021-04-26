using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClose : MonoBehaviour
{
    public Transform player;
    public GameObject door;
    private Animator animator_pivotDoor;
    private Animator animator_player;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("JohnLemon").transform;
        animator_pivotDoor = door.GetComponent<Animator>();
        animator_player = player.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.transform == player)
        {
            animator_pivotDoor.SetTrigger("cerrarPuerta");
        }
    }
}

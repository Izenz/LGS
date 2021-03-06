using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorOpen : MonoBehaviour
{
    public Transform player;
    public GameObject door, item, panelTexto;
    private Animator animator_pivotDoor;
    private Animator animator_player;
    public bool abierta;

    bool isPlayerInRange;
    private void Start()
    {
        player = GameObject.Find("JohnLemon").transform;
        animator_pivotDoor = door.GetComponent<Animator>();
        animator_player = player.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            isPlayerInRange = true;
            Debug.Log(TextModels.infoDoor);
            if (!abierta)
            {
                panelTexto.GetComponent<TMP_Text>().text = TextModels.infoDoor;
                panelTexto.SetActive(true);
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            isPlayerInRange = false;
            panelTexto.SetActive(false);
        }
    }

    private void Update()
    {
        if(isPlayerInRange)
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.F)) && !animator_pivotDoor.GetBool("abrirPuerta"))
            {
                if (player.GetComponent<PlayerController>().CompruebaInventory(item.GetComponent<Item>()))
                {
                    abierta = true;
                    animator_pivotDoor.SetTrigger("abrirPuerta");
                    animator_player.SetTrigger("OpenDoor");
                }
                else 
                {
                    Debug.Log("No tienes la llave, pppppesao!!");
                    panelTexto.GetComponent<TMP_Text>().text = TextModels.noCanOpenDoorKey;
                }
                
            }
        }
    }
}

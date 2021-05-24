using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickUpItem : MonoBehaviour
{
    private Transform player;
    public GameObject panelTexto;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("JohnLemon").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            panelTexto.GetComponent<TMP_Text>().text = TextModels.pickUpObject;
            panelTexto.SetActive(true);
            other.gameObject.GetComponent<PlayerController>().CanPickUp = true;
            other.gameObject.GetComponent<PlayerController>().ItemObjectToPickUp = this.transform.GetComponent<Item>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            panelTexto.SetActive(false);
            other.gameObject.GetComponent<PlayerController>().CanPickUp = false;
            other.gameObject.GetComponent<PlayerController>().ItemObjectToPickUp = null;
        }
    }

    
}

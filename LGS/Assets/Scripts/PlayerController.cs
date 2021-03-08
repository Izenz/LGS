using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement;

    private Animator _animator;
    private Rigidbody _rigidbody;

    private AudioSource _audioSource;

    [SerializeField]
    private float turnSpeed;

    private bool stop;

    private CinemachineFramingTransposer transposer;
    private IEnumerator coroutine;

    public CinemachineVirtualCamera vc;

    private Quaternion rotation = Quaternion.identity;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        transposer = vc.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Update()
    {
        /*
         * Hay que añadir flechas arriba y abajo.
         */
        if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) && stop)
        {
            StopCoroutine(coroutine);
            coroutine = CameraY(transposer.m_ScreenY, 0.5f, 0.3f);
            StartCoroutine(coroutine);
            stop = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement.Set(horizontal, 0, vertical);

        /*
         * La magia mágica
         *Gira demasiado rápido pero... eso se podrá regular.
         */

       /* Vector3 camForward = vc.transform.forward;
        Vector3 camRight = vc.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        movement = vertical * camForward + horizontal * camRight;*/

        if(vertical == 1 )
        {
            stop = true;
            coroutine = CameraY(transposer.m_ScreenY, 0.75f, 0.3f);
            StartCoroutine(coroutine);
        }
        else if(vertical == -1)
        {
            stop = true;
            coroutine = CameraY(transposer.m_ScreenY, 0.25f, 0.3f);
            StartCoroutine(coroutine);
        }

       
        // Debug.Log(vc.GetCinemachineComponent<CinemachineFramingTransposer>);
        //  movement.Set(transform.forward.x, 0, transform.forward.z);

        /*
         * Tenemos que normalizar el vector ya que puede ser que las distancias no sean iguales
         * Al pulsar arriba obtenemos el vector (1, 0, 0)
         * Si pulsamos arriba + derecha obtenemos el vector (1, 0, 1)
         * En el primer caso la distancia es de 1 y en el segundo de raíz de 2. Son diferentes
         */
        movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        _animator.SetBool("IsWalking", isWalking);

        if(isWalking)
        {
            if(!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource.Stop();
        }
        //Debug.Log(transform.forward);

        /*Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, 
            turnSpeed * Time.fixedDeltaTime, 0f);*/
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement,
            turnSpeed * Time.fixedDeltaTime, 0f);

        rotation = Quaternion.LookRotation(desiredForward);
    }

    private void OnAnimatorMove()
    {
        // movement me da la dirección
        //Este es el truco que se usa cuando la propia animación ya lleva movimiento.
        //s = so + V*t       so + delta S
        _rigidbody.MovePosition(_rigidbody.position + movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(rotation);
    }

    IEnumerator CameraY(float from, float to, float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            transposer.m_ScreenY = Mathf.Lerp(from, to, (Time.time - startTime) / duration);
            //Debug.Log(transposer.m_ScreenY);
            yield return 0;

        }
        yield return 0;
    }
}


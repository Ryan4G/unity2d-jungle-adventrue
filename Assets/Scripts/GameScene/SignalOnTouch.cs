using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class SignalOnTouch : MonoBehaviour
{
    public UnityEvent onTouch;

    public bool playAudioOnTouch = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SendSignal(collision.gameObject);
    }

    private void SendSignal(GameObject gameObject)
    {
        if (gameObject.CompareTag("Player"))
        {
            if (playAudioOnTouch)
            {
                var audio = GetComponent<AudioSource>();

                if (audio && audio.gameObject.activeInHierarchy)
                {
                    audio.Play();
                }
            }

            onTouch.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SendSignal(collision.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

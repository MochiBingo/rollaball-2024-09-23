using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class triggerDoor : MonoBehaviour
{
    public GameObject AnimatedObject;

    private Animator _animator;
    public GameObject player;
    

    private void Start()
    {
        _animator = AnimatedObject.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider player)
    {
            _animator.enabled = true;
    }
}

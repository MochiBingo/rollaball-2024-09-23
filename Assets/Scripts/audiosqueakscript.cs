using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class audioscript : MonoBehaviour
{
    public AudioSource source;

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            source.Play(0);
        }
    }
}

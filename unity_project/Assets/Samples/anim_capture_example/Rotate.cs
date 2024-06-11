using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    float speed = 150f;

    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}

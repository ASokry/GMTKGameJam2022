using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateThis : MonoBehaviour
{

    float degrees = 0;
    [SerializeField] float speed = 1;

    void Update()
    {
        transform.Rotate(Vector3.forward * (speed * Time.deltaTime));
    }
}

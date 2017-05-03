using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtForce : MonoBehaviour
{

    [SerializeField] Transform target;

    private void Update()
    {
        transform.LookAt( target );
    }
}

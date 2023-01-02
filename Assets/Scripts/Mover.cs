using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{

    [SerializeField] Transform target;
    NavMeshAgent nav;

    float currentSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MoveToCursor();
        }

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = nav.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        currentSpeed = localVelocity.z;

        GetComponent<Animator>().SetFloat("forwardSpeed", (currentSpeed));
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool hasHit = Physics.Raycast(ray, out hit);

        if(hasHit)
        {
            nav.destination = hit.point;
        }
    }
}

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CameraFollowScript : MonoBehaviour
{
    public GameObject Player;
    public float RotationSpeed = 1;
    public float smoothSpeed = 0.125f;
    [Header("Default Offset")]
    public Vector3 tempOffSet;
    public delegate void cameralasttargetset(Transform target);
    public static cameralasttargetset CameraLastTargetSet;

    void FixedUpdate()
    {
        if (Player == null) { return; }
        Vector3 desiredPosition = Player.transform.position + tempOffSet;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        Quaternion lookRotation = Quaternion.LookRotation(Player.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, RotationSpeed * Time.deltaTime);
    }

    void LastTargetSet(Transform Target)
    {
        Player = Target.gameObject;
    }

    private void OnEnable()
    {
        CameraLastTargetSet += LastTargetSet;
    }

    private void OnDisable()
    {
        CameraLastTargetSet -= LastTargetSet;
    }
}

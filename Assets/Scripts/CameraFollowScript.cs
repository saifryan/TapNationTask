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

    void FixedUpdate()
    {
        if (Player == null) { return; }
        Vector3 desiredPosition = Player.transform.position + tempOffSet;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        Quaternion lookRotation = Quaternion.LookRotation(Player.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, RotationSpeed * Time.deltaTime);
    }
}

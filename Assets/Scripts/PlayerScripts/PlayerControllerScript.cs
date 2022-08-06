using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using JetSystems;

public class PlayerControllerScript : MonoBehaviour
{
    [Header(" Managers ")]
    [SerializeField] private PlayerFormationScript squadFormation;

    [Header(" Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveCoefficient;
    [SerializeField] private float platformWidth;

    

    private Vector3 clickedPosition;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        
    }

    // Update is called once per frame
    
    void Update()
    {
        if (UIManagerScript.IsGame())
            MoveForward();

        if (!UIManagerScript.IsGame()) return;

        UpdateProgressBar();

    }

    public void StoreClickedPosition()
    {
        clickedPosition = transform.position;
        
    }

    public void GetSlideValue(Vector2 slideInput)
    {
        slideInput.x *= moveCoefficient;
        float targetX = clickedPosition.x + slideInput.x;

        float maxX = platformWidth / 2 - squadFormation.GetSquadRadius();

        targetX = Mathf.Clamp(targetX, -maxX, maxX);

        transform.position = transform.position.With(x: Mathf.Lerp(transform.position.x, targetX, 0.3f));
    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }

    private void UpdateProgressBar()
    {
        float initialDistanceToFinish = LevelDataScript.Instance.EndPoint.position.z - initialPosition.z;
        float currentDistanceToFinish = LevelDataScript.Instance.EndPoint.position.z - transform.position.z;


        float distanceLeftToFinish = initialDistanceToFinish - currentDistanceToFinish;

        float progress = distanceLeftToFinish / initialDistanceToFinish;
        UIManagerScript.updateProgressBarDelegate?.Invoke(progress);
    }


}

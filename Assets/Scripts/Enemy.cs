using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header(" Detection ")]
    [SerializeField] private LayerMask runnersLayer;
    [SerializeField] private float detectionDistance;
    private PlayerRunnerScript targetRunner;

    [Header(" Movement ")]
    [SerializeField] private float moveSpeed;

    [Header(" Animation ")]
    [SerializeField] private Animator animator;
    bool DieCheck = false;
    public GameObject Body;
    public GameObject ParticleObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DieCheck) { return; }
        if (targetRunner == null)
            FindTargetRunner();
        else
            AttackRunner();
    }

    private void FindTargetRunner()
    {
        Collider[] detectedRunners = Physics.OverlapSphere(transform.position, detectionDistance, runnersLayer);

        if (detectedRunners.Length <= 0) return;

        for (int i = 0; i < detectedRunners.Length; i++)
        {
            PlayerRunnerScript currentRunner = detectedRunners[i].GetComponent<PlayerRunnerScript>();
            if (currentRunner.IsTargeted()) continue;

            currentRunner.SetAsTarget();
            targetRunner = currentRunner;
            StartMoving();
            break;
        }


    }

    private void AttackRunner()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetRunner.transform.position, moveSpeed * Time.deltaTime);
        transform.forward = (targetRunner.transform.position - transform.position).normalized;

        if(Vector3.Distance(transform.position, targetRunner.transform.position) < 1f)
        {
            targetRunner.Explode();
            Explode();
        }
    }

    private void StartMoving()
    {
        animator.SetInteger("State", 1);
        transform.parent = null;
    }


    private void Explode()
    {
        DieCheck = true;
        Body.SetActive(false);
        ParticleObject.SetActive(true);
        StartCoroutine(ExplodeTimeDelay());
    }

    IEnumerator ExplodeTimeDelay()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}

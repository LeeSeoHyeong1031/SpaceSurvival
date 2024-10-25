using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public LayerMask whatIsTarget; // 추적 대상 레이어
    public RaycastHit2D[] targets;

    private float diff = 100;

    public void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, 20f, Vector2.zero, 0f, whatIsTarget);
    }

    public Transform GetNearestTarget()
    {
        Transform result = null;
        diff = 100;
        foreach (RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
}

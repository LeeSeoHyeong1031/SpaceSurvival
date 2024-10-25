using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public Gun gunData;
    public Transform target;

    private void Start()
    {
        StartCoroutine(FireCoroutine());
    }
    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(gunData.fireInterval);
            Fire();
        }
    }
    private void Fire()
    {
        target = GameManager.Instance.player.targetEnemy;
        if (target == null) return;

        Projectile proj = PoolManager.Instance.projectilePool.Pop();
        proj.transform.position = transform.position;
        proj.transform.up = target.position - transform.position;
        proj.damage = gunData.damage;
        proj.moveSpeed = gunData.moveSpeed;
    }
}

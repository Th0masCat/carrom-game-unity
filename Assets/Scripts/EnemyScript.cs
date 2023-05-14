using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    GameObject striker;

    


    // void EnemyTurn()
    // {
    //     float targetX = Random.Range(-2f, 2f);
    //     float targetY = Random.Range(2f, 4f);
    //     Vector3 targetPos = new Vector3(targetX, targetY, 0f);

    //     Vector3 direction = targetPos - transform.position;
    //     direction.z = 0f;
    //     direction.Normalize();

    //     float distance = Vector3.Distance(targetPos, transform.position);
    //     float force = distance * strikerSpeed;

    //     rb.AddForce(direction * force * Time.deltaTime);
    // }



}

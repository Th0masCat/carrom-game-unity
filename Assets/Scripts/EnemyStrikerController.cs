using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrikerController : MonoBehaviour
{
    [SerializeField]
    GameObject pocket;

    Rigidbody2D rb;
    
    bool again = false;

    private void OnEnable()
    {
        transform.position = new Vector3(0, 3.54f, 0);
        StartCoroutine(EnemyTurn()); 
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update() {
        enabled = !StrikerController.playerTurn;
        Debug.Log(StrikerController.playerTurn);
    }


    IEnumerator EnemyTurn()
    {
        // Determine which coin to hit based on game logic.
        // For example, the AI could target the closest coin to the pocket, or a high-value coin.
        yield return new WaitForSeconds(2f);

        GameObject[] coins = GameObject.FindGameObjectsWithTag("Black");
        GameObject closestCoin = null;
        float closestDistance = Mathf.Infinity;
        if (coins.Length == 0)
        {
            Debug.Log("No coins left");
            yield break;
        }

        foreach (GameObject coin in coins)
        {
            float distance = Vector3.Distance(coin.transform.position, pocket.transform.position);
            if (distance < closestDistance)
            {
                closestCoin = coin;
                closestDistance = distance;
            }
        }

        // Calculate the direction and speed of the striker based on the position of the target coin and the enemy's striker.

        Vector3 targetDirection = closestCoin.transform.position - transform.position;
        targetDirection.z = 0f;
        float targetSpeed = CalculateStrikerSpeed(targetDirection.magnitude);

        // Apply the calculated force to the striker and end the enemy's turn.

        
        rb.AddForce(targetDirection.normalized * targetSpeed, ForceMode2D.Impulse);

        yield return new WaitUntil(() => rb.velocity.magnitude < 0.1f);

    }

    float CalculateStrikerSpeed(float distance)
    {
        float maxDistance = 2.0f; // Maximum distance the striker can travel
        float minSpeed = 10f; // Minimum striker speed
        float maxSpeed = 40f; // Maximum striker speed

        float speed = Mathf.Lerp(minSpeed, maxSpeed, distance / maxDistance);
        return speed;
    }


}

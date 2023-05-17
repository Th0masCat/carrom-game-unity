using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrikerController : MonoBehaviour
{
    Rigidbody2D rb;
    bool isMoving;

    private void Start()
    {
        isMoving = false;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.velocity.magnitude < 0.1f && !isMoving)
        {
            isMoving = true;
            StartCoroutine(EnemyTurn());
        }
    }

    private void OnEnable()
    {
        CollisionSoundManager.shouldBeStatic = true;
        
        const int maxAttempts = 10;
        int attempts = 0;
        bool isObstructed;

        do
        {
            isObstructed = false;

            float x = Random.Range(-3.24f, 3.24f);
            transform.position = new Vector3(x, 3.45f, 0f);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);

            foreach (Collider2D collider in colliders)
            {
                Debug.Log(collider.tag);
                if (collider.CompareTag("Black") || collider.CompareTag("White") || collider.CompareTag("Striker"))
                {
                    isObstructed = true;
                    break;
                }
            }
            Debug.Log("Attempt " + attempts);

            attempts++;
        }
        while (isObstructed && attempts < maxAttempts);

        if (isObstructed)
        {
            Debug.Log("Failed to find a valid position for the enemy striker.");
            transform.position = new Vector3(0f, 3.45f, 0f);
            isObstructed = false;
        }
    }

    IEnumerator EnemyTurn()
    {
        // Determine which coin to hit based on game logic.
        // For example, the AI could target the closest coin to the pocket, or a high-value coin.
        yield return new WaitForSeconds(2f);
        CollisionSoundManager.shouldBeStatic = false;

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
            float distance = Vector3.Distance(coin.transform.position, GetClosestPocket(coin.transform.position));
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

        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => rb.velocity.magnitude < 0.1f);
        isMoving = false;
        StrikerController.playerTurn = true;
        gameObject.SetActive(false);
    }

    Vector3 GetClosestPocket(Vector3 position)
    {
        Vector3 closestPocket = Vector3.zero;
        float closestDistance = Mathf.Infinity;
        GameObject[] pockets = GameObject.FindGameObjectsWithTag("Pocket");

        foreach (GameObject pocket in pockets)
        {
            float distance = Vector3.Distance(position, pocket.transform.position);
            if (distance < closestDistance)
            {
                closestPocket = pocket.transform.position;
                closestDistance = distance;
            }
        }

        return closestPocket;
    }

    float CalculateStrikerSpeed(float distance)
    {
        float maxDistance = 4.0f; // Maximum distance the striker can travel
        float minSpeed = 10f; // Minimum striker speed
        float maxSpeed = 30f; // Maximum striker speed

        float speed = Mathf.Lerp(minSpeed, maxSpeed, distance / maxDistance);
        return speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Board"))
        {
            GetComponent<AudioSource>().Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StrikerController : MonoBehaviour
{
    [SerializeField]
    Slider StrikerSlider;

    [SerializeField]
    float strikerSpeed = 100f;

    [SerializeField]
    Transform strikerForceField;

    [SerializeField]
    GameObject pocket;
    bool isCharging;
    Vector2 direction;
    Rigidbody2D rb;
    public static bool playerTurn = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            isCharging = false;
            return;
        }

        isCharging = true;
        strikerForceField.gameObject.SetActive(true);
    }

    private void OnMouseUp()
    {
        if (!isCharging)
        {
            return;
        }

        strikerForceField.gameObject.SetActive(false);
        isCharging = false;
        Vector3 direction = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction.z = 0f;
        rb.AddForce(direction * strikerSpeed);
        playerTurn = false;
    }

    private void OnMouseDrag()
    {
        if (!isCharging)
        {
            return;
        }

        Vector3 direction = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction.z = 0f;

        strikerForceField.LookAt(transform.position + direction);

        float scaleValue = Vector3.Distance(transform.position, transform.position + direction);
        strikerForceField.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
    }

    private void Update()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            StrikerSlider.gameObject.SetActive(false);
        }


        if (!isCharging)
        {
            StartCoroutine(StrikerXPos());
        }

    }

    IEnumerator EnemyTurn()
    {
        // Determine which coin to hit based on game logic.
        // For example, the AI could target the closest coin to the pocket, or a high-value coin.

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

        rb.AddForce(targetDirection.normalized * targetSpeed);
        
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => rb.velocity.magnitude < 0.1f);
        rb.velocity = Vector2.zero;
        rb.rotation = 0f;

        playerTurn = true;
    }

    float CalculateStrikerSpeed(float distance)
    {
        float maxDistance = 2.0f; // Maximum distance the striker can travel
        float minSpeed = 50f; // Minimum striker speed
        float maxSpeed = 100f; // Maximum striker speed

        float speed = Mathf.Lerp(minSpeed, maxSpeed, distance / maxDistance);
        return speed;
    }


    public IEnumerator StrikerXPos()
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => rb.velocity.magnitude < 0.1f);
        rb.velocity = Vector2.zero;
        rb.rotation = 0f;
        
        if (playerTurn)
        {
            StrikerSlider.gameObject.SetActive(true);
            transform.position = new Vector3(StrikerSlider.value, -4.57f, 0);
        }
        else
        {
            transform.position = new Vector3(StrikerSlider.value, 3.45f, 0);
            StartCoroutine(EnemyTurn());
        }
    }

}

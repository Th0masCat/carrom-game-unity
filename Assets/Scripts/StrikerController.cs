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
    float maxScale = 1f;

    [SerializeField]
    Transform strikerForceField;

    bool isMoving;
    bool isCharging;
    float maxForceMagnitude = 30f;
    Rigidbody2D rb;
    
    public static bool playerTurn;

    private void Start()
    {
        playerTurn = true;
        isMoving = false;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        transform.position = new Vector3(StrikerSlider.value, -4.57f, 0);
    } 

    private void Update()
    {
        if (rb.velocity.magnitude < 0.1f && !isMoving)
        {
            isMoving = true;
            StartCoroutine(OnMouseUp());
        }
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

    private IEnumerator OnMouseUp()
    {
        isMoving = true;
        yield return new WaitForSeconds(0.1f);
        if (!isCharging)
        {
            yield break;
        }

        strikerForceField.gameObject.SetActive(false);
        isCharging = false;
        Vector3 direction = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction.z = 0f;

        
        float forceMagnitude = direction.magnitude * strikerSpeed;
        forceMagnitude = Mathf.Clamp(forceMagnitude, 0f, maxForceMagnitude);

        rb.AddForce(direction.normalized * forceMagnitude, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => rb.velocity.magnitude < 0.1f);
        isMoving = false;
        playerTurn = false;
        gameObject.SetActive(false);
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

        if (scaleValue > maxScale)
        {
            scaleValue = maxScale;
        }

        strikerForceField.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
    }
    
    public void SetSliderX()
    {
        if (rb.velocity.magnitude < 0.1f)
        {
            transform.position = new Vector3(StrikerSlider.value, -4.57f, 0);
        }
    }
}

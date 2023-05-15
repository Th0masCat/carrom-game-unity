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

        if (!isCharging && rb.velocity.magnitude < 0.1f && playerTurn == true)
        {
            StrikerXPos();
        }

    }


    public void StrikerXPos()
    {
        Debug.Log("StrikerXPos");
        playerTurn = false;
        StrikerSlider.gameObject.SetActive(true);
        transform.position = new Vector3(StrikerSlider.value, -4.57f, 0);
    }

}

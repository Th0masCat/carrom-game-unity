using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StrikerController : MonoBehaviour
{
    [SerializeField]
    Slider StrikerSlider;

    // Start is called before the first frame update
    void Start()
    {
        StrikerSlider.onValueChanged.AddListener(StrikerXPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StrikerXPos(float value){
        transform.position = new Vector3(value, -4.57f, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector3 movementVector = new Vector3(10f,10f,10f);
    [SerializeField] float period = 2f;

    [Range(0, 1)][SerializeField]float movementFactor; //0 for not move,1 for fully move
    Vector3 startingPos;
    void Start()
    {
        startingPos = transform.position;//获得该建筑物的位置
    }
    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / period;// grows continuely from 0

        const float tau = Mathf.PI * 2; //about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau);//goes from -1 to 1;
        movementFactor = rawSinWave / 2f+0.5f;
        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPos + offset;
    }
}

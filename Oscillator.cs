using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector3 movementVector;
    [Range(0, 1)][SerializeField]float movementFactor; //0 for not move,1 for fully move
    Vector3 startingPos;
    void Start()
    {
        startingPos = transform.position;//获得该建筑物的位置
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPos + offset;
    }
}

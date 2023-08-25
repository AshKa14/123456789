using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    
    [SerializeField]
    private float fastSpeed = 10.0f;

    // Sobrescribir el m�todo Update para un movimiento m�s r�pido
    void Update()
    {
        // L�gica de movimiento m�s r�pido para los FastEnemy
        transform.Translate(Vector3.forward * fastSpeed * Time.deltaTime);
    }
}



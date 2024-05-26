using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public Transform target;                // El objetivo hacia el cual el objeto se mover�
    public float speed = 5f;                // Velocidad de movimiento normal
    public float rotationSpeed = 5f;        // Velocidad de rotaci�n normal
    public float chargeSpeed = 20f;         // Velocidad de embestida
    public float chargeDuration = 3f;       // Duraci�n de la embestida
    public float detectionRadius = 10f;     // Radio del SphereCollider para detecci�n

    private Rigidbody rb;                   // Referencia al componente Rigidbody
    private Vector3 lastKnownPosition;      // �ltima posici�n conocida del objetivo
    private bool isCharging = false;        // Indica si el objeto est� embistiendo
    private float chargeStartTime;          // Tiempo en el que se inici� la embestida

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        // A�adir y configurar SphereCollider
        SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = detectionRadius;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isCharging && other.transform == target)
        {
            // No hacer nada si el objetivo vuelve a entrar en el rango durante la embestida
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!isCharging && other.transform == target)
        {
            lastKnownPosition = target.position;
            Invoke("StartCharge", 3f); // Iniciar embestida despu�s de 3 segundos
        }
    }

    void StartCharge()
    {
        if (!isCharging)
        {
            isCharging = true;
            chargeStartTime = Time.time;
        }
    }

    void Update()
    {
        if (isCharging)
        {
            float chargeElapsed = Time.time - chargeStartTime;
            if (chargeElapsed >= chargeDuration)
            {
                isCharging = false; // Terminar la embestida despu�s de la duraci�n definida
            }
            else
            {
                ChargeTowards(lastKnownPosition);
            }
        }
        else
        {
            if (target != null)
            {
                MoveTowards(target.position);
            }
        }
    }

    void MoveTowards(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        direction.Normalize();

        Vector3 newPosition = Vector3.MoveTowards(rb.position, position, speed * Time.deltaTime);
        rb.MovePosition(newPosition);

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion newRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        rb.MoveRotation(newRotation);
    }

    void ChargeTowards(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        direction.Normalize();

        Vector3 newPosition = Vector3.MoveTowards(rb.position, position, chargeSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(targetRotation); // Rotaci�n instant�nea durante la embestida
    }
}


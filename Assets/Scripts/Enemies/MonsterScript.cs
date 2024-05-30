using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public PlayerController _Pcontroller;

    public Transform target;           
    public float speed = 3f;           
    public float rotationSpeed = 5f;   
    public float chargeSpeed = 20f;    
    public float chargeDuration = 5f;  
    public float detectionRadius = 10f;
    public Animator _Manimator;

    private Rigidbody rb;              
    private Vector3 lastKnownPosition; 
    private bool isCharging = false;   
    private float chargeStartTime;

    public bool _PlayerInMeleeRange;

    private float _chargeCC = 7;
    private float _CchargeCC;

    public bool _PlayerHitMe;

    //Stats
    private int _MMaxhealth;
    public int _McurrHealth;
    public int _MmeleeDmg = 25;
    public int _MChargeDmg = 40;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        _Pcontroller = FindObjectOfType<PlayerController>().gameObject.GetComponent<PlayerController>();

        // Añadir y configurar SphereCollider
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
            lastKnownPosition = new Vector3(target.position.x, target.position.y + 2, target.position.z);
            Invoke("StartCharge", 3f); // Iniciar embestida después de 3 segundos
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
        target = FindObjectOfType<PlayerController>().gameObject.transform;
        HealthManager();
        ChargeAttackAndMovement();
        MeleeAttack();
        
    }

    private void HealthManager() {
        if (_PlayerHitMe)
        {
            _McurrHealth =- _Pcontroller._Patkdmg;
            _PlayerHitMe = false;
        }
        if (_McurrHealth <= 0)
        {
            Destroy(this);
        }
    }

    private void ChargeAttackAndMovement() {
        if (isCharging)
        {
            float chargeElapsed = Time.time - chargeStartTime;
            if (chargeElapsed >= chargeDuration)
            {
                isCharging = false; // Terminar la embestida después de la duración definida
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
                MoveTowards(new Vector3(target.position.x, target.position.y + 2, target.position.z));
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
        rb.MoveRotation(targetRotation); // Rotación instantánea durante la embestida
    }

    private void MeleeAttack() {
        
    }

    private IEnumerator ChargeCooldown()
    {
        Debug.Log("AttkCC");
        yield return new WaitForSeconds(_chargeCC);
        Debug.LogError("AttkReady");
        _CchargeCC = 0f;
    }
}


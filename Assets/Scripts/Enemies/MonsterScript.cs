using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public PlayerController _Pcontroller;
    public GameManager _gameManager;

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

    public float _chargeCC;
    private float _CchargeCC;

    public bool _PlayerHitMe;

    public bool _Imdead;

    public bool _Attacking;

    public AudioSource _dedRoar;
    public AudioSource _hitRoar;

    //Stats
    public float _MMaxhealth;
    public float _McurrHealth;
    public int _MmeleeDmg = 25;
    public int _MChargeDmg = 40;

    void Start()
    {
        _McurrHealth = _MMaxhealth;
        _Imdead = false;
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
        if (!isCharging && other.transform == target && !_Attacking)
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
        if (!_Imdead)
        {
            target = FindObjectOfType<PlayerController>().gameObject.transform;
            HealthManager();
            ChargeAttackAndMovement();
            MeleeAttack();
        }
        else
        {
            _gameManager._TotalEnemiesDefeated++;
            _gameManager._roomCleared = true;
        }
        
    }

    private void HealthManager() {
        if (_PlayerHitMe)
        {
            _McurrHealth = _McurrHealth - _Pcontroller._Patkdmg;
            Debug.Log(_McurrHealth);
            _PlayerHitMe = false;
        }
        if (_McurrHealth <= 0)
        {
            _dedRoar.Play();
            transform.position = _gameManager._EnemyStandByPos.transform.position;
            _Imdead = true;
        }
    }

    private void ChargeAttackAndMovement() {
        if (isCharging)
        {
            float chargeElapsed = Time.time - chargeStartTime;
            if (chargeElapsed >= chargeDuration)
            {
                isCharging = false; // Terminar la embestida después de la duración definida
                StartCoroutine(ChargeCooldown());
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
        if (_CchargeCC == 0f)
        {
            _CchargeCC = 1;
            Vector3 direction = position - transform.position;
            direction.Normalize();

            Vector3 newPosition = Vector3.MoveTowards(rb.position, position, chargeSpeed * Time.deltaTime);
            rb.MovePosition(newPosition);

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(targetRotation); // Rotación instantánea durante la embestida
        }
    }

    private void MeleeAttack() {
        if (_PlayerInMeleeRange)
        {
            _Attacking = true;
            Attack();
        }
        else
        {
            _Attacking = false;
        }
    }

    private IEnumerator Attack() {
        yield return new WaitForSeconds(2);
        if (_PlayerInMeleeRange)
        {
            _Pcontroller._PCurrHealth = _Pcontroller._PCurrHealth - _MmeleeDmg;
        }
    }

    private IEnumerator ChargeCooldown()
    {
        Debug.Log("AttkCC");
        yield return new WaitForSeconds(_chargeCC);
        Debug.LogError("AttkReady");
        _CchargeCC = 0f;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovent : MonoBehaviour
{    
    public GameObject bullet;
    public Transform firePoint;
    public GameObject gun;
    public float moveSpeed;
    public float attackDelay;
    public float passedTime;

    private Vector2 movement; // Dirección del movimiento
    private Rigidbody2D rb; // Referencia al Rigidbody2D
    private Animator animator; // Referencia al componente Animator
    private bool facingRight = true; // Estado de la dirección del personaje
    private Vector2 lookDirection;
    private float lookAngle;
    private MovementEntriies movementEntries;
    private GameObject virtualCursor;

    private void Awake()
    {
        movementEntries = new MovementEntriies();
    }

    private void OnEnable()
    {
        movementEntries.Enable();
    }

    private void OnDisable()
    {
        movementEntries.Disable();
    }

    void Start()
    {
        // Obtiene el componente Rigidbody2D y Animator del objeto al que está asignado el script
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        virtualCursor = GameObject.Find("VirtualCursor");
    }

    void Update()
    {
        // Captura la entrada del jugador en los ejes horizontal (A/D) y vertical (W/S)
        movement.x = movementEntries.Movement.Horizontal.ReadValue<float>();
        movement.y = movementEntries.Movement.Vertical.ReadValue<float>();

        // Actualiza el parámetro IsMoving en el Animator
        animator.SetBool("IsMoving", movement.magnitude > 0);

        // Llamamos a la función para verificar la dirección del jugador
        FlipSprite();

        RotateFirePoint();

        ShotBullet();

        if (passedTime < attackDelay)
        {
            passedTime += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        // Aplica el movimiento al Rigidbody2D para moverse
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void FlipSprite()
    {
        // Si el personaje está moviéndose hacia la izquierda y está mirando a la derecha, se voltea
        if (movement.x < 0 && facingRight)
        {
            Flip();
        }
        // Si el personaje está moviéndose hacia la derecha y está mirando a la izquierda, se voltea
        else if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        // Cambia el estado de la dirección
        facingRight = !facingRight;

        // Invertir la rotación en el eje Y para voltear el sprite
        Vector3 rotation = transform.localEulerAngles;
        rotation.y += 180f;
        transform.localEulerAngles = rotation;
    }

    void RotateFirePoint()
    {
        if (Application.isMobilePlatform)
        {
            lookDirection = Camera.main.ScreenToWorldPoint(virtualCursor.transform.position) - new Vector3(transform.position.x, transform.position.y);
        }
        else
        {
            lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(transform.position.x, transform.position.y);
        }

        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);
        Vector3 gunRotation = gun.transform.localEulerAngles;
        gunRotation.z = lookAngle * -1;
        if (!facingRight) gunRotation.y = 180f; else gunRotation.y = 0;
        gun.transform.localEulerAngles = gunRotation;
    }

    void ShotBullet()
    {
        // Detectar clic del mouse
        if (Input.GetMouseButtonDown(0))
        {
            if (passedTime >= attackDelay)
            {
                SpawnBullet();
            }
        }
        // Detectar entrada del joystick
        else if (movementEntries.Cursor.MoveCursor.ReadValue<Vector2>() != Vector2.zero)
        {
            if (passedTime >= attackDelay)
            {
                SpawnBullet();
            }
        }
    }

    // Método separado para la lógica de instanciar y disparar la bala
    void SpawnBullet()
    {
        GameObject bulletClone = Instantiate(bullet);
        PlayerBulletMovement bulletScript = bulletClone.GetComponent<PlayerBulletMovement>();

        if (bulletScript != null)
        {
            bulletScript.ShotBullet(firePoint, lookAngle);
        }

        passedTime = 0;
    }

}

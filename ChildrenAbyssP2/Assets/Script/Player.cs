using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 7.0f;
    public float runningMultiplier = 1.5f;
    public float jumpForce = 2.0f;
    public float gravity = 9.81f;
    public float mouseSensitivity = 800.0f;

    public float velocidadMovimiento = 8.0f;
    public float velocidadRotacion = 200.0f;

    private CharacterController controller;
    private Animator anim;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private float xRotation = 0f;
    public bool Demonio;

    public GameObject jugadorSkin;
    public GameObject demonioSkin;

    public Avatar jugadorAvatar;
    public Avatar demonioAvatar;

    public BarraVida barraVida; // Referencia al script BarraVida

    private float x, y;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;

        // Configurar estado inicial del jugador
        jugadorSkin.SetActive(true);
        demonioSkin.SetActive(false);
        anim.avatar = jugadorAvatar;
        anim.SetLayerWeight(0, 1); // Activar capa de animación del jugador
        anim.SetLayerWeight(1, 0); // Desactivar capa del demonio

        StartCoroutine(AutoKillAfterOneMinute()); // Iniciar cuenta regresiva
    }

    void Update()
    {
        if (barraVida.Salud <= 0)
        {
            return; // Si el jugador está muerto, no hace nada más
        }

        // Movimiento del jugador
        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Capturar entrada de movimiento
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0, y);
        move = transform.TransformDirection(move);

        // Correr
        if (Input.GetKey(KeyCode.LeftShift))
        {
            move *= speed * runningMultiplier;
        }
        else
        {
            move *= speed;
        }
        controller.Move(move * Time.deltaTime);

        // Saltar
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            anim.Play(Demonio ? "JumpDevil" : "Jump");
            playerVelocity.y += Mathf.Sqrt(jumpForce * 2.0f * gravity);
        }

        // Otras acciones
        if (Input.GetKey("b"))
        {
            anim.SetBool("Other", false);
            anim.Play(Demonio ? "DanceDevil" : "Dance");
        }
        if (x != 0 || y != 0)
        {
            anim.SetBool("Other", true);
        }

        // Transformar entre jugador y demonio
        if (Input.GetKeyDown(KeyCode.Z) && Demonio)
        {
            ToggleSkin();
        }

        playerVelocity.y -= gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Rotación de la cámara
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Animación y rotación
        transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
        transform.Translate(0, 0, y * Time.deltaTime * velocidadMovimiento);

        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el Player ha entrado en el Detector del TeleportRing
        TeleportRing teleportRing = other.GetComponentInParent<TeleportRing>();
        if (teleportRing != null && !teleportRing.teleportInProcess)
        {
            teleportRing.ColliderEntersDetector(GetComponent<Collider>());
        }
    }
    IEnumerator AutoKillAfterOneMinute()
    {
        yield return new WaitForSeconds(60.0f);

        if (barraVida.Salud > 0) // Si sigue vivo después de 1 minuto
        {
            barraVida.RecibirDanio(barraVida.Salud); // Reducir la salud a 0
        }
    }
    public void ToggleSkin()
    {
        if (barraVida.Salud <= 0) return; // No permitir transformación si está muerto

        if (!Demonio)
        {
            // Cambiar a demonio
            Demonio = true;
            jugadorSkin.SetActive(false);
            demonioSkin.SetActive(true);
            anim.avatar = demonioAvatar;
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 1);

            barraVida.SaludMaxima = 200;
            barraVida.Salud = Mathf.Min(barraVida.Salud * 2, barraVida.SaludMaxima);
            speed = 14.0f;
            jumpForce = 7.0f;
        }
        else
        {
            // Cambiar a jugador normal
            Demonio = false;
            jugadorSkin.SetActive(true);
            demonioSkin.SetActive(false);
            anim.avatar = jugadorAvatar;
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);

            barraVida.SaludMaxima = 100;
            barraVida.Salud = Mathf.Min(barraVida.Salud, barraVida.SaludMaxima);
            speed = 7.0f;
            jumpForce = 2.0f;
        }

        barraVida.ActualizarInterfaz();
    }
    public void ForceKill()
    {
        if (barraVida.Salud > 0)
        {
            barraVida.RecibirDanio(barraVida.Salud); // Reducir salud a 0
        }
    }
}
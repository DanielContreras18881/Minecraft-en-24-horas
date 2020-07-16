using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] pauseMenu pausa;
    inventorySys inv;
    Rigidbody rb;
    WorldGeneration world;
    [SerializeField] Transform arm;
    Animator ArmAnim;
    bool isGrounded;
    float h;
    float v;
    float mouseH;
    float mouseV;
    float xMouseAxis = 0f;
    public float speed;
    public float MaxSpeed;
    public float mouseSensitivity;
    public float JumpPower;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Start()
    {
        pausa = FindObjectOfType<pauseMenu>();
        inv = FindObjectOfType<inventorySys>();
        world = FindObjectOfType<WorldGeneration>();
        rb = GetComponent<Rigidbody>();
        ArmAnim = arm.GetComponentInChildren<Animator>();
    }
    void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jumping();
        }
    }
    private void Update()
    {
        ArmAnim.SetFloat("Speed", isGrounded ? Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z) : 0f);
        mouseH = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseV = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        RotateCamera();
        if ((Mathf.Abs(h) > 0) || (Mathf.Abs(v) > 0)) 
        {
            Walking();
        }
        if (Input.GetMouseButton(0)) // mouse izquierdo
        {
            StartCoroutine(DestroyBlock());
        }
        if (Input.GetMouseButton(1)) // mouse derecho
        {
            StartCoroutine(PlaceBlock());
        }
        if (Input.GetKeyDown(KeyCode.Escape)) // No sé por qué chota no funcionaaaa, la puta madreeeeeee
        {
            pausa.gameObject.SetActive(true);
        }
    }
    private void RotateCamera()
    {
        transform.Rotate(Vector3.up * mouseH); // rota jugador de izq a derecha
        xMouseAxis -= mouseV;
        xMouseAxis = Mathf.Clamp(xMouseAxis, -90, 90);
        arm.localRotation = Quaternion.Euler(xMouseAxis, 0f, 0f); // rota jugador arriba a abajo
    }
    private void Walking()
    {
        rb.AddForce(transform.TransformDirection(Vector3.right) * speed * h);
        rb.AddForce(transform.TransformDirection(Vector3.forward) * speed * v);
        if (rb.velocity.x > MaxSpeed)
        {
            rb.velocity = new Vector3(MaxSpeed, rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.z > MaxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, MaxSpeed);
        }
    }
    private void Jumping()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }
    }
    private IEnumerator DestroyBlock()
    {
        ArmAnim.SetBool("Clicked", true);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, 6f);
        if (hit.transform != null)
        {
            GameObject block = hit.transform.gameObject;
            Destroy(block);
        }
        yield return new WaitForSeconds(0.33f);
        ArmAnim.SetBool("Clicked", false);
    }
    private IEnumerator PlaceBlock()
    {
        ArmAnim.SetBool("Clicked", true);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // hacia donde mira el jugador xd
        Vector3 SpawnPos = Vector3.zero;
        Physics.Raycast(ray, out hit, 6f);
        if (hit.transform != null)
        {
            float xDiff = hit.point.x - hit.transform.position.x;
            float yDiff = hit.point.y - hit.transform.position.y;
            float zDiff = hit.point.z - hit.transform.position.z;
            Debug.Log(xDiff + " " + yDiff + " " + zDiff);
            if (Mathf.Abs(yDiff) == 1f)
            {
                SpawnPos = hit.transform.position + (Vector3.up * yDiff); // lo spawnea arriba
            }
            else if (Mathf.Abs(xDiff) == 1f)
            {
                SpawnPos = hit.transform.position + (Vector3.right * xDiff); // lo spawnea en eje x
            }
            else if (Mathf.Abs(zDiff) == 1f)
            {
                SpawnPos = hit.transform.position + (Vector3.forward * zDiff); // lo spawnea en eje z
            }
            GameObject newBlock = world.SpawnBlock(inv.selectedBlock, SpawnPos, hit.transform.parent);
        }
        yield return new WaitForSeconds(0.33f);
        ArmAnim.SetBool("Clicked", false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("isGroundable"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("isGroundable"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("isGroundable"))
        {
            isGrounded = false;
        }
    }
}

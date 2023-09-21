using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    // �������� ����������� ����
    public float speed = 5f;

    // �������� ������� ��� ������ ����
    public float verticalSpeed = 5f;

    // ������ �� ��������� Rigidbody
    private Rigidbody rb;

    // ����� ����, ������������, ��� �������� ������ ��� ����
    public LayerMask groundMask;

    public GameObject secret;

    // ������, � �������� �������� ����� secret ������ ����������� ���������
    public float radius = 1f;

    // �������, ���������� ��� ������ �����
    private void Start()
    {
        // �������� ��������� Rigidbody
        rb = GetComponent<Rigidbody>();
        // �������� ������ �� ����� secret �� ��� �����
        secret = GameObject.Find("Secret");
    }

    // �������, ���������� ������ ����
    private void Update()
    {
        // �������� ���� ������������ �� �������������� � ������������ ���
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float distance = Vector3.Distance(transform.position, secret.transform.position);


        // ������� ������ ����������� �������� ���� ������������ ��� ��������
        Vector3 direction = transform.right * horizontal + transform.forward * vertical;

        // ������� ���� ���� �� ����������� � �������� ���������
        rb.AddForce(direction * speed);

        // ���������, ������ �� ������� �������
        if (Input.GetKey(KeyCode.Space))
        {
            // ������������� ������������ �������� ���� ������ �������� �������
            rb.velocity = new Vector3(rb.velocity.x, verticalSpeed, rb.velocity.z);
        }

        // ���������, ������ �� ������� Shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // ������������� ������������ �������� ���� ������ �������� ������
            rb.velocity = new Vector3(rb.velocity.x, -verticalSpeed, rb.velocity.z);
        }

        // ���������, ������ �� ������������ ������� ������� � Shift
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift))
        {
            // �������� ������������ �������� ����
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }

        // ���������, �� ������ �� �� ���� �� ������ ������� � Shift
        if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift))
        {
            // �������� ������������ �������� ����
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
        if (horizontal == 0 && vertical == 0)
        {
            // �������� ���� ���� �� �������������� ���������
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        if (distance < radius)
        {
            // ������ ����� secret ���������, �������� ��� ��������
            secret.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            // ������ ����� secret �������, ������� ��� ��������
            secret.GetComponent<Renderer>().enabled = true;
        }
    }

    // �������, �����������, ��������� �� ��� �� �����
    private bool IsGrounded()
    {
        // ������� ��� ���� �� ������ ����� ���� �� ��������� ����������
        return Physics.Raycast(rb.position - Vector3.up * rb.GetComponent<Collider>().bounds.extents.y, Vector3.down, 0.1f, groundMask);
    }
}

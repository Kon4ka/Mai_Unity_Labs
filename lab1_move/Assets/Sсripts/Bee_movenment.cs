using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    // Скорость перемещения куба
    public float speed = 5f;

    // Скорость подъема или спуска куба
    public float verticalSpeed = 5f;

    // Ссылка на компонент Rigidbody
    private Rigidbody rb;

    // Маска слоя, определяющая, что является землей для куба
    public LayerMask groundMask;

    public GameObject secret;

    // Радиус, в пределах которого кубик secret должен становиться невидимым
    public float radius = 1f;

    // Функция, вызываемая при старте сцены
    private void Start()
    {
        // Получаем компонент Rigidbody
        rb = GetComponent<Rigidbody>();
        // Получаем ссылку на кубик secret по его имени
        secret = GameObject.Find("Secret");
    }

    // Функция, вызываемая каждый кадр
    private void Update()
    {
        // Получаем ввод пользователя по горизонтальной и вертикальной оси
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float distance = Vector3.Distance(transform.position, secret.transform.position);


        // Создаем вектор направления движения куба относительно его поворота
        Vector3 direction = transform.right * horizontal + transform.forward * vertical;

        // Придаем кубу силу по направлению с заданной скоростью
        rb.AddForce(direction * speed);

        // Проверяем, нажата ли клавиша пробела
        if (Input.GetKey(KeyCode.Space))
        {
            // Устанавливаем вертикальную скорость куба равной скорости подъема
            rb.velocity = new Vector3(rb.velocity.x, verticalSpeed, rb.velocity.z);
        }

        // Проверяем, нажата ли клавиша Shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Устанавливаем вертикальную скорость куба равной скорости спуска
            rb.velocity = new Vector3(rb.velocity.x, -verticalSpeed, rb.velocity.z);
        }

        // Проверяем, нажата ли одновременно клавиша пробела и Shift
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift))
        {
            // Обнуляем вертикальную скорость куба
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }

        // Проверяем, не нажата ли ни одна из клавиш пробела и Shift
        if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift))
        {
            // Обнуляем вертикальную скорость куба
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
        if (horizontal == 0 && vertical == 0)
        {
            // Обнуляем силу куба по горизонтальной плоскости
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        if (distance < radius)
        {
            // Делаем кубик secret невидимым, отключая его рендерер
            secret.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            // Делаем кубик secret видимым, включая его рендерер
            secret.GetComponent<Renderer>().enabled = true;
        }
    }

    // Функция, проверяющая, находится ли куб на земле
    private bool IsGrounded()
    {
        // Пускаем луч вниз от нижней точки куба на небольшое расстояние
        return Physics.Raycast(rb.position - Vector3.up * rb.GetComponent<Collider>().bounds.extents.y, Vector3.down, 0.1f, groundMask);
    }
}

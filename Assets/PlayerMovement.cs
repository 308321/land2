using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [Header("移动设置")]
    public float walkSpeed = 4.5f;  // 走路速度
    public float runSpeed = 100f;     // 跑步速度
    public float rotateSpeed = 120f;
    public float gravity = 9.8f;
    public float jumpHeight = 0.5f;

    [Header("视角设置")]
    public float horizontalMouseSensitivity = 200f;  // 水平鼠标灵敏度
    public float verticalMouseSensitivity = 200f;    // 垂直鼠标灵敏度
    public float minVerticalAngle = -30f;
    public float maxVerticalAngle = 105f;
    [Tooltip("视角锁定键")]
    public KeyCode viewLockKey = KeyCode.Q;

    private Vector3 velocity;
    private bool isGrounded;
    private float verticalRotation = 0f;
    private Transform cameraTransform;
    private bool isViewLocked = false;
    private float lockedHorizontalAngle;
    private float lockedVerticalAngle;

    [Header("头部摇动设置")]
    public float bobFrequency = 0.001f;        // 摇动频率
    public float bobAmplitude = 0.05f;     // 摇动幅度
    private float bobTimer = 0f;
    private Vector3 initialCameraLocalPos;

    [Header("准星设置")]
    public Image crosshairImage; // 十字准星UI组件

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        // 记录初始相机本地位置（用于还原）
        initialCameraLocalPos = cameraTransform.localPosition;

        // 初始时锁定鼠标并隐藏
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 确保准星初始化时位于屏幕中央
        if (crosshairImage != null)
        {
            crosshairImage.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        }
    }

    void Update()
    {
        HandleViewLockToggle();
        HandleGrounding();
        HandleMovement();

        if (!isViewLocked)
        {
            HandleRotation();
            HandleMouseLook();
        }

        HandleJump();
        HandleHeadBobbing();
    }

    private void HandleViewLockToggle()
    {
        if (Input.GetKeyDown(viewLockKey))
        {
            isViewLocked = !isViewLocked;

            if (isViewLocked)
            {
                // 锁定视角
                lockedHorizontalAngle = transform.eulerAngles.y;
                lockedVerticalAngle = verticalRotation;

                Cursor.lockState = CursorLockMode.None;  // 锁定视角时不隐藏鼠标
                Cursor.visible = true;                   // 锁定视角时鼠标可见

                Debug.Log("视角锁定");
            }
            else
            {
                // 解锁视角
                Cursor.lockState = CursorLockMode.Locked; // 解锁时锁定鼠标
                Cursor.visible = false;                   // 解锁时鼠标不可见

                Debug.Log("视角解锁");
            }
        }
    }

    private void HandleGrounding()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");  // A/D 键用于左右移动
        float vertical = Input.GetAxis("Vertical"); // W/S 键用于前后移动

        // 判断是否按下 Shift 键来进行跑步
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float currentMoveSpeed = isRunning ? runSpeed : walkSpeed;  // 根据是否跑步选择速度

        // 如果按下前进（W）或后退（S）键，则根据跑步或走路状态来调整速度
        if (vertical != 0)
        {
            controller.Move(transform.forward * vertical * currentMoveSpeed * Time.deltaTime);
        }

        // 如果按下左右移动（A/D）键，则左右移动
        if (horizontal != 0)
        {
            controller.Move(transform.right * horizontal * currentMoveSpeed * Time.deltaTime);
        }
    }

    private void HandleRotation()
    {
        // 玩家左右旋转
        if (!isViewLocked)  // 如果没有锁定视角，才允许旋转
        {
            float horizontal = Input.GetAxis("Mouse X") * horizontalMouseSensitivity * Time.deltaTime;
            transform.Rotate(0, horizontal, 0);
        }
    }

    private void HandleMouseLook()
    {
        // 获取鼠标的垂直输入并调整旋转
        if (!isViewLocked)
        {
            float mouseY = Input.GetAxis("Mouse Y") * verticalMouseSensitivity * Time.deltaTime;
            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);
            cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }
    }

    private void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleHeadBobbing()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        bool isMoving = (Mathf.Abs(inputX) > 0.1f || Mathf.Abs(inputZ) > 0.1f) && isGrounded;

        if (isMoving)
        {
            bobTimer += Time.deltaTime * bobFrequency;
            float bobOffset = Mathf.Sin(bobTimer) * bobAmplitude;
            Vector3 newPos = initialCameraLocalPos;
            newPos.y += bobOffset;
            cameraTransform.localPosition = newPos;

            Debug.Log("摇动中: " + newPos.y);
        }
        else
        {
            bobTimer = 0f;
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, initialCameraLocalPos, Time.deltaTime * 5f);

            Debug.Log("回到初始位置: " + cameraTransform.localPosition.y);
        }
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}





/*
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [Header("移动设置")]
    public float walkSpeed = 4.5f;  // 走路速度
    public float runSpeed = 100f;     // 跑步速度
    public float rotateSpeed = 120f;
    public float gravity = 9.8f;
    public float jumpHeight = 0.5f;

    [Header("视角设置")]
    public float horizontalMouseSensitivity = 200f;  // 水平鼠标灵敏度
    public float verticalMouseSensitivity = 200f;    // 垂直鼠标灵敏度
    public float minVerticalAngle = -30f;
    public float maxVerticalAngle = 105f;
    [Tooltip("视角锁定键")]
    public KeyCode viewLockKey = KeyCode.Q;

    private Vector3 velocity;
    private bool isGrounded;
    private float verticalRotation = 0f;
    private Transform cameraTransform;
    private bool isViewLocked = false;
    private float lockedHorizontalAngle;
    private float lockedVerticalAngle;

    [Header("头部摇动设置")]
    public float bobFrequency = 0.001f;        // 摇动频率
    public float bobAmplitude = 0.05f;     // 摇动幅度
    private float bobTimer = 0f;
    private Vector3 initialCameraLocalPos;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        // 记录初始相机本地位置（用于还原）
        initialCameraLocalPos = cameraTransform.localPosition;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        HandleViewLockToggle();
        HandleGrounding();
        HandleMovement();

        if (!isViewLocked)
        {
            HandleRotation();
            HandleMouseLook();
        }

        HandleJump();
        HandleHeadBobbing();
    }

    private void HandleViewLockToggle()
    {
        if (Input.GetKeyDown(viewLockKey))
        {
            isViewLocked = !isViewLocked;

            if (isViewLocked)
            {
                lockedHorizontalAngle = transform.eulerAngles.y;
                lockedVerticalAngle = verticalRotation;
                Cursor.lockState = CursorLockMode.None;  // 锁定视角时不隐藏鼠标
                Cursor.visible = true;                   // 锁定视角时鼠标可见
                Debug.Log("视角锁定");
            }
            else
            {
                // 解锁视角时，鼠标不隐藏，回到屏幕中央
                CenterMouse();
                Debug.Log("视角解锁");
            }
        }
    }

    private void HandleGrounding()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void HandleMovement()
    {
        float vertical = Input.GetAxis("Vertical");

        // 判断是否按下 Shift 键来进行跑步
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float currentMoveSpeed = isRunning ? runSpeed : walkSpeed;  // 根据是否跑步选择速度

        // 如果按下前进（W）或后退（S）键，则根据跑步或走路状态来调整速度
        if (vertical > 0)  // 向前移动
        {
            controller.Move(transform.forward * vertical * currentMoveSpeed * Time.deltaTime);
        }
        else if (vertical < 0)  // 向后移动
        {
            controller.Move(-transform.forward * -vertical * currentMoveSpeed * Time.deltaTime);
        }
    }

    private void HandleRotation()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(0, horizontal * rotateSpeed * Time.deltaTime, 0);
    }

    private void HandleMouseLook()
    {
        // 获取鼠标的垂直输入并调整旋转
        float mouseY = Input.GetAxis("Mouse Y") * verticalMouseSensitivity * Time.deltaTime;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        // 获取鼠标的水平输入并旋转
        float mouseX = Input.GetAxis("Mouse X") * horizontalMouseSensitivity * Time.deltaTime;
        transform.Rotate(0, mouseX, 0);
    }

    private void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleHeadBobbing()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        bool isMoving = (Mathf.Abs(inputX) > 0.1f || Mathf.Abs(inputZ) > 0.1f) && isGrounded;

        if (isMoving)
        {
            bobTimer += Time.deltaTime * bobFrequency;
            float bobOffset = Mathf.Sin(bobTimer) * bobAmplitude;
            Vector3 newPos = initialCameraLocalPos;
            newPos.y += bobOffset;
            cameraTransform.localPosition = newPos;

            Debug.Log("摇动中: " + newPos.y);
        }
        else
        {
            bobTimer = 0f;
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, initialCameraLocalPos, Time.deltaTime * 5f);

            Debug.Log("回到初始位置: " + cameraTransform.localPosition.y);
        }
    }

    // 使鼠标始终位于屏幕中央
    private void CenterMouse()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Cursor.SetCursor(null, screenCenter, CursorMode.Auto);
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
*/
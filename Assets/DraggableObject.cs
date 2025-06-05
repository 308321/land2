using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private bool isDragging = false;
    private Rigidbody rb;
    private Vector3 offset;
    private float fixedZ; // 固定的Z轴位置

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fixedZ = transform.position.z; // 记录初始Z轴位置
    }

    void Update()
    {
        HandleMouseInput();

        if (isDragging)
        {
            DragMove();
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isDragging)
            {
                // 尝试拾取物体
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject == this.gameObject)
                    {
                        Debug.Log("拾取物体: " + gameObject.name); // 拾取日志
                        StartDragging(hit.point);
                    }
                }
            }
            else
            {
                Debug.Log("放下物体: " + gameObject.name); // 放下日志
                StopDragging();
            }
        }
    }

    void StartDragging(Vector3 hitPoint)
    {
        isDragging = true;
        rb.isKinematic = true;

        // 计算鼠标点击位置与物体中心的偏移量
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, fixedZ - Camera.main.transform.position.z));
        offset = transform.position - mouseWorldPos;
    }

    void StopDragging()
    {
        isDragging = false;
        rb.isKinematic = false;
    }

    void DragMove()
    {
        // 将鼠标位置转换为世界坐标，保持Z轴不变
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, fixedZ - Camera.main.transform.position.z));

        // 应用偏移量并保持Z轴不变
        Vector3 targetPosition = mouseWorldPos + offset;
        targetPosition.z = fixedZ;

        transform.position = targetPosition;
    }
}
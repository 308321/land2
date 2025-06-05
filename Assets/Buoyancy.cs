using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    public Transform waterSurface;
    public float buoyancyMultiplier = 15f; // 浮力系数（总体大小）
    public float densityThreshold = 1f;
    public float dragInWater = 3f; // 水中线性阻力
    public float angularDragInWater = 3f; // 水中角阻力
    public float waterDamping = 0.5f; // 浮力方向的速度阻尼，控制上浮平滑程度

    private Rigidbody rb;
    private float waterLevel;
    private bool isInWater;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateWaterLevel();
    }

    void UpdateWaterLevel()
    {
        if (waterSurface != null)
        {
            waterLevel = waterSurface.position.y + waterSurface.localScale.y * 2.4f;
        }
    }

    void FixedUpdate()
    {
        if (transform.position.y < waterLevel)
        {
            if (!isInWater)
            {
                // 第一次进入水中时增加阻力
                rb.drag = dragInWater;
                rb.angularDrag = angularDragInWater;
                isInWater = true;
            }

            // 计算下沉了多少
            float depth = waterLevel - transform.position.y;

            // 估算密度
            float density = rb.mass / (transform.localScale.x * transform.localScale.y * transform.localScale.z);

            if (density < densityThreshold)
            {
                // 浮力大小和下沉深度成比例
                Vector3 buoyancy = Vector3.up * depth * buoyancyMultiplier;

                // 加一点速度阻尼，防止狂抖
                Vector3 damping = -rb.velocity * waterDamping;

                rb.AddForce(buoyancy + damping, ForceMode.Force);
            }
        }
        else
        {
            if (isInWater)
            {
                // 离开水面，恢复正常阻力
                rb.drag = 0f;
                rb.angularDrag = 0.05f;
                isInWater = false;
            }
        }
    }
}

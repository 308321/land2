using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class EclipseExperiment : MonoBehaviour
{
    [Header("必需物体")]
    public Transform lantern;
    public Transform earth;
    public Transform moon;

    [Header("移动设置")]
    public float minX = -275.81f;
    public float maxX = -255.81f;
    public float orbitRadius = 3f;
    [Range(0.1f, 10f)] public float lanternSpeed = 3f;
    [Range(0.1f, 5f)] public float moonSpeed = 1f;
    public LayerMask draggableLayer;

    [Header("视觉效果")]
    public ParticleSystem eclipseEffect;
    public Material highlightMaterial;
    [Range(0f, 1f)] public float eclipseDarkness = 0.3f;

    private Transform selectedObject;
    private Vector3 offset;
    private bool isDragging;
    private Material originalLanternMaterial;
    private Material originalMoonMaterial;
    private float originalLightIntensity;
    private Light directionalLight;
    private Vector3 lanternVelocity;
    private Vector3 moonVelocity;
    private bool eclipseTriggered = false;  // ⭐新增：避免重复触发

    void Start()
    {
        if (!lantern || !earth || !moon)
        {
            Debug.LogError("必需物体未分配！", this);
            enabled = false;
            return;
        }

        StoreOriginalMaterials();

        directionalLight = FindObjectOfType<Light>();
        if (directionalLight)
            originalLightIntensity = directionalLight.intensity;

        EnsureEventSystemExists();

        lantern.position = new Vector3(
            Mathf.Clamp(lantern.position.x, minX, maxX),
            lantern.position.y,
            lantern.position.z
        );
    }

    void StoreOriginalMaterials()
    {
        if (lantern.TryGetComponent<Renderer>(out var lanternRenderer))
            originalLanternMaterial = lanternRenderer.material;

        if (moon.TryGetComponent<Renderer>(out var moonRenderer))
            originalMoonMaterial = moonRenderer.material;
    }

    void EnsureEventSystemExists()
    {
        if (FindObjectOfType<EventSystem>() == null)
        {
            var es = new GameObject("EventSystem");
            es.AddComponent<EventSystem>();
            es.AddComponent<StandaloneInputModule>();
        }
    }

    void Update()
    {
        HandleMouseInput();
        if (isDragging)
            MoveSelectedObject();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
            TryStartDrag();
        else if (Input.GetMouseButtonUp(0) && isDragging)
            EndDrag();
    }

    void TryStartDrag()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 100f, draggableLayer))
        {
            if (hit.transform == lantern || hit.transform == moon)
            {
                StartDrag(hit.transform);
                HighlightObject(hit.transform, true);
            }
        }
    }

    void StartDrag(Transform obj)
    {
        selectedObject = obj;
        isDragging = true;
        offset = selectedObject.position - GetMouseWorldPoint();
    }

    void EndDrag()
    {
        HighlightObject(selectedObject, false);
        isDragging = false;
        CheckEclipse();
    }

    void HighlightObject(Transform obj, bool highlight)
    {
        if (!highlightMaterial || !obj.TryGetComponent<Renderer>(out var renderer))
            return;

        renderer.material = highlight ? highlightMaterial :
            (obj == lantern ? originalLanternMaterial : originalMoonMaterial);
    }

    void MoveSelectedObject()
    {
        Vector3 targetPos = GetMouseWorldPoint() + offset;

        if (selectedObject == lantern)
        {
            targetPos = new Vector3(
                Mathf.Clamp(targetPos.x, minX, maxX),
                lantern.position.y,
                lantern.position.z
            );

            lantern.position = Vector3.SmoothDamp(
                lantern.position,
                targetPos,
                ref lanternVelocity,
                0.15f,
                lanternSpeed
            );
        }
        else if (selectedObject == moon)
        {
            Vector3 toEarth = earth.position - targetPos;
            targetPos = earth.position - Vector3.ClampMagnitude(toEarth, orbitRadius);

            moon.position = Vector3.Lerp(
                moon.position,
                targetPos,
                moonSpeed * Time.deltaTime
            );
        }
    }

    Vector3 GetMouseWorldPoint()
    {
        var plane = new Plane(Camera.main.transform.forward, selectedObject.position);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return plane.Raycast(ray, out float distance) ? ray.GetPoint(distance) : selectedObject.position;
    }

    void CheckEclipse()
    {
        Vector3 sunToEarth = (earth.position - lantern.position).normalized;
        Vector3 earthToMoon = (moon.position - earth.position).normalized;

        float angle = Vector3.Angle(sunToEarth, earthToMoon);
        float distanceRatio = Vector3.Distance(moon.position, earth.position) / orbitRadius;

        if (angle < 5f && distanceRatio > 0.9f && distanceRatio < 1.1f)
            OnEclipseSuccess();
    }

    void OnEclipseSuccess()
    {
        if (eclipseTriggered) return;  // ⭐新增：避免多次触发
        eclipseTriggered = true;

        eclipseEffect?.Play();

        if (directionalLight)
        {
            directionalLight.intensity = originalLightIntensity * eclipseDarkness;
            Invoke(nameof(RestoreLight), 3f);
        }
    }

    void RestoreLight()
    {
        if (directionalLight)
            directionalLight.intensity = originalLightIntensity;
    }

    // ⭐⭐新增内容开始⭐⭐
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"进入了触发器 {other.name}", this);

        if (other.transform == moon && gameObject.name == "阴影部分")
        {
            Debug.Log("小铜球进入了阴影部分！");
            OnEclipseSuccess();
        }
    }
    // ⭐⭐新增内容结束⭐⭐

    void OnDrawGizmosSelected()
    {
        if (!earth) return;

        Gizmos.color = Color.yellow;
        Vector3 start = new Vector3(minX, earth.position.y, earth.position.z);
        Vector3 end = new Vector3(maxX, earth.position.y, earth.position.z);
        Gizmos.DrawLine(start, end);
        Gizmos.DrawWireCube((start + end) * 0.5f, new Vector3(maxX - minX, 0.5f, 0.5f));

        Gizmos.color = new Color(0, 0.5f, 1f, 0.5f);
        Gizmos.DrawWireSphere(earth.position, orbitRadius);
    }
}

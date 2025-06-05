using UnityEngine;  // 添加这行
using GLTFast;

public class TestGltf : MonoBehaviour
{ // 现在MonoBehaviour可以被识别
    void Start()
    {
        Debug.Log("GLTFast loaded: " + (new GltfImport() != null));
    }
}
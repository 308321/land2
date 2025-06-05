#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class TerrainGradientTool : EditorWindow {
    private Gradient gradient = new Gradient();
    private Texture2D gradientTexture;
    private Terrain terrain;
    private Material terrainMaterial;
    
    [MenuItem("Tools/Terrain Height Gradient")]
    public static void ShowWindow() {
        GetWindow<TerrainGradientTool>("Terrain Gradient");
    }
    
    void OnEnable() {
        // 初始化默认渐变
        gradient = new Gradient() {
            colorKeys = new GradientColorKey[] {
                new GradientColorKey(Color.blue, 0),
                new GradientColorKey(Color.green, 0.5f),
                new GradientColorKey(Color.red, 1)
            },
            alphaKeys = new GradientAlphaKey[] {
                new GradientAlphaKey(1, 0),
                new GradientAlphaKey(1, 1)
            }
        };
    }
    
    void OnGUI() {
        GUILayout.Label("Terrain Height Gradient Settings", EditorStyles.boldLabel);
        
        terrain = EditorGUILayout.ObjectField("Terrain", terrain, typeof(Terrain), true) as Terrain;
        
        // 渐变编辑器
        EditorGUI.BeginChangeCheck();
        gradient = EditorGUILayout.GradientField("Height Gradient", gradient);
        if (EditorGUI.EndChangeCheck()) {
            UpdateGradientTexture();
        }
        
        if (terrain != null) {
            if (GUILayout.Button("Apply to Terrain")) {
                ApplyToTerrain();
            }
        } else {
            EditorGUILayout.HelpBox("Please assign a Terrain object", MessageType.Warning);
        }
    }
    
    void UpdateGradientTexture() {
        gradientTexture = new Texture2D(256, 1);
        gradientTexture.wrapMode = TextureWrapMode.Clamp;
        
        for (int x = 0; x < gradientTexture.width; x++) {
            float t = x / (float)gradientTexture.width;
            Color color = gradient.Evaluate(t);
            gradientTexture.SetPixel(x, 0, color);
        }
        gradientTexture.Apply();
    }
    
    void ApplyToTerrain() {
        if (gradientTexture == null) {
            UpdateGradientTexture();
        }
        
        // 获取或创建地形材质
        terrainMaterial = terrain.materialTemplate;
        if (terrainMaterial == null || terrainMaterial.shader.name != "Custom/TerrainHeightGradient") {
            terrainMaterial = new Material(Shader.Find("Custom/TerrainHeightGradient"));
            terrain.materialTemplate = terrainMaterial;
        }
        
        // 设置材质参数
        terrainMaterial.SetFloat("_MinHeight", terrain.transform.position.y);
        terrainMaterial.SetFloat("_MaxHeight", terrain.transform.position.y + terrain.terrainData.size.y);
        terrainMaterial.SetTexture("_GradientRamp", gradientTexture);
        
        // 强制更新地形
        EditorUtility.SetDirty(terrain);
    }
}
#endif
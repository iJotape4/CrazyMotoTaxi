using UnityEngine;

public class Vehicle : MonoBehaviour
{
    Material mat;
    [SerializeField] Color color;

    private const string p_MainColor = "_Color";
    private void Awake()
    {
        mat = GetComponent<MeshRenderer>().material;
    }
    void Start()
    {
        color = Random.ColorHSV();
        mat.SetColor(p_MainColor, color );
    }

    //Here are some commonly used methods to modify shader properties:
    //SetColor: Change the color of a material(e.g., the albedo tint color).
    //SetFloat: Set a floating-point value(e.g., the normal map multiplier).
    //SetInteger: Set an integer value in the material.
    //SetTexture: Assign a new texture to the material 1.
}
using UnityEngine;

public class VehicleColor : MonoBehaviour
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
}
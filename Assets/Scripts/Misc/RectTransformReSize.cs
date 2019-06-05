using UnityEngine;

public class RectTransformReSize : MonoBehaviour
{

    private RectTransform rect;
    [SerializeField] private bool add;
    [SerializeField] private float up;
    [SerializeField] private float down;
    [SerializeField] private float right;
    [SerializeField] private float left;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    public void reSize()
    {

    }
}

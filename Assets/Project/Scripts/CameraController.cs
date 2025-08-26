using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [Header("Zoom settings (2D Orthographic)")]
    [SerializeField] private float step = 0.5f;       // �� ������� �������� size �� ���� �����
    [SerializeField] private float duration = 0.25f;  // ������������ �����
    [SerializeField] private float minOrtho = 1f;     // ����������� orthographicSize (�����)
    [SerializeField] private float maxOrtho = 10f;    // ������������ orthographicSize (������)
    [SerializeField] private Ease ease = Ease.OutQuad;

    private Camera cam;
    private Tween currentTween;

    private void Awake()
    {
        cam = GetComponent<Camera>() ?? Camera.main;
        if (cam == null)
            Debug.LogError("CameraSimpleZoom2D: Camera not found on object and Camera.main is null.");
        else if (!cam.orthographic)
            Debug.LogWarning("CameraSimpleZoom2D: Camera is not orthographic. Script designed for 2D orthographic cameras.");
    }

    /// <summary>���������� (��������� orthographicSize)</summary>
    public void ZoomIn()
    {
        if (cam == null) return;
        StartZoom(true);
    }

    /// <summary>�������� (��������� orthographicSize)</summary>
    public void ZoomOut()
    {
        if (cam == null) return;
        StartZoom(false);
    }

    private void StartZoom(bool zoomIn)
    {
        // �������� ���������� ����
        if (currentTween != null && currentTween.IsActive()) currentTween.Kill();

        float current = cam.orthographicSize;
        float target = current + (zoomIn ? -step : step);
        target = Mathf.Clamp(target, minOrtho, maxOrtho);

        // ���� ������� �������� ����� �������� � ������ �� ������
        if (Mathf.Approximately(target, current)) return;

        currentTween = cam.DOOrthoSize(target, duration).SetEase(ease);
    }
}

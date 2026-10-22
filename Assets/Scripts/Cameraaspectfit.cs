using UnityEngine;
using UnityEngine.SceneManagement;

// 화면 비율이 달라도 에디터에서 보던 가로 범위가 그대로 보이게 카메라를 맞춘다.
// 빈 오브젝트에 붙일 필요 없이, 씬이 로드될 때 자동으로 MainCamera에 적용된다.
public class CameraAspectFit : MonoBehaviour
{
    // 기준 화면비 (에디터 Game 뷰에서 UI/오브젝트 배치를 맞춘 비율)
    const float DesignAspect = 2.4f;

    Camera cam;
    float baseFov, baseOrtho;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Boot()
    {
        SceneManager.sceneLoaded += (s, m) => Attach();
        Attach();
    }

    static void Attach()
    {
        var c = Camera.main;
        if (c != null && c.GetComponent<CameraAspectFit>() == null) c.gameObject.AddComponent<CameraAspectFit>();
    }

    void Awake()
    {
        cam = GetComponent<Camera>();
        baseFov = cam.fieldOfView;
        baseOrtho = cam.orthographicSize;
    }

    void LateUpdate()
    {
        float aspect = (float)Screen.width / Screen.height;
        if (aspect >= DesignAspect) { Apply(1f); return; }
        Apply(DesignAspect / aspect);   // 좁은 화면일수록 세로 범위를 넓혀 가로를 유지
    }

    void Apply(float k)
    {
        if (cam.orthographic) cam.orthographicSize = baseOrtho * k;
        else cam.fieldOfView = 2f * Mathf.Atan(Mathf.Tan(baseFov * Mathf.Deg2Rad * 0.5f) * k) * Mathf.Rad2Deg;
    }
}
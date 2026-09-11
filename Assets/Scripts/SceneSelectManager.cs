using UnityEngine;

public class SceneSelectManager : MonoBehaviour
{

    public void OnNextStage()
    {
        GameManager.Instance.NextStage();
    }
}

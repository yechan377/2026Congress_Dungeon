using UnityEngine;

public class SingletonManagers<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static bool HasInstance => instance != null;
    public static T Instance
    {
        get
        {
            if(instance != null) return instance;

            GameObject go = new GameObject();
            instance = go.AddComponent<T>();
            go.name = instance.GetType().Name;

            DontDestroyOnLoad( go );

            return instance;
        }
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
    }

    /*
        1. GameManager : SingletonManagers<GameMAnager>
        2. GameManager.Instance
        3. GameManager.Instance -> 없음?
            -> 새로 만들어줘야함 
            1. GameObject 를 새로 만들고
            2. GameObject.AddComponent<GameManager>()
        4. 있어 
            return instance;
        5. Awake -> 만약 기존 인스턴스가 존재해 그리고 그게 내가 아니야
            -> 나 자신 삭제
    */
}

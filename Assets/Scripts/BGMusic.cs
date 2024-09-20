using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public static BGMusic instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(instance);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}

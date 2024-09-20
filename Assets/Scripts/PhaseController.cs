using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseController : MonoBehaviour
{
    public static PhaseController Instance;
    [HideInInspector] public int phaseCounter;
    [SerializeField] Animator eye;
    bool goToNextPhase;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        phaseCounter = 1;
        DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType<PhaseController>().Length > 1)
            Destroy(gameObject);
    }

    public void DisableNextPhase() 
    {
        goToNextPhase = false;
        eye.enabled = false;
    }

    public void TriggerNextPhase()
    {
        if (phaseCounter < 3)
        {
            eye.enabled = true;
            goToNextPhase = true;
        }
        else
        {
            DisableNextPhase();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string otherLayerName = LayerMask.LayerToName(other.gameObject.layer);
        if (otherLayerName == "Player" && goToNextPhase)
        {
            phaseCounter++;
            SceneManager.LoadScene("GamePlay Scene");
        }
    }
}

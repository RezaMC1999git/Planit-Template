using UnityEngine;

public class DialougesTrigger : MonoBehaviour
{
    [SerializeField] GameObject EButton;
    bool canDialouge;

    private void Update()
    {
        if (canDialouge && Input.GetKeyDown(KeyCode.E))
        {
            DialougesManager.instance.ShowDialougeBox();
            canDialouge = false;
            EButton.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string otherLayerName = LayerMask.LayerToName(other.gameObject.layer);
        if (otherLayerName == "Player")
        {
            canDialouge = true;
            EButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        string otherLayerName = LayerMask.LayerToName(other.gameObject.layer);
        if (otherLayerName == "Player")
        {
            canDialouge = false;
            EButton.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialougesManager : MonoBehaviour
{
    public static DialougesManager instance;
    [SerializeField] TextMeshProUGUI tittleText, dialougeText;
    [SerializeField] List<string> firstPhaseDialouges, secondPhaseDialouges, thirdPhaseDialouges;
    List<string> characterNames = new List<string> { "NPC", "Player" };
    [SerializeField] Animator animator;
    int counter = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        PhaseController.Instance.DisableNextPhase();
    }

    public void ShowDialougeBox()
    {
        animator.Play("Appear");
        P_Movement.instance.canMove = false;
        tittleText.text = characterNames[0];
        switch (PhaseController.Instance.phaseCounter)
        {
            case 1:
                dialougeText.text = firstPhaseDialouges[0];
                break;
            case 2:
                dialougeText.text = secondPhaseDialouges[0];
                break;
            case 3:
                dialougeText.text = thirdPhaseDialouges[0];
                break;
            default:
                break;
        }

        animator.enabled = true;
    }

    public void NextDialouge()
    {
        counter++;
        switch (PhaseController.Instance.phaseCounter)
        {
            case 1:
                if (counter < firstPhaseDialouges.Count)
                {
                    dialougeText.text = firstPhaseDialouges[counter];
                    tittleText.text = characterNames[counter % 2];
                }
                else
                    StartCoroutine(WaitForDialouges());
                break;

            case 2:
                if (counter < secondPhaseDialouges.Count)
                {
                    dialougeText.text = secondPhaseDialouges[counter];
                    tittleText.text = characterNames[counter % 2];
                }
                else
                    StartCoroutine(WaitForDialouges());
                break;

            case 3:
                if (counter < thirdPhaseDialouges.Count)
                {
                    dialougeText.text = thirdPhaseDialouges[counter];
                    tittleText.text = characterNames[counter % 2];
                }
                else
                    StartCoroutine(WaitForDialouges());
                break;
            default:
                break;
        }

    }

    private IEnumerator WaitForDialouges()
    {
        PhaseController.Instance.TriggerNextPhase();
        animator.Play("DissAppear");
        yield return new WaitForSeconds(1f);
        P_Movement.instance.canMove = true;
        counter = 0;
        animator.enabled = false;
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FollowPoints : MonoBehaviour
{
    public static FollowPoints instance;
    public Transform npcTransform, visualTransform;
    [SerializeField] 
    List<Transform> wayPoints;
    [SerializeField]
    float followSpeed = 5f;
    [SerializeField]
    Animator npcAnimator;
    
    [HideInInspector]
    public bool playerDetected;

    float visualLocalScale;
    int counter = 1;

    private void Awake()
    {
        visualLocalScale = visualTransform.localScale.x;
        wayPoints = wayPoints.OrderBy(x => Random.value).ToList();
        npcTransform.transform.position = wayPoints[0].position;
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        Follow();
    }

    private void Follow() 
    {
        if (!playerDetected) 
        {
            npcAnimator.SetBool("Walk", true);
            if (npcTransform.position != wayPoints[counter].position) 
            {
                npcTransform.position = Vector2.MoveTowards(npcTransform.position, wayPoints[counter].position, followSpeed * Time.deltaTime);
                if (wayPoints[counter].position.x < npcTransform.position.x)
                    visualTransform.localScale = new Vector3(-visualLocalScale, visualLocalScale, 1f);
                else
                    visualTransform.localScale = new Vector3(visualLocalScale, visualLocalScale, 1f);
            }
                
            else
            {
                counter++;
                if (counter >= wayPoints.Count) 
                {
                    wayPoints = wayPoints.OrderBy(x => Random.value).ToList();
                    counter = 0;
                }
            }
        }
        else
        {
            npcAnimator.SetBool("Walk", false);
        }
    }

    public void LookAtPlayer(Transform playerTransform) 
    {
        if(npcTransform.position.x < playerTransform.position.x)
            visualTransform.localScale = new Vector3(visualLocalScale, visualLocalScale, 1f);
        else
            visualTransform.localScale = new Vector3(-visualLocalScale, visualLocalScale, 1f);
    }
}

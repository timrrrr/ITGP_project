using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour
{
    
    public int clothesNumber = 0;
    public int clothesCapacity = 1;
    private Rigidbody Rb;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isLookingFor = false;
    [SerializeField] private Vector3 ExitPos;
    [SerializeField] private States currentState;
    [SerializeField] private NavMeshAgent agent;
    
    private Vector3 previousPosition;
    public float curSpeed;
    
    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        Debug.Log(curSpeed);
        if (curSpeed > 1f)
            animator.SetBool("IsWalking", true);
        else
            animator.SetBool("IsWalking", false);
    }
    
    private IEnumerator UpdateSpeed ()
    {
        while (true)
        {
            Vector3 curMove = transform.position - previousPosition;
            curSpeed = curMove.magnitude / 0.2f;
            previousPosition = transform.position;
            
            yield return new WaitForSeconds(0.2f);
        }
        
    }
    
    private void Start()
    {
        StartCoroutine(LookForHanger());
        StartCoroutine(UpdateSpeed());
    }
    
    

    private IEnumerator LookForHanger()
    {
        isLookingFor = true;
        agent.SetDestination(transform.position); // to stop the customer from moving
        currentState = States.LookingForAHanger;
        
        //Debug.Log("start while true");
        while (true)
        {
            Vector3 hangerPos = FindRandomHanger();
            if (!hangerPos.Equals(new Vector3(0f, -100f, 0f)) && clothesNumber < clothesCapacity)
            {
                GoToHanger(hangerPos); 
                break;
            }
            //Debug.Log("Cant find :("); 
            yield return new WaitForSeconds(2);
        }
        isLookingFor = false;
        yield return null;
    }

    public void StartLookingAgain()
    {
        if (currentState != States.LookingForAHanger && !isLookingFor)
        {
            StartCoroutine(LookForHanger());
            //Debug.Log("StartLookingAgain"); 
        }
    }
    
    // returns coordinates of the nearest hanger o
    // returns (0,-100,0) if can't find any
    static private Vector3 FindRandomHanger() 
    
    {
        GameObject[] hangers = GameObject.FindGameObjectsWithTag("Hanger");
        if (hangers.Length != 0)
        {
            //hangers.Shuffle(hangers.Length);
            foreach (GameObject hanger in hangers)
            {
                ClothHanger clothHangerScript = hanger.GetComponent<ClothHanger>();
                
                if (clothHangerScript.clothesNumber > 0)
                {
                    return hanger.transform.position;
                }
            }
        }
        return new Vector3(0f, -100f, 0f);
    }

    private void GoToHanger(Vector3 hangerPos)
    {
        currentState = States.GoingToHanger;
        agent.SetDestination(hangerPos);
        
    }

    public void PickClothes()
    {
        if(clothesNumber < clothesCapacity)
            clothesNumber += 1;
        GoToTheCheckout();
    }

    private void GoToTheCheckout()
    {
        currentState = States.GoingToTheCheckout;
        Vector3 checkoutPos = GameObject.FindGameObjectWithTag("Checkout").transform.position;
        agent.SetDestination(checkoutPos);
    }
    public void WalkAway()
    {
        currentState = States.WalksAway; 
        agent.SetDestination(ExitPos);
        StartCoroutine(DestroyLater());

        //Debug.Log("walks away");
    }

    private IEnumerator DestroyLater()
    {
        yield return new WaitForSeconds(15);
        Destroy(gameObject);
    }

    private enum States
    {
        LookingForAHanger,
        GoingToHanger,
        GoingToTheCheckout,
        WaitingToPay,
        WalksAway
    }
    
    
}


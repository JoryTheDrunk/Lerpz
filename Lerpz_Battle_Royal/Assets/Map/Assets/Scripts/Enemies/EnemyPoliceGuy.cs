//Mitchell Hewitt
using UnityEngine;
using System.Collections;

public class EnemyPoliceGuy : MonoBehaviour
{

    //Animations are as follows: idle, threaten, turnjump, and attackrun

    public float attackTurnTime = 0.7f;
    public float rotateSpeed = 120.0f;
    public float attackDistance = 17.0f;
    public float extraRunTime = 2.0f;
    public int damage = 1;

    public float attackSpeed = 5.0f;
    public float attackRotateSpeed = 20.0f;

    public float idleTime = 1.6f;

    public Vector3 punchPosition = new Vector3(0.4f, 0, 0.7f);
    public float punchRadius = 1.1f;

    // Sounds:
    public AudioClip idleSound;
    public AudioClip attackSound;

    private float attackAngle = 10.0f;
    private bool isAttacking = false;
    private float lastPunchTime = 0.0f;

    public Transform target;

    private CharacterController characterController;

    public LevelStatus levelStateMachine;


    // Use this for initialization
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        StartCoroutine(Setup());

    }
    private void Update()
    {
        if(Vector3.Distance(target.position, this.transform.position) < attackDistance)
        {
            RotateTowardsPosition(target.position, rotateSpeed);
            if(isAttacking == false)
            {
                StartCoroutine(Attack());
            }
            
        }
        if (Vector3.Distance(target.position, this.transform.position) > attackDistance)
        {
            isAttacking = false;
        }


    }
    IEnumerator Setup()
    {
 
        levelStateMachine = GameObject.Find("/Level").GetComponent<LevelStatus>();

        if (!levelStateMachine)
        {
            Debug.Log("ERROR!: EnemyPoliceGuy NO LEVEL STATUS SCRIPT FOUND!");
        }

        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        GetComponent<Animation>().wrapMode = WrapMode.Loop;

        //Setup Animations
        GetComponent<Animation>()["gothit"].wrapMode = WrapMode.Once;
        GetComponent<Animation>()["threaten"].wrapMode = WrapMode.Once;
        GetComponent<Animation>()["turnjump"].wrapMode = WrapMode.Once;
        GetComponent<Animation>().Play("idle");

        GetComponent< Animation > ().Play("idle");
        GetComponent< Animation > ()["threaten"].wrapMode = WrapMode.Once;
        GetComponent< Animation > ()["turnjump"].wrapMode = WrapMode.Once;
        GetComponent< Animation > ()["gothit"].wrapMode = WrapMode.Once;
        GetComponent< Animation > ()["gothit"].layer = 1;


          GetComponent<Animation>()["gothit"].layer = 1;

        //Initialize audio clip. Make sure it's not set to the 'idle' sound.
        GetComponent<AudioSource>().clip = idleSound;
        StartCoroutine(Idle());

        yield return new WaitForSeconds(Random.value);

        //Just attack for now 
     
            //Don't do anything when idle. And wait for player to be in range.
            //This is the perfect time for the player to attack us.
         
            //Prepare, turn to player and attack him.
         
      
    }

    IEnumerator Idle()
    {
        if (idleSound)
        {
            if (GetComponent<AudioSource>().clip != idleSound)
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = idleSound;
                GetComponent<AudioSource>().loop = true;
                GetComponent<AudioSource>().Play(); //Play the idle sound.
            }
        }

        // Don't do anything when idle
        // The perfect time for the player to attack us
        yield return new WaitForSeconds(idleTime);

        // And if the player is really far away.
        // We just idle around until he comes back
        // unless we're dying, in which case we just keep idling.
        characterController.SimpleMove(Vector3.zero);
        new WaitForSeconds(0.2f);

        Vector3 offset = transform.position - target.position;

        // if player is in range again, stop lazyness
        // Good Hunting!

        if (offset.sqrMagnitude < attackDistance)
        {
            yield return offset;
        }

       
    }

    float RotateTowardsPosition(Vector3 targetPos, float rotateSpeed)
    {
        // Compute relative point and get the angle towards it.
        Vector3 relative = transform.InverseTransformPoint(targetPos);
        float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;

        // Clamp it with the max rotation speed.
        float maxRotation = rotateSpeed * Time.deltaTime;
        float clampedAngle = Mathf.Clamp(angle, -maxRotation, maxRotation);

        // Rotate
        transform.Rotate(0, clampedAngle, 0);

        // Return current angle.
        return angle;
    }

    public IEnumerator Attack()
    {
        isAttacking = true;

        if (attackSound)
        {
            AudioSource source = GetComponent<AudioSource>();
            AudioClip clip = GetComponent<AudioSource>().clip;
            if (clip != attackSound)
            {
                source.Stop();
                source.clip = attackSound;
                source.loop = true;
                source.Play();
            }
        }

        // Already queue up the attack run animation but set it's blend wieght to 0
        // it gets blended in later
        // it is looping so it will keep playing until we stop it.
        Animation attackRun = GetComponent<Animation>();
        attackRun.Play("attackrun");

        // First we wait for a bit so the player can prepare while we turn around
        // As we near an angle of 0, we will begin to move
        float angle;
        angle = 180.0f;
        float time;
        time = 0.0f;

        while (angle > 5 || time < attackTurnTime)
        {
    
            time += Time.deltaTime;
            angle = Mathf.Abs(RotateTowardsPosition(target.position, rotateSpeed));
            float move = Mathf.Clamp01((90 - angle) / 90);

            // depending on the angle, start moving
            //GetComponent.< Animation > ()["attackrun"].weight = GetComponent.< Animation > ()["attackrun"].speed = move;
            Vector3 direction = transform.TransformDirection(Vector3.forward * attackSpeed * move);
            characterController.SimpleMove(direction);

            yield return direction;
 
        }

        // Run towards player
        float timer = 0.0f;
        bool lostSight = false;
 
        while (timer < extraRunTime)
        {

            angle = RotateTowardsPosition(target.position, attackRotateSpeed);

            // The angle of our forward direction and the player position is larger than 50 degrees
            // That means he is out of sight
            if (Mathf.Abs(angle) > 40)
                lostSight = true;
       
            // If we lost sight then we keep running for some more time (extraRunTime). 
            // then stop attacking 
            if (lostSight)
                timer += Time.deltaTime;

            // Just move forward at constant speed
            Vector3 direction = transform.TransformDirection(Vector3.forward * attackSpeed);
            characterController.SimpleMove(direction);

            // Keep looking if we are hitting our target
            // If we are, knock them out of the way dealing damage
            Vector3 pos = transform.TransformPoint(punchPosition);
            if (Time.time > lastPunchTime + 0.3 && (pos - target.position).magnitude < punchRadius)
            {
      
                // deal damage
                target.SendMessage("ApplyDamage", damage);
           
                // knock the player back and to the side
                Vector3 slamDirection = transform.InverseTransformDirection(target.position - transform.position);
  
                slamDirection.y = 0;
                slamDirection.z = 1;
                if (slamDirection.x >= 0)
                {
         
                    slamDirection.x = 1;
                
                }
                else
                {
           
                    slamDirection.x = -1;
             
                }
        

                //target.SendMessage("Slam", transform.TransformDirection(slamDirection));
                target.GetComponent<ThirdPersonController>().Slam(slamDirection);
               

                /////////////////////////////////////////
          
                lastPunchTime = Time.time;
    
            }

            // We are not actually moving forward.
            // This probably means we ran into a wall or something. Stop attacking the player.
            if (characterController.velocity.magnitude < attackSpeed * 0.3)
                break;

            // yield for one frame
            yield return new WaitForEndOfFrame();
            isAttacking = false;
  
            // Now we can go back to playing the idle animation
            Animation idle = GetComponent<Animation>();
            idle.CrossFade("idle");
        }
    }

    void ApplyDamage()
    {
        Animation gotHit = GetComponent<Animation>();
        gotHit.CrossFade("gothit");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.TransformPoint(punchPosition), punchRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
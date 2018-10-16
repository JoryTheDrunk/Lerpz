//Mitchell Hewitt
using UnityEngine;
using System.Collections;

public class ThirdPersonCharacterAttack : MonoBehaviour
{

    int punchSpeed = 1;
    float punchHitTime = 0.2f;
    float punchTime = 0.4f;
    Vector3 punchPosition = new Vector3(0, 0, 0.8f);
    float punchRadius = 1.3f;
    int punchHitPoints = 1;

    public AudioClip punchSound;

    private bool busy = false;

    void Start()
    {
        Animation punch = GetComponent<Animation>();
        punch.GetComponent<Animation>()["punch"].speed = punchSpeed;
    }

    void Update()
    {
        ThirdPersonController controller = GetComponent<ThirdPersonController>();
        if (!busy && Input.GetButtonDown("Fire1") && controller.IsGroundedWithTimeout() && !controller.IsMoving())
        {
            SendMessage("DidPunch");
            busy = true;
        }
    }

    IEnumerator DidPunch()
    {
        AnimationState punch = GetComponent<Animation>().CrossFadeQueued("punch", 0.1f, QueueMode.PlayNow);
        yield return new WaitForSeconds(punchHitTime);
        Vector3 pos = transform.TransformPoint(punchPosition);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject go in enemies)
        {
            GameObject enemy = go.gameObject;
            enemy.GetComponent<EnemyDamage>();
            if (enemy == null)
                continue;

            if (Vector3.Distance(enemy.transform.position, pos) < punchRadius)
            {
                enemy.SendMessage("ApplyDamage", punchHitPoints);
                // Play sound.
                if (punchSound)
                {
                    AudioSource source = GetComponent<AudioSource>();
                    source.PlayOneShot(punchSound);
                }
            }
        }
        yield return new WaitForSeconds(punchTime - punchHitTime);
        busy = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.TransformPoint(punchPosition), punchRadius);
    }
}
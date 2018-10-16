//Mitchell Hewitt
using UnityEngine;

public class LaserTrap : MonoBehaviour
{

    public float height = 3.2f;
    public float speed = 2.0f;
    public float timingOffset = 0.0f;
    public int laserWidth = 12;
    public int damage = 1;
    public GameObject hitEffect;
    private ThirdPersonStatus Player;
    private Vector3 originalPosition;
    private RaycastHit hit;
    private float lastHitTime = 0.0f;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonStatus>();
        originalPosition = transform.position;
        LineRenderer lineRen = GetComponent<LineRenderer>();
        lineRen.SetPosition(1, Vector3.forward * laserWidth);
    }

    void Update()
    {
        float offset = (1 + Mathf.Sin(Time.time * speed + timingOffset)) * height / 2;
        transform.position = originalPosition + new Vector3(0, offset, 0);

        if (Time.time > lastHitTime + 0.25 && Physics.Raycast(transform.position, transform.forward, hit.distance, laserWidth))
        {
            //if (hit.collider.tag == "Player" || hit.collider.tag == "Enemy")
            //{
            //    Instantiate(hitEffect, hit.point, Quaternion.identity);
            //    hit.collider.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
            //    lastHitTime = Time.time;
            //}
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player.health--;
            Instantiate(hitEffect, other.transform.position, Quaternion.identity);
        }
    }
}

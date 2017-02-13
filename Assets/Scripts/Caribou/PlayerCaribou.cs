using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class PlayerCaribou : MonoBehaviour {

    public GameObject PlayerCalf;
    public AbilityBar abilityBar;
    Vector3 targetPos = new Vector3();
    public float speed = 5f;
    float snortCoolDown = 0f;
    public int maxSnortCooldown = 10;
    public float snortCoolDownSpeed = 1f;

    AudioSource source;
    


    public void MousePressed(Vector3 loc)
    {
        targetPos = loc;
    }

    public void Snort()
    {
        if (snortCoolDown == 0)
        {

            if (PlayerCalf && PlayerCalf.GetComponent<FlockingAI>() && !PlayerCalf.GetComponent<FlockingAI>().isCorpse)
            {
                PlayerCalf.GetComponent<Calf>().currentSpeedBoost = PlayerCalf.GetComponent<Calf>().maxSpeedBoost;
            }
            source.Play();
            snortCoolDown = maxSnortCooldown;
        }
    }

    // Use this for initialization
    void Start () {
        GameObject.FindObjectOfType<FlockManager>().flock.Add(this.gameObject);
        targetPos = transform.position;
        source = GetComponent<AudioSource>();
        
    }
	
	// Update is called once per frame
	void Update () {

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        float step = speed * Time.deltaTime;


        float angToGoal = MathHelper.AngleBetweenPoints(this.transform.position, targetPos);
        if (Vector3.Distance(this.transform.position, targetPos) > .2f)
        {
            transform.eulerAngles = new Vector3(0, 0, angToGoal);
        }


        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        if (snortCoolDown > 0f)
        {
            snortCoolDown -= Time.deltaTime * snortCoolDownSpeed;
        }
        else if (snortCoolDown <= 0f)
        {
            snortCoolDown = 0f;
        }

        abilityBar.SetFillAmount(snortCoolDown / maxSnortCooldown);
    }
}

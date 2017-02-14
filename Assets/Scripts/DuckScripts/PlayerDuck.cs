using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class PlayerDuck : MonoBehaviour {

    public GameObject QuackCircle;
    public Transform foxParent;
    public AbilityBar abilityBar;
    public float speed = 5f;
    //Vector3 TargetPosition;
    public float quackRadius = 12f;
    float quackCoolDown = 0f;
    public int maxQuackCooldown = 10;
    public float quackCoolDownSpeed = 1f;
    Vector3 targetPos = new Vector3();
    AudioSource source;
    AudioClip quack;


    public void MousePressed(Vector3 loc)
    {
       targetPos = loc;  
    }

    public void Quack()
    {
        if (quackCoolDown == 0)
        {
            //QuackCircle.GetComponent<QuackCircle>().currentAlpha = QuackCircle.GetComponent<QuackCircle>().maxAlpha;
            QuackCircle.GetComponent<OpacityFade>().SetPresentOpacity(QuackCircle.GetComponent<QuackCircle>().maxAlpha);
            QuackCircle.GetComponent<OpacityFade>().SetTargetOpacity(0, QuackCircle.GetComponent<QuackCircle>().circleVisibleLife);
            foreach (GameObject duckling in GameObject.FindObjectOfType<DucklingManager>().Ducklings)
            {
                if (!duckling.GetComponent<Duckling>().isDead)
                {
                    float dist = Vector3.Distance(transform.position, duckling.transform.position);
                    if (dist <= quackRadius)
                    {
                        duckling.GetComponent<Duckling>().quackStrength = duckling.GetComponent<Duckling>().maxQuackStrength;
                    }
                }
            }
            Transform closest = foxParent.GetChild(0);
            foreach(Transform t in foxParent)
            {
                if (Vector2.Distance(transform.position, t.position) < Vector2.Distance(transform.position, closest.position))
                    closest = t;
            }
            closest.GetComponent<Fox>().HeardAQuack();

            LOLAudio.Instance.PlayAudio("DuckCall.wav", false);
            quackCoolDown = maxQuackCooldown;
        }
    }

    public Vector3 GetTargetPos()
    {
        Vector2 _clickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = new Vector3( _clickedPos.x, _clickedPos.y, 1 );
        return (targetPos);
    }

	// Use this for initialization
	void Start () {
        //GameObject.FindObjectOfType<FlockManager>().flock.Add(this.gameObject);
        targetPos = transform.position;
        source = GetComponent<AudioSource>();
        quack =(AudioClip)Resources.Load("sounds/duckCall.mp3");
        QuackCircle.GetComponent<OpacityFade>().SetPresentOpacity(0);
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
            

        transform.position = Vector3.MoveTowards(transform.position, targetPos , step);

        if(quackCoolDown > 0f)
        {
            quackCoolDown -= Time.deltaTime * quackCoolDownSpeed;
        }
        else if(quackCoolDown <= 0f)
        {
            quackCoolDown = 0f;
        }

        abilityBar.SetFillAmount(quackCoolDown / maxQuackCooldown);
    }
}

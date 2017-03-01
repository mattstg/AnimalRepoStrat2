using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectedByRiver : MonoBehaviour {

    Dictionary<WaterStream, WaterStreamStruct> moveBy = new Dictionary<WaterStream, WaterStreamStruct>();
    public bool isPlayerDuck = false;

    public void SetMoveBy(Vector2 dir, WaterStream _caller, float forceAmt, bool remove)
    {
        if (remove)
            moveBy.Remove(_caller);
        else
        {
            if (!moveBy.ContainsKey(_caller))
                moveBy.Add(_caller, new WaterStreamStruct(dir,forceAmt));
        }
    }

    public void Cleanse()
    { //When checkpoints happen and river colliders get disabled, it could lead to caribou still thinking there affected
        moveBy = new Dictionary<WaterStream, WaterStreamStruct>();
    }

	// Update is called once per frame
	void Update ()
    {
        if (moveBy.Count <= 0)
            return;

        foreach (KeyValuePair<WaterStream, WaterStreamStruct> kv in moveBy)
        {
            Vector2 _old = transform.position;
            Vector2 _new = transform.position = Vector2.MoveTowards(_old, _old + kv.Value.dir * kv.Value.forceAmt, kv.Value.forceAmt * Time.deltaTime);

            if (isPlayerDuck)
            {
                Vector2 offset = kv.Value.dir * kv.Value.forceAmt * Time.deltaTime;
                PlayerDuck player = GetComponent<PlayerDuck>();
                player.targetPos = player.targetPos + new Vector3(offset.x, offset.y, 0);
            }
        }
    }

    public class WaterStreamStruct
    {
        public Vector2 dir;
        public float forceAmt;
        public WaterStreamStruct(Vector2 _dir, float _forceAmt)
        {
            dir = _dir;
            forceAmt = _forceAmt;
        }
    }
}

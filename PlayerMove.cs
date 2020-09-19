using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PlayerMove : MonoBehaviour
{
    public PhotonView PV;

    public SpriteRenderer SR;

    private float Axis;
    private void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            Axis = Input.GetAxisRaw("Horizontal");

            transform.Translate(Axis * Time.deltaTime * 7, 0, 0);

            if (Axis != 0) PV.RPC("FlipXRPC", RpcTarget.AllBuffered, Axis);
        }
    }

    [PunRPC]
    void FlipXRPC(float axis)
    {
        SR.flipX = axis == -1;
    }

}

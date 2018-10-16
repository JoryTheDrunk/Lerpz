//Cale T
//CSG-128
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Third Person Player/Third Person Player Animation")]
public class ThirdPersonPlayerAnimation : MonoBehaviour
{
    public float runSpeedScale = 1.0f;
    public float walkSpeedScale = 1.0f;

    // Use this for initialization
    void Start()
    {
        GetComponent<Animation>().wrapMode = WrapMode.Loop;

        GetComponent<Animation>()["run"].layer = -1;
        GetComponent<Animation>()["walk"].layer = -1;
        GetComponent<Animation>()["idle"].layer = -2;
        GetComponent<Animation>().SyncLayer(-1);

        GetComponent<Animation>()["ledgefall"].layer = 9;
        GetComponent<Animation>()["ledgefall"].wrapMode = WrapMode.Loop;


        // The jump animation is clamped and overrides all others
        GetComponent<Animation>()["jump"].layer = 10;
        GetComponent<Animation>()["jump"].wrapMode = WrapMode.ClampForever;

        GetComponent<Animation>()["jumpfall"].layer = 10;
        GetComponent<Animation>()["jumpfall"].wrapMode = WrapMode.ClampForever;

        // This is the jet-pack controlled descent animation.
        GetComponent<Animation>()["jetpackjump"].layer = 10;
        GetComponent<Animation>()["jetpackjump"].wrapMode = WrapMode.ClampForever;

        GetComponent<Animation>()["jumpland"].layer = 10;
        GetComponent<Animation>()["jumpland"].wrapMode = WrapMode.Once;

        GetComponent<Animation>()["walljump"].layer = 11;
        GetComponent<Animation>()["walljump"].wrapMode = WrapMode.Once;

        // we actually use this as a "got hit" animation
        GetComponent<Animation>()["buttstomp"].speed = 0.15f;
        GetComponent<Animation>()["buttstomp"].layer = 20;
        GetComponent<Animation>()["buttstomp"].wrapMode = WrapMode.Once;
        var punch = GetComponent<Animation>()["punch"];
        punch.wrapMode = WrapMode.Once;

        // We are in full control here - don't let any other animations play when we start
        GetComponent<Animation>().Stop();
        GetComponent<Animation>().Play("idle");
    }

    // Update is called once per frame
    void Update()
    {
        ThirdPersonController playerController = GetComponent<ThirdPersonController>();
        float currentSpeed = playerController.GetSpeed();

        // Fade in run
        if (currentSpeed > playerController.walkSpeed)
        {
            GetComponent<Animation>().CrossFade("run");
            // We fade out jumpland quick otherwise we get sliding feet
            GetComponent<Animation>().Blend("jumpland", 0);
        }
        // Fade in walk
        else if (currentSpeed > 0.1)
        {
            GetComponent<Animation>().CrossFade("walk");
            // We fade out jumpland realy quick otherwise we get sliding feet
            GetComponent<Animation>().Blend("jumpland", 0);
        }
        // Fade out walk and run
        else
        {
            GetComponent<Animation>().Blend("walk", 0.0f, 0.3f);
            GetComponent<Animation>().Blend("run", 0.0f, 0.3f);
            GetComponent<Animation>().Blend("run", 0.0f, 0.3f);
        }

        GetComponent<Animation>()["run"].normalizedSpeed = runSpeedScale;
        GetComponent<Animation>()["walk"].normalizedSpeed = walkSpeedScale;

        if (playerController.IsJumping())
        {
            if (playerController.IsControlledDescent())
            {
                GetComponent<Animation>().CrossFade("jetpackjump", 0.2f);
            }
            else if (playerController.HasJumpReachedApex())
            {
                GetComponent<Animation>().CrossFade("jumpfall", 0.2f);
            }
            else
            {
                GetComponent<Animation>().CrossFade("jump", 0.2f);
            }
        }
        // We fell down somewhere
        else if (!playerController.IsGroundedWithTimeout())
        {
            GetComponent<Animation>().CrossFade("ledgefall", 0.2f);
        }
        // We are not falling down anymore
        else
        {
            GetComponent<Animation>().Blend("ledgefall", 0.0f, 0.2f);
        }
    }

    void DidLand()
    {
        GetComponent<Animation>().Play("jumpland");
    }

    void DidButtStomp()
    {
        GetComponent<Animation>().CrossFade("buttstomp", 0.1f);
        GetComponent<Animation>().CrossFadeQueued("jumpland", 0.2f);
    }

    void Slam()
    {
        GetComponent<Animation>().CrossFade("buttstomp", 0.2f);
        ThirdPersonController playerController = GetComponent<ThirdPersonController>();
        while (!playerController.IsGrounded())
        {
            StartCoroutine(WaitAFrame());
        }
        GetComponent<Animation>().Blend("buttstomp", 0, 0);
    }

    void DidWallJump()
    {
        // Wall jump animation is played without fade.
        // We are turning the character controller 180 degrees around when doing a wall jump so the animation accounts for that.
        // But we really have to make sure that the animation is in full control so 
        // that we don't do weird blends between 180 degree apart rotations
        GetComponent<Animation>().Play("walljump");
    }

    //Used to wait one frame
    IEnumerator WaitAFrame()
    {
        yield return 0;
    }
}
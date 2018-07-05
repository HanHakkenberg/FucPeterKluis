using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gather : Weapon {

    public Equippable tool;

    public PlayerMovement playerMov;

    public bool waiting;
    public bool use;
    public float gatherRange = 2.5F;

    private GameObject player;
    public ParticleSystem myPartical;
    private Collider targetResource;
    private bool hitResource;
    // Use this for initialization
    void Start() {

        player = GameObject.FindGameObjectWithTag("Player");
        playerMov = FindObjectOfType<PlayerMovement>();
        tool = equippable;
        Inventory.itemInHand = tool;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)&& playerMov.anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle")) {
            RaycastHit hit;
            if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, gatherRange)) {
                if (hit.transform.tag == "Resource") {
                    //Debug.Log("Hit" + hit.transform);
                    targetResource = hit.collider;
                    // Debug.Log("Hit resource " + targetResource);

                    Use();
                }
            }
        }

        if (Inventory.itemInHand != playerMov.player.GetComponent<Player>().hand) {

            if (Input.GetMouseButton(0)) {
                if (hitResource) {

                    if (!waiting) {
                        playerMov.anim.SetBool("Playeraxestop", false);
                        playerMov.anim.SetBool("Playeraxe", true);
                        playerMov.anim.SetBool("Player_AxeSwing", true);
                        StartCoroutine(WaitForAnim(targetResource));
                    }

                }
            }

        }
    }

    public void GetResource(Collider col) {
        if (col != null) {
            if (myPartical != null) {
                myPartical.Play();
            }
            col.transform.GetComponent<Resource>().Harvest(this);
        }
    }

    public override void Use() {

        //  Debug.Log("Use start");
        if (!waiting) {
            use = true;

            if (Inventory.itemInHand != playerMov.player.GetComponent<Player>().hand) {
                playerMov.anim.SetBool("Playeraxestop", false);
                playerMov.anim.SetBool("Playeraxe", true);
                playerMov.anim.SetBool("Player_AxeSwing", true);

            } else {
                if (!playerMov.anim.GetBool("Playergrab")) {
                    playerMov.anim.SetBool("Playergrab", true);
                }
            }

            beingUsed = true;
            StartCoroutine(WaitForAnim(targetResource));
            // Debug.Log("Use end");
        }

    }

    private IEnumerator AnimSoundTiming() {
        yield return new WaitForSeconds(playerMov.anim.GetCurrentAnimatorStateInfo(0).length - .30F);
        hitResource = true;

        
            GetResource(targetResource);
     

    }

    private IEnumerator WaitForAnim(Collider col) {
        // Debug.Log("Start couran");
        waiting = true;
        StartCoroutine(AnimSoundTiming());
        yield return new WaitForSeconds(playerMov.anim.GetCurrentAnimatorStateInfo(0).length - 0.35f);
        use = false;
        if (Inventory.itemInHand != playerMov.player.GetComponent<Player>().hand) {
            playerMov.anim.SetBool("Player_AxeSwing", false);
            playerMov.anim.SetBool("Playeraxestop", true);
            playerMov.anim.SetBool("Playeraxe", false);
        } else {
            playerMov.anim.SetBool("Playergrab", false);
        }

        beingUsed = false;
        //  Debug.Log("End couran");
        waiting = false;

    }
}
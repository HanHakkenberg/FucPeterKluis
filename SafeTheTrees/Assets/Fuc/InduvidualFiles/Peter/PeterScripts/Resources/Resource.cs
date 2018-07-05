using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {
    public Item myResource; //Still exists so we don't need to re-add everything
    public List<Item> resourceDrops = new List<Item>();
    public Equippable.CanGather type;
    public int toughness = 1;
    public Animator myAnimator;

    private bool isGrowing;
    private bool canGather = true;
    public bool berries;
    public GameObject berryChild;
    public float berryGrowTime = 7;

    private GameManager gm;


    private void Start() {
        gm = FindObjectOfType<GameManager>();
        myAnimator = GetComponent<Animator>();
        if (!resourceDrops.Contains(myResource)) {
            resourceDrops.Add(myResource);
        }
    }

    public void OnMouseOver()
    {
        if(type == Equippable.CanGather.WoodGather)
        {
            Cursor.SetCursor(gm.axeCursor, Vector2.zero, CursorMode.Auto);
        }
        else if (type == Equippable.CanGather.StonesGather)
        {
            Cursor.SetCursor(gm.pickCursor, Vector2.zero, CursorMode.Auto);
        }
        else if (type == Equippable.CanGather.HandGather)
        {
            Cursor.SetCursor(gm.handCursor, Vector2.zero, CursorMode.Auto);
        }
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void Harvest(Gather g) {

        SoundPlayer sp = GetComponent<SoundPlayer>();


        if (Inventory.itemInHand != null && Inventory.itemInHand.myGathering == type && canGather) {

            if(sp != null)
            {
                sp.ResourcePlay();

            }
            if (toughness > 0) {
                toughness--;

                if(myAnimator != null)
                {
                    myAnimator.SetTrigger("Harvest");
                }
            } else {
                g.use = false;
                g.beingUsed = false;
                g.waiting = false;

                int itemsInInv = 0;
                foreach (Slot s in Inventory.Instance.theInventory)
                {

                    if (s.myItem == null)
                    {
                        foreach (Item i in resourceDrops)
                        {
                            Inventory.Instance.AddItem(i); //Un comment when there is inventory in scene pl0x


                        }

                        break;
                    }
                    else
                    {
                        itemsInInv++;
                    }

                }

                if (itemsInInv >= Inventory.Instance.theInventory.Count) {
                    ObjectPooler.instance.GetFromPool(myResource.itemName, transform.position, Quaternion.Euler(new Vector3())); //No Place Chosen Yet
                    //Drop resource on floor
                }
                if (!berries) {
                    if(myAnimator != null)
                    {
                        myAnimator.SetBool("Stop", true);
                    }
                } else {
                    berryChild.SetActive(false);
                    if (!isGrowing) {
                        StartCoroutine(RegrowBerries());
                        canGather = false;
                    }
                }
            }
        }

    }

    public void DestroyResource() {
        Destroy(gameObject);
    }

    private IEnumerator RegrowBerries() {
        isGrowing = true;
        yield return new WaitForSeconds(berryGrowTime);
        berryChild.SetActive(true);
        isGrowing = false;
        canGather = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StoreManager : MonoBehaviour {

    private bool canBePlaced;
    bool placeableTop, placeableRight, placeableLeft;

    public GameObject JumpPrefab, SprintPrefab, CrouchPrefab, ReversePrefab, WallJumpPrefab;

    public Tilemap levelTiles;

    private Vector3 mousePos;

    private SpriteRenderer newActionRenderer;
    private BoxCollider2D newActionCollider;
    private GameObject newAction;
    private string newActionType;

    [HideInInspector]
    public int jumpPrice, sprintPrice, crouchPrice, reversePrice, wallJumpPrice;

    [HideInInspector]
    public bool placingAction;


    void Start () {
        placingAction = false;
        newAction = null;

        jumpPrice = GameManagerScript.initialJumpPrice;
        sprintPrice = GameManagerScript.initialSprintPrice;
        crouchPrice = GameManagerScript.initialCrouchPrice;
        reversePrice = GameManagerScript.initialCrouchPrice;
	}
	
	void Update () {
        //Debug.Log(placingAction);
		

        if (placingAction && newAction)
        {
            FindOrPlace();
        }



	}

    public void SelectAction(string type)
    {
        switch (type)
        {
            case "Jump":
                if (GameManagerScript.PlayerMoney >= jumpPrice)
                {
                    BuyAction(JumpPrefab);
                    GameManagerScript.PlayerMoney -= jumpPrice;
                }
                break;

            case "Sprint":
                if (GameManagerScript.PlayerMoney >= sprintPrice)
                {
                    BuyAction(SprintPrefab);
                    GameManagerScript.PlayerMoney -= sprintPrice;
                }
                break;

            case "Crouch":
                if (GameManagerScript.PlayerMoney >= crouchPrice)
                {
                    BuyAction(CrouchPrefab);
                    GameManagerScript.PlayerMoney -= crouchPrice;
                }
                break;

            case "Reverse":
                if (GameManagerScript.PlayerMoney >= reversePrice)
                {
                    BuyAction(ReversePrefab);
                    GameManagerScript.PlayerMoney -= reversePrice;
                }
                break;

            case "WallJump":
                if (GameManagerScript.PlayerMoney >= wallJumpPrice)
                {
                    BuyAction(WallJumpPrefab);
                    GameManagerScript.PlayerMoney -= wallJumpPrice;
                }
                break;
        }
    }

    void BuyAction(GameObject prefab)
    {
        if (!placingAction)
        {
            newAction = Instantiate(prefab, mousePos, Quaternion.identity);
            newActionRenderer = newAction.GetComponent<SpriteRenderer>();
            newActionCollider = newAction.GetComponent<BoxCollider2D>();

            newActionCollider.enabled = false;
            canBePlaced = false;

            placingAction = true;
        }
    }
    void FindOrPlace()
    {
        canBePlaced = false;
        placeableRight = false;
        placeableLeft = false;
        placeableTop = false;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);

        Vector3Int mousePosInt = new Vector3Int((int)Mathf.Floor(mousePos.x), (int)Mathf.Floor(mousePos.y), (int)Mathf.Floor(mousePos.z));

        Vector3Int selectedCellPos = levelTiles.WorldToCell(mousePosInt);

        if (levelTiles.GetTile(selectedCellPos) != null)
        {
            if (levelTiles.GetTile(new Vector3Int(selectedCellPos.x + 1, selectedCellPos.y, 0)) == null && levelTiles.GetTile(new Vector3Int(selectedCellPos.x + 2, selectedCellPos.y, 0)) == null)
            {
                placeableRight = true;
            }
            else if (levelTiles.GetTile(new Vector3Int(selectedCellPos.x - 1, selectedCellPos.y, 0)) == null && levelTiles.GetTile(new Vector3Int(selectedCellPos.x - 2, selectedCellPos.y, 0)) == null)
            {
                placeableLeft = true;
            }
            else if (levelTiles.GetTile(new Vector3Int(selectedCellPos.x, selectedCellPos.y + 1, 0)) == null && levelTiles.GetTile(new Vector3Int(selectedCellPos.x, selectedCellPos.y + 2, 0)) == null)
            {
                placeableTop = true;
            }
        }

        if (placeableTop || placeableRight || placeableLeft)
        {
            if(newActionType != "WallJump" || (newActionType == "WallJump" && !placeableTop))
            {
                canBePlaced = true;
                ChangeColor("canPlace");
            }
            else
            {
                canBePlaced = false;
                ChangeColor("cannotPlace");
            }

        }
        else
        {
            canBePlaced = false;
            ChangeColor("cannotPlace");
        }


        if (!canBePlaced)
        {
            newAction.transform.position = mousePos;
        }

        else
        {
            newAction.transform.position = levelTiles.GetCellCenterWorld(selectedCellPos);

            if (Input.GetButtonDown("Fire1"))
            {
                //Debug.Log("Placed action and can be placed: " + canBePlaced);
                placingAction = false;
                ChangeColor("placed");
                newActionCollider.enabled = true;

                GameManagerScript.actions.Add(newAction.transform);

                newAction = null;
                newActionCollider = null;
                newActionRenderer = null;
            }
        }
    }
    void ChangeColor(string type)
    {
        Color tmp = newActionRenderer.color;

        switch (type)
        {
            case "cannotPlace":
                tmp.r = 1f;
                tmp.g = 0f;
                tmp.b = 0f;
                tmp.a = 75f / 255;
                break;

            case "canPlace":
                tmp.r = 0f;
                tmp.g = 1f;
                tmp.b = 0f;
                tmp.a = 75f / 255;
                break;

            case "placed":
                tmp.r = 1f;
                tmp.g = 1f;
                tmp.b = 1f;
                tmp.a = 1f;
                break;
        }

        newActionRenderer.color = tmp;

    }
}

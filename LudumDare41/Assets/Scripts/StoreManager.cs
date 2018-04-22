﻿using System.Collections;
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

    public int initialJumpPrice, initialSprintPrice, initialCrouchPrice, initialReversePrice, initialWallJumpPrice;

    public float initialPriceIncrement;


    [HideInInspector]
    public bool placingAction;


    void Start () {
        placingAction = false;
        newAction = null;

        GameManagerScript.jumpPrice = initialJumpPrice;
        GameManagerScript.sprintPrice = initialSprintPrice;
        GameManagerScript.crouchPrice = initialCrouchPrice;
        GameManagerScript.reversePrice = initialReversePrice;
        GameManagerScript.wallJumpPrice = initialWallJumpPrice;
        GameManagerScript.priceIncrement = initialPriceIncrement;
	}
	
	void Update () {

        if (placingAction)
        {
            if(newAction)
            FindOrPlace();
        }

        else
        {
            FindOrDelete();
        }
	}

    public void SelectAction(string type)
    {
        switch (type)
        {
            case "Jump":
                if (GameManagerScript.PlayerMoney >= GameManagerScript.jumpPrice)
                {
                    BuyAction(JumpPrefab);
                    GameManagerScript.PlayerMoney -= GameManagerScript.jumpPrice;
                }
                break;

            case "Sprint":
                if (GameManagerScript.PlayerMoney >= GameManagerScript.sprintPrice)
                {
                    BuyAction(SprintPrefab);
                    GameManagerScript.PlayerMoney -= GameManagerScript.sprintPrice;
                }
                break;

            case "Crouch":
                if (GameManagerScript.PlayerMoney >= GameManagerScript.crouchPrice)
                {
                    BuyAction(CrouchPrefab);
                    GameManagerScript.PlayerMoney -= GameManagerScript.crouchPrice;
                }
                break;

            case "Reverse":
                if (GameManagerScript.PlayerMoney >= GameManagerScript.reversePrice)
                {
                    BuyAction(ReversePrefab);
                    GameManagerScript.PlayerMoney -= GameManagerScript.reversePrice;
                }
                break;

            case "WallJump":
                if (GameManagerScript.PlayerMoney >= GameManagerScript.wallJumpPrice)
                {
                    BuyAction(WallJumpPrefab);
                    GameManagerScript.PlayerMoney -= GameManagerScript.wallJumpPrice;
                }
                break;
        }
        if (placingAction)
        {
			GameManagerScript.shop.Play ();
            newActionType = type;
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

            if (levelTiles.GetTile(new Vector3Int(selectedCellPos.x, selectedCellPos.y + 1, 0)) == null && levelTiles.GetTile(new Vector3Int(selectedCellPos.x, selectedCellPos.y + 2, 0)) == null)
            {
                placeableTop = true;
            }
        }

        //Debug.Log("Action type"+ newActionType);
        if (placeableTop) 
        {
            if(newActionType != "WallJump")
            {
                //Debug.Log("Can place top");
                canBePlaced = true;
                ChangeColor("canPlace");
            }
            else
            {
                if (placeableRight || placeableLeft)
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



        }
        else if (placeableRight || placeableLeft)
        {
            if (newActionType != "WallJump")
            {
                canBePlaced = false;
                ChangeColor("cannotPlace");
            }

            else
            {
                canBePlaced = true;
                ChangeColor("canPlace");
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

                switch(newActionType)
                {

                }
                newAction = null;
                newActionCollider = null;
                newActionRenderer = null;
                newActionType = null;
            }
        }
    }

    void FindOrDelete()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(hit.collider != null)
            {
                if(hit.collider.tag == "Jump" || hit.collider.tag == "Sprint" || hit.collider.tag == "Reverse" || hit.collider.tag == "WallJump" || hit.collider.tag == "Crouch")
                {
                    if(GameManagerScript.actions.Contains(hit.collider.transform))
                    {
                        GameManagerScript.actions.Remove(hit.collider.transform);
                        Destroy(hit.collider.gameObject);
                    }
                }
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

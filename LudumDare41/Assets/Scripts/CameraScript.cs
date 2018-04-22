using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float dragSpeed = 2;
    private float actualDragSpeed;


    private Vector3 dragOrigin;
    public bool lockOnPlayer = false;
    public bool cameraDragging = true;

    public float outerLeft = -10f;
    public float outerRight = 10f;
    public float outerTop, outerDown;


    void LateUpdate()
    {
        if (!lockOnPlayer)
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            float left = Screen.width * 0.2f;
            float right = Screen.width - (Screen.width * 0.2f);

            if (mousePosition.x < left || mousePosition.x > right)
            {
                cameraDragging = true;
                actualDragSpeed = dragSpeed;
            }
				
            if (mousePosition.x < left)
            {
                cameraDragging = true;
            }
            else if (mousePosition.x > right)
            {
                cameraDragging = true;
            }

            if (cameraDragging)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    dragOrigin = Input.mousePosition;
                    return;
                }

                if (!Input.GetMouseButton(0))
                    return;

              Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
              Vector3 moveX = new Vector3(pos.x * dragSpeed, 0, 0);

                if (moveX.x > 0f)
                {
                    if (this.transform.position.x < outerRight)
                    {
                        transform.Translate(moveX, Space.World);
                    }
                }
                else
                {
                    if (this.transform.position.x > outerLeft)
                    {
                        transform.Translate(moveX, Space.World);
                    }
                }
                Vector3 moveY = new Vector3(0, pos.y * dragSpeed, 0);
                if (moveY.y > 0f)
                {
                    if (this.transform.position.y < outerTop)
                    {
                        transform.Translate(moveY, Space.World);
                    }
                }
                else
                {
                    if (this.transform.position.y > outerDown)
                    {
                        transform.Translate(moveY, Space.World);
                    }
                }
            }

        }

        else
        {
			if (this.transform.position.x > outerRight)
			{
				this.transform.position = new Vector3 (transform.position.x, GameManagerScript.player.transform.position.y, -10f);
			}
			if (this.transform.position.x < outerLeft)
			{
				this.transform.position = new Vector3 (transform.position.x, GameManagerScript.player.transform.position.y, -10f);
			}

			if (this.transform.position.y > outerTop)
			{
				this.transform.position = new Vector3 (GameManagerScript.player.transform.position.x, transform.position.y, -10f);
			}
			if (this.transform.position.y < outerTop)
			{
				this.transform.position = new Vector3 (GameManagerScript.player.transform.position.x, transform.position.y, -10f);
			}

            //this.transform.position = new Vector3(GameManagerScript.player.transform.position.x, GameManagerScript.player.transform.position.y, -10f);

			if (cameraDragging)
			{

				if (Input.GetMouseButtonDown(0))
				{
					dragOrigin = Input.mousePosition;
					return;
				}
				Debug.Log(actualDragSpeed);
				if (!Input.GetMouseButton(0)) return;

				Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
				Vector3 move = new Vector3(pos.x * actualDragSpeed, 0, 0);
			}
          
        }

    }
}
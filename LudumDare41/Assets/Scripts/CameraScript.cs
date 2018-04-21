using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public float dragSpeed = 2;
    private float actualDragSpeed;
  

    private Vector3 dragOrigin;
    public bool lockOnPlayer = false;
    public bool cameraDragging = true;

    public float outerLeft = -10f;
    public float outerRight = 10f;


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

                if (move.x > 0f)
                {
                    if (this.transform.position.x < outerRight)
                    {
                        transform.Translate(move, Space.World);
                    }
                }
                else
                {
                    if (this.transform.position.x > outerLeft)
                    {
                        transform.Translate(move, Space.World);
                    }
                }
            }
        }

       
    }


}
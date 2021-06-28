using System.Collections.Generic;
using UnityEngine;

public class BoxItem : MonoBehaviour
{
    public List<Vector2Int> configs;

    private bool moving;

    private float startPosX;
    private float startPosY;

    public Vector3 resetPosition;

    LoadAgain load;

    private void Update()
    {
        Moving();
    }

    private void Moving()
    {
        if (moving)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = transform.parent.InverseTransformPoint(mousePos);

            gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0f);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;

            mousePos = Input.mousePosition;
            mousePos = transform.parent.InverseTransformPoint(mousePos);

            startPosX = mousePos.x - transform.localPosition.x;
            startPosY = mousePos.y - transform.localPosition.y;

            moving = true;
        }
    }

    private void OnMouseUp()
    {
        moving = false;
        GameManager.Instance.BoxMoveDone(this);
        
    }

    public void ResetPosition()
    {
        transform.localPosition = resetPosition  ;
    }    
}

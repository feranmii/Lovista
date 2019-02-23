using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EyeVision : MonoBehaviour
{
    public LineRenderer lr;
    public SpriteMask mask;
    private Camera camera1;

    private void Start()
    {
        camera1 = Camera.main;
        lr.positionCount  = 2;
    }

    public void Update()
    {
        Vector2 point = camera1.ScreenToWorldPoint(Input.mousePosition);
        lr.SetPosition(0, lr.transform.position);
        lr.SetPosition(1, transform.TransformDirection(mask.transform.position));
        mask.transform.position = point;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = camera1.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D =new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector3.zero);

            if (hit.collider != null) 
            {
                print(hit.collider.gameObject);
            }
        }
    }

    public void CheckForHeart()
    {
        
    }

   
    
}

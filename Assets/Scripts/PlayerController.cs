using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float moveSpeed;
    
    private bool isMoving;
    private Vector2 input;
    
    //Stores previous key pressed
    private Vector2 prevKey;
    string Key = "";
    
    // Update is called once per frame
    void Update()
    {
        //Get input axis
        if(!isMoving){
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            
            
            //Checking input and storing as previous key
            if(input.x != prevKey.x){
                Key = "X";
                prevKey.x = input.x;
            }
            if (input.y != prevKey.y)
            {
                Key = "Y";
                prevKey.y = input.y;
            }
        
            if(input.x != 0 && input.y == 0){
                Key = "X";
            }
            if (input.x == 0 && input.y != 0)
            {
                Key = "Y";
            }
            
            
            //Movement will switch based on the latest button pressed, diagonal movement is removed
            if(Key == "X"){
                input.y = 0;                
            } 
            else if(Key == "Y"){
                input.x = 0;
            }
        
            //Moves character with coroutine
            if (input != Vector2.zero){
                Vector3 targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;
                
                StartCoroutine(Move(targetPos));
            }
        }
        
        
    }
    
    IEnumerator Move(Vector3 targetPos){
        
        isMoving = true;
        
        while ((targetPos-transform.position).sqrMagnitude > Mathf.Epsilon){
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 enum MoveDirection {UP, DOWN, LEFT, RIGHT }

public class SnakeController : MonoBehaviour {

    public float defaultTicTime;
    public float ticTimeLimit;
    public Vector3 tempPos;
    
    private float currentTiccTime;
    private bool isAlive;
    private bool isBoosted;
    [SerializeField]
    private bool isReversed;

    private MoveDirection moveDirection = MoveDirection.UP;
    [SerializeField]
    private List<GameObject> snakeBody;

    
    [SerializeField]
    private GameObject bodyPrefab;




    // Use this for initialization
    void Start()
    {
        InitializeSnake();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Inputs();
            UpdateSnakePosition();
        }

    }
    public void ExtendSnake()
    {
         AddBodyElement();
    }

    public void ShortenSnake()
    {
        if (snakeBody.Count > 1)
        {
            GameObject bodyElement;
            if (!isReversed)
            {
                bodyElement = snakeBody[snakeBody.Count - 1];
            }
            else
            {
                bodyElement = snakeBody[1];
            }
            snakeBody.Remove(bodyElement);
            BoosterController.instance.RemoveElementTransFormToList(bodyElement.transform);
            Destroy(bodyElement);
        }
    }
    public void SetBoostSpeed(float boostedSpeed)
    {
        if (isBoosted)
        {
            StopCoroutine("ReturnBasicSpeed");
            ticTimeLimit = defaultTicTime;
        }
        ticTimeLimit = boostedSpeed;
        StartCoroutine("ReturnBasicSpeed");
    }
    public void ReverseSnake()
    {
        Reverse();
    }
    private void Inputs()
    {
        if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(moveDirection!= MoveDirection.DOWN)
            {
                moveDirection = MoveDirection.UP;
                currentTiccTime = ticTimeLimit;
            }
        }
        if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (moveDirection != MoveDirection.UP)
            {
                moveDirection = MoveDirection.DOWN;
                currentTiccTime = ticTimeLimit;
            }
        }
        if (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (moveDirection != MoveDirection.RIGHT)
            {
                moveDirection = MoveDirection.LEFT;
                currentTiccTime = ticTimeLimit;
            }
        }
        if (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (moveDirection != MoveDirection.LEFT)
            {
                moveDirection = MoveDirection.RIGHT;
                currentTiccTime = ticTimeLimit;
            }
        }
    }

    
    private void UpdateSnakePosition()
    {
        currentTiccTime += 1 * Time.deltaTime;
        if(currentTiccTime>= ticTimeLimit)
        {
            if (!isReversed)
            {
                for (int j = snakeBody.Count - 1; j > 0; j--)
                {
                    snakeBody[j].transform.position = snakeBody[j - 1].transform.position;
                }
            }
            else
            {
                for (int j = 1 ; j < snakeBody.Count - 1; j++)
                {
                    snakeBody[j].transform.position = snakeBody[j + 1].transform.position;
                }
                snakeBody[snakeBody.Count-1].transform.position = snakeBody[0].transform.position;
            }
            
            switch (moveDirection)
            {
                case (MoveDirection.UP):
                    tempPos.z++;
                    break;
                case (MoveDirection.DOWN):
                    tempPos.z--;
                    break;
                case (MoveDirection.LEFT):
                    tempPos.x--;
                    break;
                case (MoveDirection.RIGHT):
                    tempPos.x++;
                    break;
            }
            currentTiccTime = 0;
        }
        snakeBody[0].transform.position = tempPos;
    }
    private void AddBodyElement()
    {
        GameObject part = Instantiate(bodyPrefab, transform.parent) as GameObject;
        part.name = "Body";
        part.transform.position = snakeBody[snakeBody.Count - 1].transform.position;
        snakeBody.Add(part);
        BoosterController.instance.AddActiveElementTransformToList(part.transform);
        

    }
    private void InitializeSnake()
    {
        ticTimeLimit = defaultTicTime;
        isReversed = false;
        isAlive = true;
        isBoosted = false;
        
    }
    private void Reverse()
    {
        Vector3 tailPos;
        if (!isReversed)
        {
            tailPos = snakeBody[snakeBody.Count - 1].transform.position;
            for (int j = snakeBody.Count - 1; j > 0; j--)
            {
                snakeBody[j].transform.position = snakeBody[j - 1].transform.position;
            }
            snakeBody[0].transform.position = tailPos;
            tempPos = tailPos;
            isReversed = true;
        }
        else
        {
            tailPos = snakeBody[1].transform.position;
            for (int j = 1; j < snakeBody.Count - 1; j++)
            {
                snakeBody[j].transform.position = snakeBody[j + 1].transform.position;
            }
            snakeBody[snakeBody.Count - 1].transform.position = snakeBody[0].transform.position;
            tempPos = tailPos;
            snakeBody[0].transform.position = tailPos;
            isReversed = false;
        }
        ReverseDirection();

    }
    private void ReverseDirection()
    {
        switch (moveDirection)
        {
            case (MoveDirection.UP):
                moveDirection = MoveDirection.DOWN;
                break;
            case (MoveDirection.DOWN):
                moveDirection = MoveDirection.UP;
                break;
            case (MoveDirection.LEFT):
                moveDirection = MoveDirection.RIGHT;
                break;
            case (MoveDirection.RIGHT):
                moveDirection = MoveDirection.LEFT;
                break;
        }
    }
    IEnumerator ReturnBasicSpeed()
    {
        isBoosted = true;
        yield return new WaitForSeconds(10f);
        ticTimeLimit = defaultTicTime;
        isBoosted = false;
    }
}

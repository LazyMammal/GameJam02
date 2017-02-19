using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YugeWall : MonoBehaviour, CommandInterface
{
    public GameObject block_prefab;
    public Transform wallHeadTrigger, wallTailTrigger;
    public int maxHeight = 10;
    public int maxLength = 150;
    private int[] wallHeight;
    private GameObject[,] blockRefs;
    private int wallHead = 0, wallTail = 0, wallLength = 0;
    private bool isPaused = true;

    void Start()
    {
        wallHeight = new int[maxLength];
        blockRefs = new GameObject[maxLength, maxHeight];
    }

    public void DoCommand()
    {
        // start building wall
        AddBlockAtHead();
        isPaused = false;
    }

    void Update()
    {
        if (!isPaused)
        {
            // check HEAD trigger position
            if (wallHeadTrigger)
            {
                float xPos = wallHeadTrigger.transform.position.x;
                while (xPos > GetHeadTransform().position.x)
                {
                    AddBlockAtHead();
                }
            }
            // check HEAD trigger position
            if (wallTailTrigger)
            {
                float xPos = wallTailTrigger.transform.position.x;
                while (xPos > GetTailTransform().position.x)
                {
                    EraseColumnAtTail();
                }
            }
        }
    }

    Transform GetHeadTransform()
    {
        Transform tr = transform;
        if (wallLength >= 0 && wallHead >= 0 && wallHead < maxLength)
        {
            GameObject block = blockRefs[wallHead, 0];
            if (block && block.activeSelf) tr = block.transform;
        }

        return tr;
    }

    Transform GetTailTransform()
    {
        Transform tr = transform;
        if (wallLength >= 0 && wallTail >= 0 && wallTail < maxLength)
        {
            GameObject block = blockRefs[wallTail, 0];
            if (block && block.activeSelf) tr = block.transform;
        }

        return tr;
    }

    void CreateBlock(int x, int y, Vector3 pos, Quaternion rot)
    {
        // re-use block if exists
        GameObject block = blockRefs[x, y];
        if (block == null)
        {
            block = (GameObject)Instantiate(block_prefab, pos, rot);
        }
        else
        {
            block.transform.position = pos;
            block.transform.rotation = rot;
        }
        block.SetActive(true);
        block.transform.SetParent(transform);

        // store array index location in block itself
        YugeWallBlock b = block.GetComponent<YugeWallBlock>();
        b.x = x;
        b.y = y;

        // keep reference to block for later re-use
        blockRefs[x, y] = block;
    }

    void EraseBlock(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < maxLength && y < maxLength)
        {
            // get block reference
            GameObject block = blockRefs[x, y];

            if (block)
            {
                // hide block but don't destroy the object
                block.SetActive(false);
            }
        }
    }

    void EraseColumn(int x)
    {
        // erase blocks but don't adjust wall length
        for (int y = 0; y < maxHeight; y++)
        {
            EraseBlock(x, y);
        }
    }

    void EraseColumnAtTail()
    {
        if (wallLength > 0)
        {
            EraseColumn(wallTail);
            wallTail = (wallTail + 1) % maxLength;
            wallLength--;
        }
    }
    void AddBlockAtHead()
    {
        Transform head = GetHeadTransform();

        if (wallLength > 0)
        {
            wallHead = (wallHead + 1) % maxLength;
            if (wallHead == wallTail)
            {
                EraseColumnAtTail();
            }
        }
        CreateBlock(wallHead, 0, head.position + transform.right, head.rotation);
        wallLength++;
    }
}


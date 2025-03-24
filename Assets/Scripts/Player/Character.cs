using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private PlayerMovement playerMovement;

    public float interactionRayLength = 5;

    public LayerMask groundMask;

    public BlockType selecteBlockInInventory = BlockType.Nothing;
    public bool fly = false;

    bool isWaiting = false;

    public World world;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        world = FindObjectOfType<World>();
    }

    private void Start()
    {
        playerInput.OnMouseClick += HandleMouseClick;
        playerInput.OnFly += HandleFlyClick;
    }

    private void HandleFlyClick()
    {
        fly = !fly;
    }

    void Update()
    {
        if (fly)
        {
            playerMovement.Fly(playerInput.MovementInput, playerInput.IsJumping, playerInput.RunningPressed);

        }
        else
        {
            if (playerMovement.IsGrounded && playerInput.IsJumping && isWaiting == false)
            {
                isWaiting = true;
                StopAllCoroutines();
                StartCoroutine(ResetWaiting());
            }
            playerMovement.HandleGravity(playerInput.IsJumping);
            playerMovement.Walk(playerInput.MovementInput, playerInput.RunningPressed);
        }

    }
    IEnumerator ResetWaiting()
    {
        yield return new WaitForSeconds(0.1f);
        isWaiting = false;
    }

    private void HandleMouseClick()
    {
        Ray playerRay = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(playerRay, out hit, interactionRayLength, groundMask))
        {
            ModifyTerrain(hit);
        }

    }

    private void ModifyTerrain(RaycastHit hit)
    {
        if (selecteBlockInInventory == BlockType.Nothing)
        {
            world.SetBlock(hit, BlockType.Air);
        }
    }
}

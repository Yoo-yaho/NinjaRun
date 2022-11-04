using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float gravity;
    public Vector2 velocity;
    public float maxXVelocity = 100.0f;
    public float maxAcceleration = 10.0f;
    public float acceleration = 10.0f;
    public float distance = 0.0f;
    public float jumpVelocity = 20.0f;
    public float groundHeight = 10.0f;
    public bool isGrounded = false;
    public bool isHoldingJump = false;
    public bool isDead = false;
    public bool isHit = false;
    public float maxHoldJumpTime = 0.4f; // 최대 점프 지속 가능한 시간
    public float maxMaxHoldJumpTime = 0.4f;
    public float holdTime = 0.0f; // 점프 지속 시간
    public float jumpGroundThreshold = 1f; // 점프 가능한 y좌표의 한계점
    public float runSpeed;
    public float maxRunSpeed;
    public float skillMana;
    public float skillRegen;

    public LayerMask groundLayerMask;
    public LayerMask obstacleLayerMask;
    public SpriteRenderer spriteRenderer;

    public Animator animator;
    public Image skillBar;

    GroundFall fall;
    CameraController cameraController;

    private void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        Vector2 pos = this.transform.position;
        // Mathf.Abs(절댓값을 반환할 수)
        float groundDistance = Mathf.Abs(pos.y - groundHeight);

        if (isGrounded || groundDistance <= jumpGroundThreshold)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                isHoldingJump = true;
                velocity.y = jumpVelocity;
                holdTime = 0.0f;
                animator.SetBool("isGrounded", isGrounded);

                if (fall != null)
                {
                    fall.player = null;
                    fall = null;
                    cameraController.StopShaking();
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
            animator.SetTrigger("isFall");
        }
        
        if (Input.GetKey(KeyCode.Z))
        {
            this.gameObject.tag = "Unbeatable";
            this.gameObject.layer = 8;
            spriteRenderer.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
            skillBar.fillAmount -= skillMana * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            this.gameObject.tag = "Player";
            spriteRenderer.color = new Color(1, 1, 1);
        }

        skillBar.fillAmount += skillRegen * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Vector2 pos = this.transform.position;      

        if (runSpeed < maxRunSpeed)
        {
            runSpeed += 0.0005f;
            animator.SetFloat("RunSpeed", runSpeed);
        }

        if (isDead)
        {
            return;
        }

        if (pos.y < -20)
        {
            isDead = true;
        }

        if (!isGrounded)
        {
            if (isHoldingJump)
            {
                holdTime += Time.fixedDeltaTime;

                if (holdTime >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }

            pos.y += velocity.y * Time.fixedDeltaTime;
            
            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y);
            Vector2 rayDir = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDir, rayDistance, groundLayerMask);

            if (hit2D.collider != null)
            {
                Ground ground = hit2D.collider.GetComponent<Ground>();

                if (ground != null)
                {
                    if (pos.y >= ground.groundHeight)
                    {
                        groundHeight = ground.groundHeight;
                        pos.y = groundHeight;
                        //velocity.y = 0;

                        isGrounded = true;
                        animator.SetBool("isGrounded", isGrounded);
                    }

                    fall = ground.GetComponent<GroundFall>();

                    if (fall != null)
                    {
                        fall.player = this;
                        cameraController.StartShaking();
                    }
                }
            }

            Debug.DrawRay(rayOrigin, rayDir * rayDistance, Color.red);

            Vector2 wallOrigin = new Vector2(pos.x, pos.y);
            RaycastHit2D wallHit = Physics2D.Raycast(wallOrigin, Vector2.right,
                velocity.x * Time.fixedDeltaTime, groundLayerMask);

            if (wallHit.collider != null)
            {
                Ground ground = wallHit.collider.GetComponent<Ground>();

                if (ground != null)
                {
                    if(pos.y < ground.groundHeight)
                    {
                        velocity.x = 0;
                    }
                }
            }
        }

        distance += velocity.x * Time.fixedDeltaTime;

        if (isGrounded)
        {
            float velocityRatio = velocity.x / maxXVelocity;

            acceleration = maxAcceleration * (1 - velocityRatio);
            maxHoldJumpTime = maxMaxHoldJumpTime * velocityRatio;
            velocity.x += acceleration * Time.fixedDeltaTime;
                        
            if (velocity.x >= maxXVelocity)
            {
                velocity.x = maxXVelocity;
            }

            Vector2 rayOrigin = new Vector2(pos.x - 0.7f, pos.y);
            Vector2 rayDir = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;

            if (fall != null)
            {
                rayDistance = -fall.fallSpeed * Time.fixedDeltaTime;
            }

            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDir, rayDistance);

            if (hit2D.collider == null)
            {
                isGrounded = false;
            }

            Debug.DrawRay(rayOrigin, rayDir * rayDistance, Color.blue);
        }

        Vector2 obstacleOrigin = new Vector2(pos.x, pos.y);
        RaycastHit2D obstacleHitX = Physics2D.Raycast(obstacleOrigin, Vector2.right,
            velocity.x * Time.fixedDeltaTime, obstacleLayerMask);

        if(obstacleHitX.collider != null)
        {
            Obstacle obstacle = obstacleHitX.collider.GetComponent<Obstacle>();

            if (obstacle != null)
            {
                HitObstacle(obstacle);
            }
        }

        RaycastHit2D obstacleHitY = Physics2D.Raycast(obstacleOrigin, Vector2.up,
            velocity.y * Time.fixedDeltaTime, obstacleLayerMask);

        if (obstacleHitY.collider != null)
        {
            Obstacle obstacle = obstacleHitY.collider.GetComponent<Obstacle>();

           if (obstacle != null)
           {            
                HitObstacle(obstacle);
            }
        }

        if (obstacleHitY.collider == null)
        {
            isHit = false;
        }
           
        this.transform.position = pos;
    }

    void HitObstacle(Obstacle obstacle)
    {
        Destroy(obstacle.gameObject);
        velocity.x *= 0.5f;
        runSpeed *= 0.7f;
        animator.SetFloat("RunSpeed", runSpeed);
        isHit = true;
    }
}

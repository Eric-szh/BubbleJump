using UnityEngine;

public class InputBinding : MonoBehaviour
{
    public GameObject tail;
    private float MouthCD = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<CharacterController2D>().isDead)
        {
            return;
        }
        float horizontal = Input.GetAxis("Horizontal");
        bool jump = Input.GetButtonDown("Jump");
        bool down = Input.GetButton("Down");
        // Debug.Log("Horizontal: " + horizontal + " Jump: " + jump + " Down: " + down);
        GetComponent<CharacterController2D>().Move(horizontal, jump, down);

        bool isAiming = Input.GetButton("Fire1") || Input.GetButton("Fire2");
        tail.GetComponent<TailCtrler>().isAiming = isAiming;

        bool shoot = Input.GetButtonDown("Shoot");
        if (shoot)
        {
            GetComponent<CharacterSkillControler>().Shoot();
            MouthCD = 0.5f;
        }

        bool skill1 = Input.GetButtonUp("Fire1");
        if (skill1)
        {
            GetComponent<CharacterSkillControler>().Skill1();
        }
        bool skill2 = Input.GetButtonUp("Fire2");
        if (skill2)
        {
            GetComponent<CharacterSkillControler>().Skill2();
        }
        bool skill3 = Input.GetButtonUp("Fire3");
        if (skill3)
        {
            GetComponent<CharacterSkillControler>().Skill3();
        }
        bool charge = Input.GetButton("Charge");  
        if (charge)
        {
            GetComponent<CharacterController2D>().Charge();
        }

        MouthCD -= Time.deltaTime;
        if (MouthCD <= 0)
        {
            tail.GetComponent<TailCtrler>().isShooting = false;
        } else
        {
            tail.GetComponent<TailCtrler>().isShooting = true;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CharacterSkillControler : MonoBehaviour
{
    public float shootCD = 0.5f;
    private float shootCDTimer;
    private bool shootReady = true;
    public GameObject bulletPrefab;
    public Transform leftBulletPoint;
    public Transform rightBulletPoint;
    public float skill1CD = 0.5f;
    private float skill1CDTimer;
    private bool skill1Ready = true;
    public GameObject skill1ParticlePrefab;
    public Transform skill1StartingPoint;
    public GameObject currentParticle;
    public float skill2CD = 0.5f;
    private float skill2CDTimer;
    private bool skill2Ready = true;
    public GameObject skill2BubblePrefab;
    public Transform skill2StartingPoint;
    public float skill3CD = 0.5f;
    private float skill3CDTimer;
    private bool skill3Ready = true;
    public GameObject skill3Shield;
    public float skill3ShieldDuration = 5f;
    public bool skill3canMegaDrop;
    public bool skill3Activated;
    public float skill3DamageRadius = 5f;
    public List<bool> skillState = new List<bool> { false, false, false };

    public void SetSkillState(int index, bool state)
    {
        skillState[index] = state;
    }

    public void Shoot()
    {
        if (!shootReady)
        {
            return;
        }
        shootReady = false;
        shootCDTimer = 0;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector3 mouseDir = MouseUtil.getMouseDirection(transform.position);
        bool isLeft = mouseDir.x < 0;
        if (isLeft)
        {
            bullet.transform.position = leftBulletPoint.position;
        }
        else
        {
            bullet.transform.position = rightBulletPoint.position;
        }
        AudioManager.Instance.PlaySound(35, 0.5f);
        bullet.GetComponent<BulletCtrl>().isLeft = isLeft;
    }


    public void Skill1()
    {
        if (!skill1Ready)
        {
            return;
        }
        if (!skillState[0] && !GetComponent<CharacterController2D>().creative)
        {
            return;
        }
        skill1Ready = false;
        skill1CDTimer = 0;
        Destroy(currentParticle);
        Vector3 mousePosition2d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition2d - transform.position;
        direction.z = 0;
        direction.Normalize();
        Quaternion rotation = Quaternion.FromToRotation(Vector3.right, direction);
        GameObject particle = Instantiate(skill1ParticlePrefab, skill1StartingPoint.position, rotation);
        currentParticle = particle;
        AudioManager.Instance.PlaySound(1, 0.5f);
    }

    public void Skill2()
    {
        if (!skill2Ready)
        {
            return;
        }
        if (!skillState[1] && !GetComponent<CharacterController2D>().creative)
        {
            return;
        }
        skill2Ready = false;
        skill2CDTimer = 0;
        Instantiate(skill2BubblePrefab, skill2StartingPoint.position, Quaternion.identity);
        AudioManager.Instance.PlaySound(2, 0.5f);
    }

    public void Skill3()
    {
        if (!skill3Ready)
        {
            return;
        }
        if (!skillState[2] && !GetComponent<CharacterController2D>().creative)
        {
            return;
        }
        skill3Ready = false;
        skill3CDTimer = 0;
        skill3Shield.GetComponent<ShieldCtrl>().GainShield();
        skill3Activated = true;
        skill3canMegaDrop = true;
        Invoke("Skill3End", skill3ShieldDuration);  
        AudioManager.Instance.PlaySound(4, 0.5f);
    }

    public void Skill3Damage()
    {
        // damage all enemies in the radius tagged as Enemy
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, skill3DamageRadius, 1 << LayerMask.NameToLayer("Enemy"));
        foreach (Collider2D collider in colliders)
        {
            MonsterUtil monster = collider.GetComponent<MonsterUtil>();
            if (monster != null)
            {
                monster.Damage(1);
            }
        }
    }

    public void Skill3Blocked()
    {
        skill3canMegaDrop = false;
        skill3Activated = false;
        CancelInvoke("Skill3End");
        skill3Shield.GetComponent<ShieldCtrl>().LoseShield();
    }

    public void Skill3End()
    {
        skill3Shield.GetComponent<ShieldCtrl>().LoseShield();
        skill3Activated = false;
        skill3canMegaDrop = false;
    }

    private void Update()
    {
        if (!shootReady)
        {
            shootCDTimer += Time.deltaTime;
            if (shootCDTimer >= shootCD)
            {
                shootReady = true;
            }
        }

        if (!skill1Ready)
        {
            skill1CDTimer += Time.deltaTime;
            if (skill1CDTimer >= skill1CD)
            {
                skill1Ready = true;
            }
        }

        if (!skill2Ready)
        {
            skill2CDTimer += Time.deltaTime;
            if (skill2CDTimer >= skill2CD)
            {
                skill2Ready = true;
            }
        }

        if (!skill3Ready)
        {
            skill3CDTimer += Time.deltaTime;
            if (skill3CDTimer >= skill3CD)
            {
                skill3Ready = true;
            }
        }
    }
}

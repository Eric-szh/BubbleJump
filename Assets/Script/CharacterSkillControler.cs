using UnityEngine;

public class CharacterSkillControler : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform leftBulletPoint;
    public Transform rightBulletPoint;
    public GameObject skill1ParticlePrefab;
    public Transform skill1StartingPoint;
    public GameObject skill2BubblePrefab;
    public Transform skill2StartingPoint;

    public void Shoot()
    {
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
        bullet.GetComponent<BulletCtrl>().isLeft = isLeft;
    }


    public void Skill1()
    {
        SkillController.Instance.RemoveFoam();
        Vector3 mousePosition2d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition2d - transform.position;
        direction.z = 0;
        direction.Normalize();
        Quaternion rotation = Quaternion.FromToRotation(Vector3.right, direction);
        Instantiate(skill1ParticlePrefab, skill1StartingPoint.position, rotation);
    }

    public void Skill2()
    {
        Instantiate(skill2BubblePrefab, skill2StartingPoint.position, Quaternion.identity);
    }
}

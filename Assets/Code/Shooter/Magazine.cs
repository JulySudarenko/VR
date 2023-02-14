using UnityEngine;

namespace Code.Shooter
{
    public class Magazine : MonoBehaviour
    {
        public WeaponType.magazType typeOfMagazine = WeaponType.magazType.pistol;
        public int ammo = 30;

        [HideInInspector] public bool isOpen = true;
        [HideInInspector] public Rigidbody MyRg;
        [HideInInspector] public Collider myCol;
        [HideInInspector] public GameObject Bullet; // ссылка на бутофорную пулю

        private void Start()
        {
            MyRg = GetComponent<Rigidbody>();
            myCol = GetComponent<Collider>();

            if (transform.GetChild(0)) Bullet = transform.GetChild(0).gameObject;
        }
    }
}

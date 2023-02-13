using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;

namespace GeekBrains
{
    public class WeaponSocket : Socket // ДЗ
    {       
        private Gun myGun;

        protected override void FixedUpdate()
        {
            if (myGun != null) return;

            base.FixedUpdate();
        }

        public override void PlaceObject(GameObject go)
        {
            myGun = go.GetComponent<Gun>();

            if (myGun.TypeOfGun == _typeOfMagazine)
            {
                MyInteractable interactable = go.GetComponent<MyInteractable>();
                interactable.DetachFromHand();
                interactable.onPickUp.AddListener(TakeOffObject);
                myGun.GetComponent<Rigidbody>().isKinematic = true;

                go.transform.SetParent(gameObject.transform);

                if (smoothAttach) StartCoroutine(Attaching(go));
                else
                {
                    go.transform.localRotation = Quaternion.identity;
                    go.transform.localPosition = Vector3.zero;
                }
            }
        }

        protected override void TakeOffObject()
        {
            myGun.GetComponent<MyInteractable>().onPickUp.RemoveListener(TakeOffObject);
            _lastMagazine = myGun.gameObject;
            myGun = null;
        }

        public override void Release()
        {
            if (myGun == null) return;

            myGun.transform.SetParent(null);
            myGun.GetComponent<Rigidbody>().isKinematic = false;
            TakeOffObject();
        }
    }
}
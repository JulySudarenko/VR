using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;

namespace GeekBrains
{
    public class Socket : MonoBehaviour
    {
        [SerializeField] protected WeaponType.magazType _typeOfMagazine = WeaponType.magazType.pistol;

        [SerializeField] protected float radius;
        [SerializeField] protected LayerMask mask;

        [SerializeField] protected bool smoothAttach;
        [SerializeField] protected float smoothSpeed;

        [HideInInspector] public Magazine mag;

        protected GameObject _lastMagazine;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        protected virtual void FixedUpdate()
        {
            if (mag != null) return;
            
            if(_lastMagazine != null)
            {
                Collider[] lastCols = Physics.OverlapSphere(transform.position, radius + 0.2f, mask);
                bool lastInSphere = false;
                for (int i = 0; i < lastCols.Length; i++)
                {
                    if (lastCols[i].gameObject == _lastMagazine)
                    {
                        lastInSphere = true;
                        break;
                    }
                }
                if (!lastInSphere) _lastMagazine = null;
            }

            Collider[] cols = Physics.OverlapSphere(transform.position, radius, mask);

            for(int i = 0; i < cols.Length; i++)
            {
                if(cols[i].gameObject != _lastMagazine)
                {
                    _lastMagazine = null;
                    PlaceObject(cols[i].gameObject);
                    break;
                }
            }
        }

        public virtual void PlaceObject(GameObject go)
        {            
            Magazine goMag = go.GetComponent<Magazine>();

            if (goMag.isOpen && goMag.typeOfMagazine == _typeOfMagazine)
            {
                mag = goMag;

                MyInteractable interactable = go.GetComponent<MyInteractable>();
                interactable.DetachFromHand();
                interactable.onPickUp.AddListener(TakeOffObject);
                mag.MyRg.isKinematic = true;
                mag.isOpen = false;
                mag.myCol.isTrigger = true;

                go.transform.SetParent(gameObject.transform);

                if (smoothAttach) StartCoroutine(Attaching(go));
                else
                {
                    go.transform.localRotation = Quaternion.identity;
                    go.transform.localPosition = Vector3.zero;
                }
            }
        }

        protected virtual void TakeOffObject()
        {            
            //mag.transform.SetParent(null);
            mag.isOpen = true;
            mag.myCol.isTrigger = false; 
            mag.GetComponent<MyInteractable>().onPickUp.RemoveListener(TakeOffObject);
            _lastMagazine = mag.gameObject;
            mag = null;
        }

        public virtual void Release()
        {
            if (mag == null) return;

            mag.transform.SetParent(null);
            mag.MyRg.isKinematic = false;
            TakeOffObject();
        }

        protected IEnumerator Attaching(GameObject go)
        {
            while (true)
            {
                if (go.gameObject == mag.gameObject)
                {
                    go.transform.position = Vector3.Lerp(go.transform.position, transform.position, smoothSpeed);
                    go.transform.rotation = Quaternion.Lerp(go.transform.rotation, transform.rotation, smoothSpeed);

                    if (transform.position == go.transform.position
                        && transform.rotation == go.transform.rotation) yield break;

                    yield return new WaitForFixedUpdate();
                }
                else yield break;
            }
        }
    }
}
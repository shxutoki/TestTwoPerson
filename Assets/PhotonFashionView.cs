namespace Photon.Pun
{
    using UnityEngine;
    using System.Collections.Generic;
    public class PhotonFashionView : MonoBehaviourPun, IPunObservable
    {

        public bool m_SynchronizePosition = true;

        public List<GameObject> fasions_object;
        public List<bool> fasions_vaild;

        bool m_firstTake = false;

        public void Awake()
        {

        }

        private void Reset()
        {
        }

        void OnEnable()
        {

        }

        public void Update()
        {
            int fasion_num = fasions_object.Count;

            for (int fasion_id = 0; fasion_id < fasion_num; fasion_id++)
            {
                fasions_object[fasion_id].SetActive(fasions_vaild[fasion_id]);
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            // Write
            if (stream.IsWriting)
            {
                if (this.m_SynchronizePosition)
                {
                    int fasion_num = fasions_object.Count;
                    for (int fasion_id = 0; fasion_id < fasion_num; fasion_id++)
                    {
                        stream.SendNext(fasions_vaild[fasion_id]);
                    }

                }

            }
            // Read
            else
            {
                if (this.m_SynchronizePosition)
                {
                    int fasion_num = fasions_object.Count;
                    for (int fasion_id = 0; fasion_id < fasion_num; fasion_id++)
                    {
                        bool m_Networ_fasions_vaild = (bool)stream.ReceiveNext();
                        fasions_vaild[fasion_id] = m_Networ_fasions_vaild;
                    }

                }

            }
        }
    }
}
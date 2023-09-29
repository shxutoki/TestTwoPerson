namespace Photon.Pun
{
    using UnityEngine;
    using System.Collections.Generic;
    public class PhotonFashionView : MonoBehaviourPun, IPunObservable
    {

        public bool m_SynchronizeSize = true;
        public bool m_SynchronizeTex = true;

        public int tex_Num = 0;

        public List<GameObject> fasions_object;
        public List<Texture> TshirtTex;
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
                if (this.m_SynchronizeSize)
                {
                    int fasion_num = fasions_object.Count;
                    for (int fasion_id = 0; fasion_id < fasion_num; fasion_id++)
                    {
                        stream.SendNext(fasions_vaild[fasion_id]);
                    }

                }

                if (this.m_SynchronizeTex)
                {
                    stream.SendNext(tex_Num);
                }

            }
            // Read
            else
            {
                if (this.m_SynchronizeSize)
                {
                    int fasion_num = fasions_object.Count;
                    for (int fasion_id = 0; fasion_id < fasion_num; fasion_id++)
                    {
                        bool m_Networ_fasions_vaild = (bool)stream.ReceiveNext();
                        fasions_vaild[fasion_id] = m_Networ_fasions_vaild;
                    }

                }

                if (this.m_SynchronizeTex)
                {
                    tex_Num = (int)stream.ReceiveNext();
                    foreach (GameObject obj in this.fasions_object)
                    {
                        obj.GetComponent<Renderer>().material.mainTexture = TshirtTex[tex_Num];
                    }
                }

            }
        }
    }
}
namespace Photon.Pun
{
    using UnityEngine;
    using System.Collections.Generic;
    public class PhotonFashionViewNew : MonoBehaviourPun, IPunObservable
    {

        public bool m_SynchronizeSize = true;
        public bool m_SynchronizeTex = true;
        public bool m_SynchronizeMotion = true;
        public bool m_SynchronizeIP = true;

        public int motion = 0;
        public bool changeclothes = false;

        public int tex_Num = 0;

        [SerializeField]
        List<GameObject> ModelSizes;

        [SerializeField]
        public List<GameObject> ModelTshirts;

        [SerializeField]
        public List<bool> fasions_vaild;

        bool m_firstTake = false;

        public string myip;
        public int myport;
        public string partnerip;
        public int partnerport;

        public GameObject GazeSender;
        public GameObject GazeReceiver;

        public void Awake()
        {
            //Load all the Fitted Models, set M size as default.
            int ChildrenNum = this.transform.childCount;
            for (int i = 0; i < ChildrenNum; i++)
            {
                GameObject model = this.transform.GetChild(i).gameObject;
                ModelSizes.Add(model);
                ModelTshirts.Add(model.transform.Find("Tshirt").gameObject);

                bool valid = false;
                if (i == 2)
                {
                    valid = true;
                }
                fasions_vaild.Add(valid);
            }

        }

        private void Reset()
        {
        }

        void OnEnable()
        {

        }

        public void Update()
        {
            int fasion_num = ModelSizes.Count;

            for (int fasion_id = 0; fasion_id < fasion_num; fasion_id++)
            {
                ModelSizes[fasion_id].SetActive(fasions_vaild[fasion_id]);
            }
            foreach (GameObject obj in this.ModelSizes)
            {
                obj.GetComponent<Animator>().SetInteger("motion", motion);
                obj.GetComponent<Animator>().SetBool("changeclothes", changeclothes);
            }
            if ((GazeSender == null) || (GazeReceiver == null))
            {
                if (GameObject.Find("GazeSharing"))
                {
                    GazeSender = GameObject.FindObjectOfType<GazeSend>().gameObject;
                }
                if (GameObject.Find("GazeSharing"))
                {
                    GazeReceiver = GameObject.FindObjectOfType<GazeReceive>().gameObject;
                }
                
            }
            if ((GazeSender != null) && (GazeReceiver != null))
            {
                GazeSender.GetComponent<GazeSend>().SetPartnerIp(partnerip, partnerport);
                GazeReceiver.GetComponent<GazeReceive>().SetReceivePort(myport);
            }
            
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            // Write
            if (stream.IsWriting)
            {
                if (this.m_SynchronizeSize)
                {
                    int fasion_num = ModelSizes.Count;
                    for (int fasion_id = 0; fasion_id < fasion_num; fasion_id++)
                    {
                        stream.SendNext(fasions_vaild[fasion_id]);
                    }

                }

                if (this.m_SynchronizeTex)
                {
                    stream.SendNext(tex_Num);
                }

                if (this.m_SynchronizeMotion)
                {
                    stream.SendNext(motion);
                    stream.SendNext(changeclothes);
                }

                if (this.m_SynchronizeIP)
                {
                    stream.SendNext(myip);
                    stream.SendNext(myport);
                    stream.SendNext(partnerport);
                }

            }
            // Read
            else
            {
                if (this.m_SynchronizeSize)
                {
                    int fasion_num = ModelSizes.Count;
                    for (int fasion_id = 0; fasion_id < fasion_num; fasion_id++)
                    {
                        bool m_Networ_fasions_vaild = (bool)stream.ReceiveNext();
                        fasions_vaild[fasion_id] = m_Networ_fasions_vaild;
                    }

                }

                if (this.m_SynchronizeTex)
                {
                    tex_Num = (int)stream.ReceiveNext();
                    foreach (GameObject obj in this.ModelTshirts)
                    {
                        obj.GetComponent<Renderer>().material.mainTexture = GameObject.Find("GenerateTshirtCollection").GetComponent<GenerateTshirtCollection>().TshirtIMGs[tex_Num];
                    }
                }

                if (this.m_SynchronizeMotion)
                {
                    motion = (int)stream.ReceiveNext();
                    changeclothes = (bool)stream.ReceiveNext();

                }

                if (this.m_SynchronizeIP)
                {
                    partnerip = (string)stream.ReceiveNext();
                    partnerport = (int)stream.ReceiveNext();
                    myport = (int)stream.ReceiveNext();
                }

            }
        }
    }
}
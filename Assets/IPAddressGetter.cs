using System.Net;
using UnityEngine;
using Photon.Pun;


public class IPAddressGetter : MonoBehaviour
{
    public string localIP;
    public string MyIP;
    public int MyPort;
    public string PartIP;
    public int PartPort;
    void Start()
    {
        localIP = GetLocalIPAddress();
        Debug.Log("Local IP Address: " + localIP);
        transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = localIP;
    }

    private void Update()
    {
        PhotonFashionViewNew[] photonFashionViewNews;
        photonFashionViewNews = GameObject.FindObjectsOfType<PhotonFashionViewNew>();
        foreach (PhotonFashionViewNew fashionviewnew in photonFashionViewNews)
        {
            if (fashionviewnew.gameObject.GetComponent<PhotonView>().IsMine)
            {
                fashionviewnew.myip = localIP;
                PartIP = fashionviewnew.partnerip;
                PartPort = fashionviewnew.partnerport;
                MyPort = fashionviewnew.myport;
                MyIP = fashionviewnew.myip;
            }
        }
        transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = localIP + ": " + MyPort.ToString();
        transform.GetChild(1).gameObject.GetComponent<TextMesh>().text = "Partner Address: " + PartIP + ": " + PartPort.ToString();
    }

    string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("Local IP Address Not Found!");
    }
}

using UnityEngine;

public class mynetwork : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public int connections = 10;
    public int listenProt = 8899;
    public bool useNat = false;
    public string ip = "127.0.0.1";
    public GameObject playerprefab;
    void OnGUI()
    {
        //判断是否存在服务器
        if (Network.peerType == NetworkPeerType.Disconnected)
        {

            if (GUILayout.Button("创建服务器"))
            {
                //进行创建服务器操作
                //Network.incomingPassword = "Tcj";
                //bool useNat = !Network.HavePublicAddress();
                NetworkConnectionError error = Network.InitializeServer(connections, listenProt, useNat);
                print(error);

            }
            if (GUILayout.Button("连接服务器"))
            {
                //进行连接服务器操作
                NetworkConnectionError error = Network.Connect(ip, listenProt);
                print(error);
            }
        }
        else if (Network.peerType == NetworkPeerType.Server)
        {
            GUILayout.Label("服务器创建完成");
        }
        else if (Network.peerType == NetworkPeerType.Client)
        {
            GUILayout.Label("客户端已接入");
        }
    }
    //注意:下面两个方法为服务器调用
    void OnServerInitialized()
    {
        print("Server 初始化完成!");
        var group = int.Parse(Network.player + "");//直接访问NetWork.plyer会得到当期客户端
        Network.Instantiate(playerprefab, new Vector3(0, 10, 0), Quaternion.identity, group);
    }
    void OnPlayerConnected(NetworkPlayer player)
    {
        print("一个客户端接入,IndexNum:" + player);
    }


    void OnConnectedToServer()
    {
        print("已成功接入服务器!");
        var group = int.Parse(Network.player + "");//直接访问NetWork.plyer会得到当期客户端
        Network.Instantiate(playerprefab, new Vector3(0, 10, 0), Quaternion.identity, group);
    }
    //netwok view 组件用来在局域网内同步一个游戏物体的数据
    //只同步自己创建的物体而无法同步其他客户端同步过来的物体数据
}

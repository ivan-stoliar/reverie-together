using UnityEngine;
using FishNet;
using FishNet.Transporting;

public enum ConnectionType
{
    Host,
    Client
}


public class Connectionhandler : MonoBehaviour
{

    public ConnectionType connectionType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
#if UNITY_EDITOR
    private void OnDisable()
    {
        InstanceFinder.ClientManager.OnClientConnectionState -= onClientConnectionState;
    }


    private void OnEnable()
    {
        InstanceFinder.ClientManager.OnClientConnectionState += onClientConnectionState;
    }

    private void onClientConnectionState(ClientConnectionStateArgs args)
    {
        if(args.ConnectionState == LocalConnectionState.Stopping)
        {
            UnityEditor.EditorApplication.isPlaying = false;

        }
    }
#endif
    void Start()
    {

        #if UNITY_EDITOR
        if(ParrelSync.ClonesManager.IsClone())
        {
            InstanceFinder.ClientManager.StartConnection();


        }
        

        else
        {

            if (connectionType == ConnectionType.Host) {

                InstanceFinder.ServerManager.StartConnection();
                InstanceFinder.ClientManager.StartConnection();

            }
            else
            {
                InstanceFinder.ClientManager.StartConnection();

            }


        }
#endif

#if DEDICATED_SERVER
 InstanceFinder.ServerManager.StartConnection();
#endif

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}

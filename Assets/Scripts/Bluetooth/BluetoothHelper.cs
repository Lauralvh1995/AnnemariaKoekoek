using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluetooth {

    public class BluetoothHelper
    {
        const string pluginName = "com.growthmoves.bluetoothmanager.BluetoothPlugin";

        static AndroidJavaClass pluginClass;
        static AndroidJavaObject pluginInstance;


        [System.Serializable]
        public class BtConnection
        {
            public string name;
            public string address;
            public string distance;
            public bool accurate;
            public string updateRate;


            public override string ToString()
            {
                return "\n\nName: " + name + " \n Address: " + address + " \n Distance: " + distance + "M \n Accurate: " + accurate + " \n Rate: " + updateRate;
            }
        }

        [System.Serializable]
        public class BtConnections
        {
            public List<BtConnection> connections = new List<BtConnection>();

            public override string ToString()
            {
                string connectionsString = "";

                foreach (BtConnection conn in connections)
                {
                    connectionsString += conn.ToString();
                }

                return connectionsString;
            }
        }

        public BluetoothHelper()
        {
            pluginClass = new AndroidJavaClass(pluginName);
            pluginInstance = pluginClass.CallStatic<AndroidJavaObject>("getInstance");
        }

        public double GetElapsedTime()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                return pluginInstance.Call<int>("getElapsedTime");
            }
            Debug.LogWarning("Wrong platform");
            return 0;
        }

        public bool GetBluetoothEnabled()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                return pluginInstance.Call<bool>("getBluetoothEnabled");
            }
            Debug.LogWarning("Wrong platform");
            return false;
        }

        public string StartAdvertising()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                return pluginInstance.Call<string>("startAdvertising");
            }
            Debug.LogWarning("Wrong platform");
            return "";
        }

        public BtConnections GetDiscoveredBluetoothDevices()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                string result = pluginInstance.Call<string>("getDiscoveredBluetoothDevices");
                BtConnections connections = JsonUtility.FromJson<BtConnections>(result);

                return connections;
        }
        Debug.LogWarning("Wrong platform");
            return new BtConnections();
        }

        public BtConnection GetBluetoothDeviceByID(string id)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                BtConnection connection;
                string result = pluginInstance.Call<string>("getDiscoveredBluetoothDeviceByID", id);
                if (!result.Equals("{}"))
                {
                    connection = JsonUtility.FromJson<BtConnection>(result);
                }
                else return null;
                return connection;

        }
        Debug.LogWarning("Wrong platform");
            return null;
        }



    }
}

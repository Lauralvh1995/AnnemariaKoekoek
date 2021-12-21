using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace Bluetooth
{

    public struct Advertiser
    {
        public Advertiser(string ID, AdvertiserType type)
        {
            this.ID = ID;
            this.type = type;
        }

        public string ID;
        public AdvertiserType type;
    }

    public enum AdvertiserType
    {
        Start,
        Finish
    }

    public class BTManager : MonoBehaviourPun
    {
        private BluetoothHelper bluetoothHelper;
        private float elapsedTime = 0;
        private bool monitoring;
        private bool advertising;
        private List<Advertiser> advertisers = new List<Advertiser>();

        private Color originalBackgroundColor;
        BluetoothHelper.BtConnections connectionObject;
        BluetoothHelper.BtConnection activeConnection;

        [SerializeField] Image background;
        [SerializeField] GameObject popupDialog;
        [SerializeField] GameObject scrollableContent;
        [SerializeField] TMP_Text debugText;
        [SerializeField] GameObject deviceButtonPrefab;

        private Dictionary<string, GameObject> connectionButtons = new Dictionary<string, GameObject>();

        private void Start()
        {
            bluetoothHelper = new BluetoothHelper();
        }

        public void StartAdvertising(AdvertiserType type)
        {
            advertising = true;
            StartCoroutine(AdvertisingRoutine());
            debugText.gameObject.SetActive(true);
        }

        public void StopAdvertising()
        {
            advertising = false;
            debugText.gameObject.SetActive(false);
        }

        public void StartMonitoring()
        {
            monitoring = true;

            originalBackgroundColor = Color.white;
            scrollableContent.SetActive(true);
            debugText.gameObject.SetActive(true);

            StartCoroutine(MonitorRoutine());
        }

        public void StopMonitoring()
        {

            monitoring = false;
            background.color = Color.white;
            scrollableContent.SetActive(false);
            activeConnection = null;
            debugText.gameObject.SetActive(false);
        }

        private IEnumerator MonitorRoutine()
        {
            while (monitoring)
            {
                elapsedTime += Time.deltaTime;


                if (elapsedTime >= 1f)
                {
                    connectionObject = GetAdvertisers();
                    if (connectionObject != null)
                    {
                        string scrollableText =
                            "Bluetooth enabled: " + bluetoothHelper.GetBluetoothEnabled();

                        foreach (BluetoothHelper.BtConnection conn in connectionObject.connections)
                        {
                            if (!connectionButtons.ContainsKey(conn.address))
                            {

                                GameObject btn = Instantiate(deviceButtonPrefab, scrollableContent.GetComponentInChildren<ContentSizeFitter>().gameObject.transform);
                                btn.GetComponentInChildren<TMP_Text>().fontWeight = FontWeight.Bold;
                                btn.GetComponentInChildren<TMP_Text>().text = conn.name.ToString();
                                btn.GetComponent<Button>().onClick.AddListener(OnBtnClick);

                                void OnBtnClick()
                                {
                                    Handheld.Vibrate();
                                    activeConnection = conn;
                                    scrollableContent.SetActive(false);
                                    debugText.text = conn.ToString();
                                    monitoring = false;

                                };

                                connectionButtons.Add(conn.address, btn);
                            }
                        }

                        debugText.text = scrollableText;

                    }
                    elapsedTime = 0;
                    yield return null;
                }
            }
        }

        private IEnumerator AdvertisingRoutine()
        {
            while (advertising)
            {
                elapsedTime += Time.deltaTime;


                if (elapsedTime >= 1f)
                {
                    string advertiserId = bluetoothHelper.StartAdvertising();
                    debugText.text = advertiserId;
                    // TODO: Add support for both start and finish types
                    photonView.RPC("ReceiveIDHost", RpcTarget.MasterClient, advertiserId, AdvertiserType.Start);
                }
                yield return null;
            }
        }

        private BluetoothHelper.BtConnections GetAdvertisers()
        {
            // TODO: Get all advertiser IDs from other players.

            BluetoothHelper.BtConnections connectionsObject = new BluetoothHelper.BtConnections();

            foreach (Advertiser advertiser in advertisers)
            {
                BluetoothHelper.BtConnection connection = bluetoothHelper.GetBluetoothDeviceByID(advertiser.ID);
                if (connection != null) connectionsObject.connections.Add(connection);
            }

            return connectionsObject;
        }

        public bool IsAdvertising()
        {
            return advertising;
        }

        public bool IsMonitoring()
        {
            return monitoring;
        }

        [PunRPC]
        private void ReceiveIDHost(string ID, AdvertiserType type)
        {
            Advertiser newAdvertiser = new Advertiser(ID, type);
            if (!advertisers.Contains(newAdvertiser))
            {
                advertisers.Add(newAdvertiser);

                string[] IDs = new string[advertisers.Count];
                AdvertiserType[] types = new AdvertiserType[advertisers.Count];

                for (int i = 0; i < advertisers.Count; i++)
                {
                    IDs[i] = advertisers[i].ID;
                    types[i] = advertisers[i].type;
                }

                photonView.RPC("ReceiveIDsClient", RpcTarget.Others, ID, types);
            }
        }

        [PunRPC]
        private void ReceiveIDsClient(string[] IDs, AdvertiserType[] types)
        {
            List<Advertiser> newAdvertisers = new List<Advertiser>();
            for (int i = 0; i < IDs.Length; i++)
            {
                newAdvertisers.Add(new Advertiser(IDs[i], types[i]));
            }

            foreach (Advertiser a in newAdvertisers)
            {
                if (!advertisers.Contains(a))
                {
                    advertisers.Add(a);
                }
            }
        }
    }
}

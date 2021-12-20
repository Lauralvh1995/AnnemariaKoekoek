using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bluetooth {
    public class BTManager : MonoBehaviour
    {
        private BluetoothHelper bluetoothHelper;
        private float elapsedTime = 0;
        private bool monitoring;
        private bool advertising;
        private List<string> advertiserIds = new List<string>();

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

        public void StartAdvertising()
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
                    // TODO: Share ID with players.
                }
                yield return null;
            }
        }

        private BluetoothHelper.BtConnections GetAdvertisers()
        {
            // TODO: Get all advertiser IDs from other players.

            BluetoothHelper.BtConnections connectionsObject = new BluetoothHelper.BtConnections();

            foreach(string id in advertiserIds)
            {
                BluetoothHelper.BtConnection connection = bluetoothHelper.GetBluetoothDeviceByID(id);
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
    }
}

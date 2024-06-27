using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ServerTimeChecker : MonoBehaviour
{
    [SerializeField] private GameObject[] switchObjects;

    // Start is called before the first frame update
    void Start()
    {
        using (WebClient wc = new WebClient())
        {
            var json = wc.DownloadString("https://yandex.com/time/sync.json?geo=213");

            var result = JObject.Parse(json);
            var date = new DateTime(1970, 1, 1).AddMilliseconds(result.Property("time").Value.ToObject<long>());
            if(date > new DateTime(2024, 7, 2))
            {
                switchObjects[0].gameObject.SetActive(true);
            }
            else
            {
                switchObjects[1].gameObject.SetActive(true);
            }
        }
    }
}

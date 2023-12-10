using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }
    private readonly string channelId = "MyChannel";

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        AndroidNotificationChannel channel = new()
        {
            Id = channelId,
            Name = "Reminders",
            Importance = Importance.Default,
            Description = "Reminder notifications to play the game"
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            AndroidNotification notification = new()
            {
                Title = "The customers miss you!",
                Text = "Come and make more smoothies!",
                FireTime = DateTime.Now.AddSeconds(30),
                LargeIcon = "smoothie_logo"
            };
            AndroidNotificationCenter.SendNotification(notification, channelId);
        }
    }

}

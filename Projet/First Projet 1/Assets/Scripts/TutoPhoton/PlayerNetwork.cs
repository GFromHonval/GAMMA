﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : Photon.MonoBehaviour
{

    [SerializeField] private GameObject PlayerCamera;
    [SerializeField] private Animation playeranimation;
    
    private PhotonView PhotonView;
    private int IsInScene;

    public int GetIsInScene
    {
        get { return IsInScene; }
        set { IsInScene = value; }
    }
    
    private void Start()
    {
        PhotonView = GetComponent<PhotonView>();
        playeranimation = GetComponent<Animation>();
        Initialize();
    }

    private void Initialize()
    {
        if (PhotonView.isMine)
        {
            
        }
        //disable non-local shits
        else
        {
            //camera 
            PlayerCamera.SetActive(false);
        }
    }
} 




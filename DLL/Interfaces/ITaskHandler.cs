﻿//  *****************************************************************************
//  File:       ITaskHandler.cs
//  Solution:   ORM-Monitor
//  Project:    ORM-Monitor
//  Date:       12/13/2016
//  Author:     Latency McLaughlin
//  Copywrite:  Bio-Hazard Industries - 1998-2016
//  *****************************************************************************

namespace ORM_Monitor.Interfaces {
  public interface ITaskHandler {
    bool IsSubscribed { get; }
    void Invoke();
  }
}
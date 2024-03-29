﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace PlayerDataInput
{
   public class DataStorage
   {
      string _savePath = Application.persistentDataPath + "/playerData.data";
      BinaryFormatter _formatter = new BinaryFormatter();

      /// <summary>
      /// It appends a player data to the list of player data which was saved previously.
      /// If no list is available, a new one is created.
      /// </summary>
      /// <param name="playerData"></param>
      public void Save(DataStoragePlayerData playerData)
      {
         List<DataStoragePlayerData> playersData = File.Exists(_savePath) ? Load() : new List<DataStoragePlayerData>();

         playersData.Add(playerData);

         FileStream fileStream = new FileStream(_savePath, FileMode.Create);
         _formatter.Serialize(fileStream, playersData);
         fileStream.Close();
      }

      /// <summary>
      /// It updates an existing player data. Two players are the same if they have same name.
      /// It throws exeption if the player data doesn't exist.
      /// </summary>
      /// <param name="playerData"></param>
      public void Update(DataStoragePlayerData playerData)
      {
         if (!File.Exists(_savePath))
            throw new InvalidOperationException("playersData file not found");

         List<DataStoragePlayerData> playersData = Load();

         if (!playersData.Any(pd => pd.Details["Name"] == playerData.Details["Name"]))
            throw new InvalidOperationException("specified playerData not found");

         for (int i = 0; i < playersData.Count; i++)
            if (playersData[i].Details["Name"] == playerData.Details["Name"])
               playersData[i] = playerData;

         FileStream fileStream = new FileStream(_savePath, FileMode.Create);
         _formatter.Serialize(fileStream, playersData);
         fileStream.Close();
      }

      /// <summary>
      /// It loads the list of player data stored on the disc.
      /// It returns an empty list if no list is found.
      /// </summary>
      /// <returns></returns>
      public List<DataStoragePlayerData> Load()
      {
         List<DataStoragePlayerData> result;

         if (!File.Exists(_savePath))
            return new List<DataStoragePlayerData>();

         FileStream fileStream = new FileStream(_savePath, FileMode.Open);
         result = _formatter.Deserialize(fileStream) as List<DataStoragePlayerData>;
         fileStream.Close();

         return result;
      }
   }
}

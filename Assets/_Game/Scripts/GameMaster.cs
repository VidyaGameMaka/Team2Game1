using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Team2Game1 {
    public class GameMaster : MonoBehaviour {

        private static bool inited = false;

        //Scriptable Object Data holders
        public static GameData_SO gameData_SO;
        public static AudioClips_SO audioClip_SO;
        public static GlobalPrefabs_SO globalPrefabs_SO;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]       
        private static void Initialize() {
            if (inited) { return; }

            //Setup references to data holders
            gameData_SO = Resources.Load<GameData_SO>("resGame Data_SO");
            audioClip_SO = Resources.Load<AudioClips_SO>("resAudioClips_SO");
            globalPrefabs_SO = Resources.Load<GlobalPrefabs_SO>("resGlobalPrefabs_SO");

            //Instantiate Required prefabs
            Instantiate(globalPrefabs_SO.inputManager_GO);

            inited = true;
        }


        #region Save, Load Delete Game
        public static void SaveGame(int slotNum) {
            BinaryFormatter bf = new BinaryFormatter();
            string mySlotString = Application.persistentDataPath + "/gameData" + slotNum.ToString() + ".dat";
            FileStream file = File.Create(mySlotString);
            var json = SimpleEncryptDecrypt.EncryptDecrypt(JsonUtility.ToJson(gameData_SO));
            bf.Serialize(file, json);
            file.Close();
        }

        public static void LoadGame(int slotNum) {
            string mySlotString = Application.persistentDataPath + "/gameData" + slotNum.ToString() + ".dat";
            if (File.Exists(mySlotString)) {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(mySlotString, FileMode.Open);
                JsonUtility.FromJsonOverwrite(SimpleEncryptDecrypt.EncryptDecrypt((string)bf.Deserialize(file)), gameData_SO);
                file.Close();
            }
            gameData_SO.saveSlotNum = slotNum;
        }

        public static void DeleteGame(int slotNum) {
            try {
                File.Delete(Application.persistentDataPath + "/gameData" + slotNum.ToString() + ".dat");
            }
            catch (Exception ex) {
                Debug.LogException(ex);
            }
        }
        #endregion


    }
}
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


//Simple XOR "Encryption"
namespace Team2Game1 {
    public class SimpleEncryptDecrypt : MonoBehaviour {
        private static int szEncryptionKey = 152;

        public static string EncryptDecrypt(string szPlainText) {
            StringBuilder szInputStringBuild = new StringBuilder(szPlainText);
            StringBuilder szOutStringBuild = new StringBuilder(szPlainText.Length);
            char Textch;
            for (int iCount = 0; iCount < szPlainText.Length; iCount++) {
                Textch = szInputStringBuild[iCount];
                Textch = (char)(Textch ^ szEncryptionKey);
                szOutStringBuild.Append(Textch);
            }
            return szOutStringBuild.ToString();
        }
    }
}
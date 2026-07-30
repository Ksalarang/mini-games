using UnityEditor;
using UnityEngine;

namespace Core.Editor
{
    public static class PlayerPrefsTools
    {
        [MenuItem("Tools/PlayerPrefs/DeleteAll")]
        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}

using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace HutongGames.PlayMakerEditor
{
    /// <summary>
    /// Try to fix common update problems automatically
    /// This class does more in Unity 5
    /// </summary>
    [InitializeOnLoad]
    public class PlayMakerAutoUpdater
    {
        const string version = "1.7.8.3";

        // static constructor called on load
        static PlayMakerAutoUpdater()
        {
            if (ShouldUpdate())
            {
                // Can't call assetdatabase here, so use update callback
                EditorApplication.update -= RunAutoUpdate;
                EditorApplication.update += RunAutoUpdate;
            }
        }

        static bool ShouldUpdate()
        {
            if (string.IsNullOrEmpty(Application.dataPath)) return false;
            return (EditorPrefs.GetString("PlayMaker.NoUpdateReminderForProject", "") != GetUpdateSignature());
        }

        // Get a unique signature for this update to avoid repeatedly updating the same project
        // NOTE: might be a better way to do this. Currently doesn't catch project changes like imports...
        static string GetUpdateSignature()
        {
            return Application.unityVersion + "__" + Application.dataPath + "__" + version;
        }

        public static bool IsPlayMakerUnity5VersionImported()
        {
            // not a foolproof test, but good enough...

            return File.Exists(Application.dataPath+"/Plugins/PlayMaker/PlayMaker.dll");
        }

        public static void RunAutoUpdate()
        {
            //Debug.Log("PlayMaker AutoUpdater " + version);

#if (UNITY_5_0 || UNITY_5)

            if (!IsPlayMakerUnity5VersionImported())
            {
                if (!EditorUtility.DisplayDialog("PlayMaker",
                    "Please update PlayMaker for Unity 5 compatibility." +
                    "\nUpdate in the Unity 5 Asset Store, " +
                    "or download from the Hutong Games store.",
                    "OK", "Don't remind again"))
                {
                    EditorPrefs.SetString("PlayMaker.NoUpdateReminderForProject", GetUpdateSignature());
                }
            }
#endif

            EditorApplication.update -= RunAutoUpdate;
        }
    }
}
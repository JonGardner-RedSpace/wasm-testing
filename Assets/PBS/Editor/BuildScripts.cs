using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using System.Diagnostics;
using System.IO;
using System;
using System.Threading;

namespace PBS.Editor
{
    static class BuildScripts
    {

#if (UNITY_EDITOR_WIN)
        private const string NPM_PATH = @"C:\Program Files\nodejs\npm.cmd"; // <-- this should be added to a editor setting
#endif

        private const string PROCESS_ID_KEY = "springroll-buildtools-process-id";
        private const string INSTALLED_KEY = "springroll-buildtools-installed";

        private static string DEV_DIR = "Springroll-Seed";
        private static string DEV_STATIC => Path.Combine(DEV_DIR, "static");
        private static string BUILD_DIR => Path.Combine("Build", "WebGL");

        private static string ROOT_DIR => new DirectoryInfo(Application.dataPath).Parent.ToString();

        [MenuItem("Springroll/Watch _F5")]
        private static void BuildRunWatch()
        {
            BuildWebGL();
            CopyBuild();
            StartWatch();
        }

        [MenuItem("Springroll/Build/Debug")]
        private static void BuildDebug()
        {
            BuildWebGL();
            CopyBuild();
            StartNPM("run build:debug");
        }

        [MenuItem("Springroll/Build/Release")]
        private static void BuildRelease()
        {
            BuildWebGL();
            CopyBuild();
            StartNPM("run build:release");         
        }

        private static void BuildWebGL()
        {
            UnityEngine.Debug.ClearDeveloperConsole();

            BuildPlayerOptions options = new BuildPlayerOptions();
            options.target = BuildTarget.WebGL;
            options.targetGroup = BuildTargetGroup.WebGL;
            options.locationPathName = BUILD_DIR;
            BuildPipeline.BuildPlayer(options);
        }

        private static void CopyBuild()
        {
            DirectoryInfo source = new DirectoryInfo(Path.Combine(ROOT_DIR, BUILD_DIR));
            DirectoryInfo destination = new DirectoryInfo(Path.Combine(ROOT_DIR, DEV_STATIC));

            CopyAll(source, destination);
            UnityEngine.Debug.Log($"Copied {source} to {destination}");
        }

        private static void CopyAll(DirectoryInfo src, DirectoryInfo dest)
        {
            if (src.FullName.ToLower() == dest.FullName.ToLower())
            {
                return;
            }

            Directory.CreateDirectory(dest.FullName);
            
            foreach (FileInfo fileInfo in src.GetFiles())
            {
                fileInfo.CopyTo(Path.Combine(dest.ToString(), fileInfo.Name), true);
            }

            foreach (DirectoryInfo nextSrc in src.GetDirectories())
            {
                DirectoryInfo nextDest = dest.CreateSubdirectory(nextSrc.Name);
                CopyAll(nextSrc, nextDest);
            } 
        }

        private static void StartWatch()
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.WorkingDirectory = Path.Combine(ROOT_DIR, DEV_DIR);

            info.FileName = NPM_PATH;
            info.Arguments = $"run start";

            Process process;
            if (!tryGetProcess(EditorPrefs.GetInt(PROCESS_ID_KEY), out process))
            {
                process = new Process();
                process.StartInfo = info;
                process.Start();

                EditorPrefs.SetInt(PROCESS_ID_KEY, process.Id);
            }
        }

        private static bool tryGetProcess(int id, out Process process)
        {
            try
            {
                process = Process.GetProcessById(id);
            }
            catch
            {
                process = null;
                return false;
            }
            return true;
        }

        private static void StartNPM(string command)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.WorkingDirectory = Path.Combine(ROOT_DIR, DEV_DIR);

            info.FileName = NPM_PATH;
            info.Arguments = $"{command}";

            Process process = new Process();
            process.StartInfo = info;
            process.Start();
        }
    }
}

 

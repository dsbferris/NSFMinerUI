using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace NSFMinerUI
{
    public static class Overclock
    {

        public const string NVOClockPath = @"Executables\nvoclock-0.0.3-win64.exe";


        public static void SetMemoryClock(int offset)
        {
            if (offset > 1500) throw new Exception("Offset too big and not supported!");

            offset *= 1000; //We need offset in KHz for NVOClock

            using Process p = new Process();
            ProcessStartInfo psi = p.StartInfo;
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.FileName = NVOClockPath;
            psi.Arguments = "set pstate -c memory " + offset.ToString();
            //psi.RedirectStandardOutput = true;
            p.StartInfo = psi;
            //p.OutputDataReceived += P_OutputDataReceived;
            //p.Start();
            //p.BeginOutputReadLine();
            //p.Start();
            AdminProcessExecution.StartAsAdmin(p);
            p.WaitForExit();
        }

        public static void SetPowerlimit(int percent)
        {
            using Process p = new Process();
            ProcessStartInfo psi = p.StartInfo;
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.FileName = NVOClockPath;
            psi.Arguments = "set -P " + percent;
            psi.RedirectStandardOutput = true;
            p.StartInfo = psi;
            //AdminProcessExecution.StartAsAdmin(p);
            //p.OutputDataReceived += P_OutputDataReceived;
            p.Start();
            p.WaitForExit();
            //p.BeginOutputReadLine();
        }

        public static void ListGpus(DataReceivedEventHandler receivedEventHandler)
        {
            using Process p = new Process();
            ProcessStartInfo psi = p.StartInfo;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.UseShellExecute = false;
            psi.FileName = NVOClockPath;
            psi.Arguments = "list";
            psi.RedirectStandardOutput = true;
            p.StartInfo = psi;
            p.OutputDataReceived += receivedEventHandler;
            p.Start();
            p.BeginOutputReadLine();
            p.WaitForExit();
        }

        public static void GetGpuInfo(uint index, DataReceivedEventHandler receivedEventHandler)
        {
            using Process p = new Process();
            ProcessStartInfo psi = p.StartInfo;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.UseShellExecute = false;
            psi.FileName = NVOClockPath;
            //index = 1; //test for stdout or stderr
            psi.Arguments = " --gpu=" + index + " -O json info";
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            p.StartInfo = psi;
            p.OutputDataReceived += receivedEventHandler;
            p.ErrorDataReceived += receivedEventHandler;
            p.Start();
            p.BeginOutputReadLine();
            p.WaitForExit();
        }

        public static GpuInfo GpuInfoToClass(string info)
        {
            GpuInfo gpuInfo = new GpuInfo();
            var state = ReadingState.Inital;
            foreach (var line in info.Split("\n"))
            {
                switch (state)
                {
                    case ReadingState.Inital:
                        if (line.Contains("power_limits")) state = ReadingState.ReadingPowerlimit;
                        break;
                    case ReadingState.ReadingPowerlimit:
                        if (line.Contains("min")) gpuInfo.PowerPercent.Min = GetLineValue(line);
                        else if (line.Contains("max")) gpuInfo.PowerPercent.Max = GetLineValue(line);
                        else if (line.Contains("default"))
                        {
                            gpuInfo.PowerPercent.Default = GetLineValue(line);
                            state = ReadingState.DoneReadingPowerLimit;
                        }
                        break;
                    case ReadingState.DoneReadingPowerLimit:
                        if (line.Contains("vfp_limits")) state = ReadingState.PassedVFPLimits;
                        break;
                    case ReadingState.PassedVFPLimits:
                        if (line.Contains("Memory")) state = ReadingState.ReadingMemoryClocks;
                        break;
                    case ReadingState.ReadingMemoryClocks:
                        if (line.Contains("min")) gpuInfo.MemoryClockDelta.Min = GetLineValue(line) / 1000; //divide by 1000 because nvoclock reports in KHz not MHz
                        else if (line.Contains("max"))
                        {
                            gpuInfo.MemoryClockDelta.Max = GetLineValue(line) / 1000;
                            gpuInfo.MemoryClockDelta.Default = 0;
                            return gpuInfo;
                        }
                        break;

                }
            }
            //should not reach here
            return null;
        }

        private static int GetLineValue(string line)
        {
            int doublepointIndex = line.LastIndexOf(":");
            string lineAfter = line.Substring(doublepointIndex + 1); //+1 to remove ":"
            lineAfter = lineAfter.ToLower();
            lineAfter = lineAfter.Replace(",", string.Empty);
            lineAfter = lineAfter.Trim();
            var number = int.Parse(lineAfter);
            return number;
        }


        public static Process StartMonitor(DataReceivedEventHandler receivedEventHandler)
        {
            //sadly nvoclock status is broken
            //so we gotta be using nvida-smi
            //string smipath = GetSMIPath();
            string smipath = "nvidia-smi";
            Process p = new Process();
            p.StartInfo.FileName = smipath;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.Arguments = "dmon -s puc -o T";
            p.StartInfo.CreateNoWindow = true;
            p.OutputDataReceived += receivedEventHandler;
            p.Start();
            p.BeginOutputReadLine();
            return p;
        }
    }

    public enum ReadingState
    {
        Inital,
        ReadingPowerlimit,
        DoneReadingPowerLimit,
        PassedVFPLimits,
        ReadingMemoryClocks
    }

    public class GpuInfo
    {
        public MinMaxDefault PowerPercent = new MinMaxDefault();
        public MinMaxDefault MemoryClockDelta = new MinMaxDefault();

    }

    public class MinMaxDefault
    {
        public int Min;
        public int Max;
        public int Default;
    }
}

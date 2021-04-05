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

            using var p = new AdminProcess() { StartInfo = new ProcessStartInfo(NVOClockPath, "set pstate -c memory " + offset) };
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartAsAdminAndWait();
            p.WaitForExit();
        }

        public static void SetPowerlimit(int percent)
        {
            using Process p = new Process() { StartInfo = new ProcessStartInfo(NVOClockPath, "set -P " + percent) };
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.Start();
            p.WaitForExit();
        }

        public static void ListGpus(DataReceivedEventHandler receivedEventHandler)
        {
            using Process p = new Process() { StartInfo = new ProcessStartInfo(NVOClockPath, "list") };
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;

            p.StartInfo.RedirectStandardOutput = true;
            p.OutputDataReceived += receivedEventHandler;

            p.Start();
            p.BeginOutputReadLine();
            p.WaitForExit();
        }

        public static void GetGpuInfo(uint index, DataReceivedEventHandler receivedEventHandler)
        {
            using Process p = new Process() { StartInfo = new ProcessStartInfo(NVOClockPath, " --gpu=" + index + " -O json info") };
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;

            p.StartInfo.RedirectStandardOutput = true;
            p.OutputDataReceived += receivedEventHandler;

            p.StartInfo.RedirectStandardError = true;
            p.ErrorDataReceived += receivedEventHandler;

            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.WaitForExit();
        }

        public static GpuInfo GpuInfoStringToClass(string info)
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
            using Process p = new Process() { StartInfo = new ProcessStartInfo("nvidia-smi", "dmon -s puc -o T") };
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            //sadly nvoclock status is broken
            //so we gotta be using nvida-smi
            p.StartInfo.RedirectStandardOutput = true;
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
        public MinMaxDefaultSelected PowerPercent = new MinMaxDefaultSelected();
        public MinMaxDefaultSelected MemoryClockDelta = new MinMaxDefaultSelected();

        public GpuInfo(MinMaxDefaultSelected powerPercent, MinMaxDefaultSelected memoryClockDelta)
        {
            PowerPercent = powerPercent ?? throw new ArgumentNullException(nameof(powerPercent));
            MemoryClockDelta = memoryClockDelta ?? throw new ArgumentNullException(nameof(memoryClockDelta));
        }
        public GpuInfo()
        {
        }
    }

    public class MinMaxDefaultSelected
    {
        private int min;
        private int max;
        private int @default;
        private int selected;

        public int Min { get => min; set => min = value; }
        public int Max { get => max; set => max = value; }
        public int Default { get => @default; set => @default = value; }
        public int Selected { get => selected; set => selected = value; }

        public MinMaxDefaultSelected(int min, int max, int @default, int selected)
        {
            Min = min;
            Max = max;
            Default = @default;
            Selected = selected;
        }

        public MinMaxDefaultSelected()
        {
        }
    }
}

using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace NSFMinerUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            monitorProcess?.Kill(true);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            listGpus();
            getGpuInfo(0);
            //Load last config
            
        }

        GpuInfo Gpu;

        void listGpus()
        {
            var handler = new DataReceivedEventHandler((s, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    CBGpuList.Items.Add(e.Data);
                    CBGpuList.SelectedIndex = 0;
                }
            });
            Overclock.ListGpus(handler);
        }

        void getGpuInfo(uint index)
        {
            StringBuilder sb = new StringBuilder();
            var handler = new DataReceivedEventHandler((s, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    sb.Append(e.Data + "\n");
                }
            });
            Overclock.GetGpuInfo(0, handler);
            var gpuInfo = Overclock.GpuInfoStringToClass(sb.ToString());

            setPowerLimitControls(gpuInfo.PowerPercent);
            setMemoryControls(gpuInfo.MemoryClockDelta);
            Gpu = gpuInfo;
            //richTextBox1.Text = sb.ToString();
        }

        void setPowerLimitControls(MinMaxDefaultSelected power)
        {
            TBPower.Maximum = power.Max;
            TBPower.Minimum = power.Min;
            TBPower.Value = power.Default;
            NumPower.Maximum = power.Max;
            NumPower.Minimum = power.Min;
            NumPower.Value = power.Default;
        }

        void setMemoryControls(MinMaxDefaultSelected mem)
        {
            TBMemory.Maximum = mem.Max;
            TBMemory.Minimum = mem.Min;
            TBMemory.Value = mem.Default;
            NumMemory.Maximum = mem.Max;
            NumMemory.Minimum = mem.Min;
            NumMemory.Value = mem.Default;
        }



        bool disableInterference = false;

        private void TBPower_Scroll(object sender, EventArgs e)
        {
            if (disableInterference || Gpu == null) return;
            int percent = TBPower.Value;
            SetPowerlimitUiValue(percent);
        }

        private void TBMemory_Scroll(object sender, EventArgs e)
        {
            if (disableInterference || Gpu == null) return;
            int offset = TBMemory.Value;
            SetMemoryUiValue(offset);
        }

        private void NumPower_ValueChanged(object sender, EventArgs e)
        {
            if (disableInterference || Gpu == null) return;
            int percent = (int)NumPower.Value;
            SetPowerlimitUiValue(percent);
        }

        private void NumMemory_ValueChanged(object sender, EventArgs e)
        {
            if (disableInterference || Gpu == null) return;
            int offset = (int)NumMemory.Value;
            SetMemoryUiValue(offset);
        }

        void SetMemoryUiValue(int offset)
        {
            disableInterference = true;
            NumMemory.Value = offset;
            TBMemory.Value = offset;
            disableInterference = false;
        }

        void SetPowerlimitUiValue(int percent)
        {
            disableInterference = true;
            NumPower.Value = percent;
            TBPower.Value = percent;
            disableInterference = false;
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            Overclock.SetPowerlimit(TBPower.Value);
            Overclock.SetMemoryClock(TBMemory.Value);
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            SetMemoryUiValue(Gpu.MemoryClockDelta.Default);
            SetPowerlimitUiValue(Gpu.PowerPercent.Default);
        }


        Process monitorProcess;
        private delegate void SafeCallDelegate(string text);
        void writeMonitorText(string text)
        {
            if (TbMonitor.InvokeRequired)
            {
                var d = new SafeCallDelegate(writeMonitorText);
                TbMonitor.Invoke(d, new object[] { text });
            }
            else
            {
                TbMonitor.Text += text + "\n";
                TbMonitor.SelectionStart = TbMonitor.Text.Length;
                TbMonitor.ScrollToCaret();
            }
        }

        private void BtnStartMonitor_Click(object sender, EventArgs e)
        {
            var handler = new DataReceivedEventHandler((s, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    writeMonitorText(e.Data);
                }
            });
            monitorProcess = Overclock.StartMonitor(handler);
            stopcounter = 0;
        }

        int stopcounter = 0;
        private void BtnStopMonitor_Click(object sender, EventArgs e)
        {
            if(monitorProcess != null)
            {
                if (!monitorProcess.HasExited)
                {
                    monitorProcess.Kill(true);
                    writeMonitorText("STOPPED!");
                }
                else
                {
                    if(stopcounter >= 2) writeMonitorText("I wont stop any further...");
                    else writeMonitorText("Already stopped bro...!");

                    stopcounter++;
                }
            }
        }
    }
}

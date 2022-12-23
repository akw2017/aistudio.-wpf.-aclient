﻿using AIStudio.Wpf.Controls;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIStudio.Wpf.BasePage.Models
{
    public class WaitFor : IDisposable
    {
        private string Identifier;
        private int HashCode;
        private int Counter;
        private BusyBox BusyBox;
        private static ConcurrentDictionary<int, WaitFor> WaitForList = new ConcurrentDictionary<int, WaitFor>();

        public static WaitFor GetWaitFor(int hashcode, string identifier, string text = "正在获取数据", WaitingStyle waitingStyle = WaitingStyle.Busy)
        {
            WaitFor waitFor;
            if (!WaitForList.TryGetValue(hashcode, out waitFor))
            {
                waitFor = new WaitFor(hashcode, identifier, text, waitingStyle);
                WaitForList.TryAdd(hashcode, waitFor);
            }
            else
            {
                waitFor.BusyBox.WaitInfo = text;
            }
            Interlocked.Increment(ref waitFor.Counter);
            return waitFor;
        }

        private WaitFor(int hashcode, string identifier, string text = "正在获取数据", WaitingStyle waitingStyle = WaitingStyle.Busy)
        {
            HashCode = hashcode;
            Identifier = identifier;
            BusyBox = WindowBase.ShowWaiting(WaitingStyle.Busy, Identifier, text);
        }

        public void SetText(string text)
        {
            BusyBox.WaitInfo = text;
        }

        public void Dispose()
        {
            if (Interlocked.Decrement(ref Counter) == 0)
            {
                WindowBase.HideWaiting(Identifier);
                WaitForList.TryRemove(HashCode, out var waitFor);
            }
        }
    }
}

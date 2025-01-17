﻿using System;
using System.Collections.Generic;

namespace AIStudio.Wpf.Entity.Models
{
    public partial class Event
    {
        public long PersistenceId { get; set; }
        public string EventData { get; set; }
        public Guid EventId { get; set; }
        public string EventKey { get; set; }
        public string EventName { get; set; }
        public DateTime EventTime { get; set; }
        public bool IsProcessed { get; set; }
    }
}

﻿using Microsoft.Azure.Mobile.Server;

namespace SampleMobileApp.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }

        public string UserId { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiSampleMigrat.Models
{
    public class Response
    {
        public bool IsSuccesFull  { get; set; }
        public string Message { get; set; }
        public Object Result { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.BL.DTOs
{
   public  class ResponseDTO
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public dynamic Data { get; set; }
    }
}

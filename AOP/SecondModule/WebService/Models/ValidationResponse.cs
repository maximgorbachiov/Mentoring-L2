using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class ValidationResponse
    {
        public bool IsValid { get; set; }
        public string ValidationFunction { get; set; }
    }
}
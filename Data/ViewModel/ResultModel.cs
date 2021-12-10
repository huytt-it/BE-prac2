using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModel
{
    public class ResultModel
    {
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
        public bool Succeed { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTZ_Transports.Models
{
    public class MotoristaDTO : TbMotorista
    {
        public string status { get; set; }
        public List<TbTipoStatus> todosStatus { get; set; }
    }
}
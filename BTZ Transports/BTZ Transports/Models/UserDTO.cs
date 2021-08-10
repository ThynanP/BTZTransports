using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTZ_Transports.Models
{
    public class UserDTO : tbUser
    {
        public string TipoUser { get; set; }
        public string Descricao { get; set; }
    }
}
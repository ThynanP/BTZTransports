using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTZ_Transports.Models
{
    public class IndexPage
    {
        public List<TbAbastecimentos> abastecimentos { get; set; }
        public List<TbCarro> carros { get; set; }
        public List<TbMotorista> motoristas { get; set; }
        public string nomeUsuario { get; set; }
        public string usuario { get; set; }
        public TbTipoUser TipoUser { get; set; }
    }
}
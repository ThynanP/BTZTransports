using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTZ_Transports.Models
{
    public class AbastecimentoDTO : TbAbastecimentos
    {
        public List<TbCarro> carros { get; set; }
        public List<TbMotorista> motoristas { get; set; }
        public List<TbTipoCombustivel> combustiveis { get; set; }
    }
}
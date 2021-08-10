using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTZ_Transports.Models
{
    public class CarroView : TbCarro
    {
        public List<TbCarro> viewCarros { get; set; }
        public List<TbTipoCombustivel> combustivel { get; set; }
        public List<TbTipoFabricante> fabricante { get; set; }
    }
    public class CarroDTO : CarroView
    {
        public List<TbTipoStatusCar> TodosStatus { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BTZ_Transports.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TbCarro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TbCarro()
        {
            this.TbAbastecimentos = new HashSet<TbAbastecimentos>();
        }
    
        public int id { get; set; }
        public string nome { get; set; }
        public string placa { get; set; }
        public int idCombustivel { get; set; }
        public int idFabricante { get; set; }
        public int ano_fab { get; set; }
        public double cap_max { get; set; }
        public string observacoes { get; set; }
        public int statusId { get; set; }
        public System.DateTime createDate { get; set; }
        public System.DateTime updateDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TbAbastecimentos> TbAbastecimentos { get; set; }
        public virtual TbTipoCombustivel TbTipoCombustivel { get; set; }
        public virtual TbTipoFabricante TbTipoFabricante { get; set; }
        public virtual TbTipoStatusCar TbTipoStatusCar { get; set; }
    }
}

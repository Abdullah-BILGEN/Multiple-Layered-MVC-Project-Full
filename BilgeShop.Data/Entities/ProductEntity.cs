using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeShop.Data.Entities
{
    public class ProductEntity :BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? UnitPrice { get; set; }


        // nulll olmayan bir değer için (örneğin -Decimal), configüration ile (required (false)) verdiğimiz zaman  ? koymak zorundayım 

        // Null olabilen bir değer için configuration ile required(false) verilirse yeterlidir. ? kullanmaya gerek yok  (örnek -> string description) 
        public int UnitsInStock { get; set; }
        public string ImagePath { get; set; }

        public int CategoryId { get; set; }


        // realtion Propert

        public CategoryEntity Category  { get; set; }  // Bİr ürünün  bir categorsi olabilir 
    }

    public class ProductConfiguration : BaseConfiguration<ProductEntity>
    {

        public override void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.Property(x=>x.Name).HasMaxLength(50);
            
            builder.Property(x=>x.Description).HasMaxLength(200).IsRequired(false);

            builder.Property(x => x.UnitPrice).IsRequired(false);

            builder.Property(x=>x.ImagePath).IsRequired(false); 


            base.Configure(builder);
        }



    }
        
    
    
    
    
   
}

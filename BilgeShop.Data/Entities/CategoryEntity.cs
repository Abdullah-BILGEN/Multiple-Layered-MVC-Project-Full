using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeShop.Data.Entities
{
    public class CategoryEntity:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }


        // Relational property

        public List<ProductEntity> Products { get; set; } // çok ürün olacağı için list türünde 
    }

    public class CategoryConfiguration : BaseConfiguration<CategoryEntity>
    {

        public override void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(40);
            builder.Property(x=>x.Description).IsRequired(false);
            

            base.Configure(builder);// Bu silinmez silersen çalışmaz
        }



    }
}

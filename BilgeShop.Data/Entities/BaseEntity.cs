using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeShop.Data.Entities
{
    public abstract class BaseEntity
    {
        public  int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

    }



    // geleneksel olarak TEntity Yazılır oraya x de yazabilirsin değişken ismi
    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        //   configure methodumu miras verdiğim classslarda ezeceğim için virtual yapıyorum
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasQueryFilter(x => x.IsDeleted == false);
            // Bu veri tabanı içerisinde yapacağım tüm sorgulamalarda yukarıdaki linq geçerli olacak. Böylelikle silinmiş verilerle uğraşmama gerek yok 

            builder.Property(x => x.ModifiedDate).IsRequired(false);
            // Modified kolonu null değer alabilir. propertye ? eklemeyi unutma !
        }
    }

    // where TEntity : BaseEntity -> bu class ın yalnızca Base entitiy tipindeki yapılarla kullanılabileceğini söylüyor
    
}

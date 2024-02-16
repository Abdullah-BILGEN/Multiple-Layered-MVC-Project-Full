﻿using BilgeShop.Data.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeShop.Data.Context
{
    public class BilgeShopContext:DbContext
    {
        private readonly IDataProtector _dataProtector;
        public BilgeShopContext(DbContextOptions<BilgeShopContext> options, IDataProtectionProvider dataProtectionProvider) : base(options)
        {
            _dataProtector = dataProtectionProvider.CreateProtector("security");
        }
        //BilgeShop çalıştırıldığında deverye girecek olan burada verileri db context e gönderiyoruz ve verilerimiz dbcontext e uygun veri tabanı oluşturuyor 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // FLUENT API ->C# tarafındaki entitylerin sql tablolarına dönüşürkenki özelliklerine yaptığım müdehaleler(Configurations) 

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            var pwd = "123";

            pwd = _dataProtector.Protect(pwd);
            //SEED DATA -> veri tabanı ilk ayaklandırıldığında gelecek olan veri.

            modelBuilder.Entity<UserEntity>().HasData(new List<UserEntity>()
            {

                new UserEntity()
                {
                 Id=1,
                 FirstName="Bilge",
                 LastName="Adam",
                 Email="admin@bilgeshop.com",
                 Password=pwd,
                 UserType=Enums.UserTypeEnum.Admin
                }


            });

            base.OnModelCreating(modelBuilder);
        }


        // Tabloya dönüşecek classları belirtiyorum 
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();        
        public DbSet<ProductEntity> Products => Set<ProductEntity>();        
       

    }
}

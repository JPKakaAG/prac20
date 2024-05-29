using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace prac20.models;

public partial class Praktos20Context : DbContext
{
    public Praktos20Context()
    {
    }

    public Praktos20Context(DbContextOptions<Praktos20Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Заказы> Заказыs { get; set; }

    public virtual DbSet<СведенияОклиентах> СведенияОклиентахs { get; set; }

    public virtual DbSet<СправочникВидовРабот> СправочникВидовРаботs { get; set; }

    public virtual DbSet<СправочникИсполнителейРабот> СправочникИсполнителейРаботs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-I00R4RJ; Database=praktos20; User=sa; Password=1234567890; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Заказы>(entity =>
        {
            entity.HasKey(e => e.НомерЗаказа).HasName("PK__Заказы__33C9BE6354CA9121");

            entity.ToTable("Заказы");

            entity.Property(e => e.НомерЗаказа).ValueGeneratedNever();
            entity.Property(e => e.Клиент).HasMaxLength(50);
            entity.Property(e => e.МаркаАвтомобиля).HasMaxLength(50);

            entity.HasOne(d => d.КлиентNavigation).WithMany(p => p.Заказыs)
                .HasForeignKey(d => d.Клиент)
                .HasConstraintName("FK_Заказы_Клиент");

            entity.HasOne(d => d.КодИсполнителяNavigation).WithMany(p => p.Заказыs)
                .HasForeignKey(d => d.КодИсполнителя)
                .HasConstraintName("FK_Заказы_КодИсполнителя");

            entity.HasOne(d => d.КодРаботыNavigation).WithMany(p => p.Заказыs)
                .HasForeignKey(d => d.КодРаботы)
                .HasConstraintName("FK_Заказы_КодРаботы");
        });

        modelBuilder.Entity<СведенияОклиентах>(entity =>
        {
            entity.HasKey(e => e.Клиент).HasName("PK__Сведения__5950E5AC2C65BDFA");

            entity.ToTable("СведенияОКлиентах");

            entity.Property(e => e.Клиент).HasMaxLength(50);
            entity.Property(e => e.АдресОбъекта).HasMaxLength(100);
            entity.Property(e => e.НаименованиеОбъекта).HasMaxLength(100);
        });

        modelBuilder.Entity<СправочникВидовРабот>(entity =>
        {
            entity.HasKey(e => e.КодРаботы).HasName("PK__Справочн__76A3F6882D372AD1");

            entity.ToTable("СправочникВидовРабот");

            entity.Property(e => e.КодРаботы).ValueGeneratedNever();
            entity.Property(e => e.МаркаАвтомобиля).HasMaxLength(50);
            entity.Property(e => e.НаименованиеРаботы).HasMaxLength(100);
            entity.Property(e => e.СтоимостьРаботы).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<СправочникИсполнителейРабот>(entity =>
        {
            entity.HasKey(e => e.КодИсполнителя).HasName("PK__Справочн__F9EEAB14E9E33CF5");

            entity.ToTable("СправочникИсполнителейРабот");

            entity.Property(e => e.КодИсполнителя).ValueGeneratedNever();
            entity.Property(e => e.Адрес).HasMaxLength(100);
            entity.Property(e => e.НаименованиеОрганизации).HasMaxLength(100);
            entity.Property(e => e.Телефон).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

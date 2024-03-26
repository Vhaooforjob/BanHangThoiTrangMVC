using BanHangThoiTrangMVC.EntityTypeConfigurations.Interfaces;
using System;


public abstract class BaseEntityEmpty : IBaseEntity
{
    public Guid Id { get; set; }
}
public abstract class BaseEntity : BaseEntityEmpty, IBaseEntity
{
    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
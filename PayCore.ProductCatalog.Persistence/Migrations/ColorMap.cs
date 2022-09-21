using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PayCore.ProductCatalog.Domain.Entities;

namespace PayCore.ProductCatalog.Persistence.Migrations
{
    public class ColorMap:ClassMapping<Color>
    {
        public ColorMap()
        {
            Id(x => x.Id, x =>
            {
                x.Type(NHibernateUtil.Int32);
                x.Column("Id");
                x.UnsavedValue(0);
                x.Generator(Generators.Increment);
            });

     
            Property(b => b.ColorName, x =>
            {
                x.Length(50);
                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
            });
            Bag(color => color.Products, map => map.Key(k => k.Column("ColorId")), rel => rel.OneToMany());

            Table("color");
        }
    }
}


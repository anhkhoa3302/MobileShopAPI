using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Models;
using System.Reflection.Emit;

namespace MobileShopAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductImg> ProductImgs { get; set; } = null!;
        public virtual DbSet<Size> Sizes { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<UserRating> UserRatings { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("brand");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("image_url")
                    .HasComment("url hình ảnh");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("image_url")
                    .HasComment("url hình ảnh");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("color");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ColorName)
                    .HasMaxLength(255)
                    .HasColumnName("colorName");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("image");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.IsCover)
                    .HasColumnName("isCover")
                    .HasDefaultValueSql("((0))")
                    .HasComment("hình ảnh là ảnh bìa");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasComment("url hình ảnh");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.Id)
                    .HasMaxLength(400)
                    .HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_order_AspNetUsers");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("orderDetail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(400)
                    .HasColumnName("orderId");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orderDetail_UserOrder");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orderDetail_product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BrandId).HasColumnName("brandId");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.ColorId)
                    .HasColumnName("colorId")
                    .HasComment("part of primaryKey");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.SizeId)
                    .HasColumnName("sizeId")
                    .HasComment("part of primaryKey");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("userId");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_product_brand");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_product_catagory");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_product_color");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_product_size");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_product_AspNetUsers");
            });

            modelBuilder.Entity<ProductImg>(entity =>
            {
                entity.ToTable("productImg");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ImageId).HasColumnName("imageId");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.ProductImgs)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("fk_productImg_image");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImgs)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("fk_productImg_product");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("size");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.SizeName)
                    .HasMaxLength(255)
                    .HasColumnName("sizeName");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transaction");

                entity.Property(e => e.Id)
                    .HasMaxLength(400)
                    .HasColumnName("id");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(400)
                    .HasColumnName("orderId");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("userId");

                entity.Property(e => e.VnpAmount).HasColumnName("vnp_Amount");

                entity.Property(e => e.VnpBankCode)
                    .HasMaxLength(40)
                    .HasColumnName("vnp_BankCode");

                entity.Property(e => e.VnpCommand)
                    .HasMaxLength(30)
                    .HasColumnName("vnp_Command");

                entity.Property(e => e.VnpCreateDate).HasColumnName("vnp_CreateDate");

                entity.Property(e => e.VnpCurrCode)
                    .HasMaxLength(5)
                    .HasColumnName("vnp_CurrCode");

                entity.Property(e => e.VnpIpAddr)
                    .HasMaxLength(60)
                    .HasColumnName("vnp_IpAddr");

                entity.Property(e => e.VnpLocale)
                    .HasMaxLength(8)
                    .HasColumnName("vnp_Locale");

                entity.Property(e => e.VnpOrderInfo).HasColumnName("vnp_OrderInfo");

                entity.Property(e => e.VnpOrderType)
                    .HasMaxLength(100)
                    .HasColumnName("vnp_OrderType");

                entity.Property(e => e.VnpSecureHash).HasColumnName("vnp_SecureHash");

                entity.Property(e => e.VnpTmnCode)
                    .HasMaxLength(15)
                    .HasColumnName("vnp_TmnCode");

                entity.Property(e => e.VnpTxnRef)
                    .HasMaxLength(120)
                    .HasColumnName("vnp_TxnRef");

                entity.Property(e => e.VnpVersion)
                    .HasMaxLength(15)
                    .HasColumnName("vnp_Version");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("fk_transaction_order");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_transaction_AspNetUsers");
            });

            modelBuilder.Entity<UserRating>(entity =>
            {
                entity.ToTable("user_rating");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasDefaultValueSql("((1))")
                    .HasComment("1,2,3,4,5");

                entity.Property(e => e.UsderId)
                    .HasMaxLength(450)
                    .HasColumnName("usderId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.UserRatings)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_rating_product");

                entity.HasOne(d => d.Usder)
                    .WithMany(p => p.UserRatings)
                    .HasForeignKey(d => d.UsderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_rating_AspNetUsers");
            });
        }
    }
}

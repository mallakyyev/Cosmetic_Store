using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Cosmetics.DAL.Data;
using AutoMapper;
using Cosmetics.BLL.DTO.CatalogModelDTO;
using Cosmetics.DAL.Models.Catalog;
using Cosmetics.BLL.DTO.ProductModelDTO;
using Cosmetics.BLL.DTO.ManufactureModelDTO;
using Cosmetics.BLL.DTO.ConfigurationModelDTO;
using Cosmetics.DAL.Models.Configuration;
using Cosmetics.DAL.Models.Cart;
using Cosmetics.BLL.DTO.CartModelDTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Cosmetics.DAL.Models.Users;
using Cosmetics.BLL.DTO.ApplicationUserModelDTO;
using Cosmetics.Web.Extensions;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Cosmetics.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMemoryCache();
            services.AddDefaultIdentity<ApplicationUser>(options => 
            {
                options.Password.RequiredLength = 7;
                options.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
                options.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
                options.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
                options.Password.RequireDigit = false; // требуются ли цифры
                options.SignIn.RequireConfirmedAccount = true; 
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddRepositories();

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllersWithViews()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));
                })
                .AddRazorRuntimeCompilation()
                .AddViewLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("ru"),
                    new CultureInfo("en"),
                    new CultureInfo("tr")
                };

                foreach (var culture in supportedCultures)
                {
                    culture.NumberFormat.NumberDecimalSeparator = ".";
                }

                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            //services.AddMvc(option => option.EnableEndpointRouting = false)
            //    .AddRazorRuntimeCompilation()
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            //    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryTranslate, CategoryTranslateDTO>();
                //cfg.CreateMap<CategoryTranslate, CategoryDTO>()
                //.ForMember(p => p.Id, from => from.MapFrom(p => p.CategoryId))
                //.ForMember(p => p.Name, from => from.MapFrom(p => p.Name))
                //.ForMember(p => p.Description, from => from.MapFrom(p => p.Description))
                //.ForMember(p => p.DisplayOrder, from => from.MapFrom(p => p.Category.DisplayOrder))
                //.ForMember(p => p.Published, from => from.MapFrom(p => p.Category.Published))
                //.ForMember(p => p.ParentCategoryId, from => from.MapFrom(p => p.Category.ParentCategoryId))
                //.ForMember(p => p.Categories, from => from.MapFrom(p => p.Category.Categories));
                cfg.CreateMap<CategoryTranslateDTO, CategoryTranslate>();
                cfg.CreateMap<ProductTranslate, ProductTranslateDTO>();
                cfg.CreateMap<ProductTranslateDTO, ProductTranslate>();

                cfg.CreateMap<Category, CategoryDTO>();
                cfg.CreateMap<CategoryDTO, Category>();
                cfg.CreateMap<Category, CreateCategoryDTO>();
                cfg.CreateMap<CreateCategoryDTO, Category>();
                cfg.CreateMap<Category, EditCategoryDTO>();
                cfg.CreateMap<EditCategoryDTO, Category>();

                cfg.CreateMap<CategoryAndProduct, CategoryAndProductDTO>();
                cfg.CreateMap<CategoryAndProductDTO, CategoryAndProduct>();

                cfg.CreateMap<Manufacture, ManufactureDTO>();
                cfg.CreateMap<ManufactureDTO, Manufacture>();
                cfg.CreateMap<Manufacture, CreateManufactureDTO>();
                cfg.CreateMap<CreateManufactureDTO, Manufacture>();

                cfg.CreateMap<Currency, CurrencyDTO>();
                cfg.CreateMap<CurrencyDTO, Currency>();
                cfg.CreateMap<Currency, CreateCurrencyDTO>();
                cfg.CreateMap<CreateCurrencyDTO, Currency>();

                cfg.CreateMap<ProductPicture, ProductPictureDTO>();
                cfg.CreateMap<ProductPictureDTO, ProductPicture>();

                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<ProductDTO, Product>();
                cfg.CreateMap<Product, CreateProductDTO>();
                cfg.CreateMap<CreateProductDTO, Product>();
                cfg.CreateMap<Product, EditProductDTO>()
                .ForMember(p => p.CategoriesId, from => from.MapFrom(p => p.CategoryAndProducts.Select(s => s.CategoryId)));
                cfg.CreateMap<EditProductDTO, Product>();

                cfg.CreateMap<Language, LanguageDTO>();
                cfg.CreateMap<LanguageDTO, Language>();
                cfg.CreateMap<Language, CreateLanguageDTO>();
                cfg.CreateMap<CreateLanguageDTO, Language>();

                cfg.CreateMap<Country, CountryDTO>();
                cfg.CreateMap<CountryDTO, Country>();

                cfg.CreateMap<SettingPortal, SettingPortalDTO>();
                cfg.CreateMap<SettingPortalDTO, SettingPortal>();
                cfg.CreateMap<SettingPortalTranslate, SettingPortalTranslateDTO>();
                cfg.CreateMap<SettingPortalTranslateDTO, SettingPortalTranslate>();
                cfg.CreateMap<SettingPortal, EditSettingPortalDTO>();
                cfg.CreateMap<EditSettingPortalDTO, SettingPortal>();

                cfg.CreateMap<ShoppingCart, ShoppingCartListDTO>();
                cfg.CreateMap<ShoppingCartListDTO, ShoppingCart>();
                cfg.CreateMap<ShoppingCart, ShoppingCartCreateDTO>();
                cfg.CreateMap<ShoppingCartCreateDTO, ShoppingCart>();

                cfg.CreateMap<CheckoutPickupPoint, CheckoutPickupPointListDTO>();
                cfg.CreateMap<CheckoutPickupPointListDTO, CheckoutPickupPoint>();
                cfg.CreateMap<CheckoutPickupPoint, CheckoutPickupPointCreateDTO>();
                cfg.CreateMap<CheckoutPickupPointCreateDTO, CheckoutPickupPoint>();

                cfg.CreateMap<ApplicationUser, ApplicationUserDTO>();
                cfg.CreateMap<ApplicationUserDTO, ApplicationUser>();
                cfg.CreateMap<ApplicationUserDTO, ListApplicationUserDTO>();
                cfg.CreateMap<ListApplicationUserDTO, ApplicationUserDTO>();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRequestLocalization();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseFileServer(new FileServerOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "node_modules")
                ),
                RequestPath = "/node_modules",
                EnableDirectoryBrowsing = false
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "areas",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}

using Autofac;
using Autofac.Integration.WebApi;
using HotelReception.Repository;
using HotelReception.Repository.Common;
using HotelReception.Service;
using HotelReception.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using HotelReception.Model;
using HotelReception.Model.Common;


namespace HotelReception.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
           

            var config = GlobalConfiguration.Configuration;

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
    .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters
                .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);


            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Payment registrations
            builder.RegisterType<PaymentService>().As<IPaymentService>();
            builder.RegisterType<PaymentRepository>().As<IPaymentRepository>();

            // Reservation registrations
            builder.RegisterType<Reservation>().As<IReservation>();
            builder.RegisterType<ReservationService>().As<IReservationService>();
            builder.RegisterType<ReservationRepository>().As<IReservationRepository>();
            //Guest registrations
            builder.RegisterType<GuestModel>().As<IGuestModel>();
            builder.RegisterType<GuestService>().As<IGuestService>();
            builder.RegisterType<GuestRepository>().As<IGuestRepository>();
            builder.RegisterType<ActiveGuestModel>().As<IActiveGuestModel>();
            // Receipt registrations
            builder.RegisterType<Receipt>().As<IReceipt>();
            builder.RegisterType<ReceiptService>().As<IReceiptService>();
            builder.RegisterType<ReceiptRepository>().As<IReceiptRepository>();
            //PostalOffice registrations
            builder.RegisterType<PostalOfficeModel>().As<IPostalOfficeModel>();
            builder.RegisterType<PostalOfficeService>().As<IPostalOfficeService>();
            builder.RegisterType<PostalOfficeRepository>().As<IPostalOfficeRepository>();
            //Country registrations 
            builder.RegisterType<CountryModel>().As<ICountryModel>();
            builder.RegisterType<CountryService>().As<ICountryService>();
            builder.RegisterType<CountryRepository>().As<ICountryRepository>();
            //Address registrations
            builder.RegisterType<AddressModel>().As<IAddressModel>();
            builder.RegisterType<AddressService>().As<IAddressService>(); 
            builder.RegisterType<AddressRepository>().As<IAddressRepository>();

            //RoomReservation registrations
            builder.RegisterType<RoomReservation>().As<IRoomReservation>();
            builder.RegisterType<RoomReservationService>().As<IRoomReservationService>();
            builder.RegisterType<RoomReservationRepository>().As<IRoomReservationRepository>();

            //ReceptionistCredentials registrations
            builder.RegisterType<ReceptionCredentialsModel>().As<IReceptionCredentialsModel>();
            builder.RegisterType<ReceptionistModel>().As<IReceptionistModel>();
            builder.RegisterType<ReceptionCredentialsRepository>().As<IReceptionCredentialsRepository>();
            builder.RegisterType<ReceptionCredentialsService>().As<IReceptionCredentialsService>();
            //Room registrations
            builder.RegisterType<RoomService>().As<IRoomService>();
            builder.RegisterType<RoomRepository>().As<IRoomRepository>();

            //RoomType registrations
            builder.RegisterType<RoomTypeService>().As<IRoomTypeService>();
            builder.RegisterType<RoomTypeRepository>().As<IRoomTypeRepository>();

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            UnityConfig.RegisterComponents();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
